using BookStore.DBContexts;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly BookStoreDBContext context;
        public BookService(BookStoreDBContext context)
        {
            this.context = context;
        }

        public async Task<Book> Create(Book createBook)
        {
            try
            {
                context.Add(createBook);
                var bookId = await context.SaveChangesAsync();
                createBook.BookId = bookId;
                return createBook;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await context.Books.Include(c => c.Category).FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<Book> Modify(Book book)
        {
            try
            {
                context.Attach(book);
                context.Entry<Book>(book).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> Remove(int bookId)
        {
            try
            {
                var book = await GetBookById(bookId);
                book.IsDeleted = true;
                context.Attach(book);
                context.Entry<Book>(book).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> Restore(int bookId)
        {
            try
            {
                var book = await GetBookById(bookId);
                book.IsDeleted = false;
                context.Attach(book);
                context.Entry<Book>(book).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
