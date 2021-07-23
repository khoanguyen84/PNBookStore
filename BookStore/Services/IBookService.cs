using BookStore.Entities;
using BookStore.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookService
    {
        Task<Book> Create(Book createBook);
    }
}
