using BookStore.DBContexts;
using BookStore.Entities;
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
    }
}
