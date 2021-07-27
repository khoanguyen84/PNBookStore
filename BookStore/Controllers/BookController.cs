using BookStore.Entities;
using BookStore.Models;
using BookStore.Models.Book;
using BookStore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private static int categoryId;
        private static string categoryName;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBookService bookService;

        public BookController(ICategoryService categoryService,
                            IWebHostEnvironment webHostEnvironment,
                            IBookService bookService)
        {
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
            this.bookService = bookService;
        }
        [Route("/Book/Index/{catId=1}/{pageNumber=1}/{pageSize=10}/{keyword=''}")]
        public async Task<IActionResult> Index(int catId, int? pageNumber, int? pageSize, string keyword)
        {
            categoryId = catId;
            var category = await categoryService.GetCategoryById(catId);
            categoryName = category.CategoryName;
            var pagination = new Pagination(category.Books.Count, pageNumber, pageSize, keyword);
            keyword = keyword == "''" ? string.Empty : keyword;
            var books = string.IsNullOrEmpty(keyword) ? category.Books : category.Books.Where(b => b.BookName.Contains(keyword) || b.Authors.Contains(keyword)).ToList();
            books = books.OrderByDescending(b => b.BookId).ToList().Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            category.Books = books;
            var listBook = new ListBook()
            {

                Category = category,
                Pagination = pagination
            };
            return View(listBook);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBook createBook)
        {
            if (ModelState.IsValid)
            {
                string filename = "no-photo.jpg";
                if (createBook.Photo != null)
                {
                    string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                    filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{createBook.Photo.FileName}";
                    string fullpath = Path.Combine(folderPath, filename);
                    using (var file = new FileStream(fullpath, FileMode.Create))
                    {
                        createBook.Photo.CopyTo(file);
                    }
                }

                var newBook = new Book()
                {
                    Photo = $"/images/{filename}",
                    Authors = createBook.Authors,
                    Description = createBook.Description,
                    BookName = createBook.BookName,
                    CategoryId = categoryId,
                    IsDeleted = false,
                    Price = createBook.Price,
                    PublishYear = createBook.PublishYear,
                    Quantity = createBook.Quantity
                };
                await bookService.Create(newBook);
                return RedirectToAction("Index","Book", new { catId = categoryId });
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }

        [HttpGet]
        [Route("/Book/Modify/{bookId}")]
        public async Task<IActionResult> Modify(int bookId)
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            var book = await bookService.GetBookById(bookId);
            var modifyBook = new ModifyBook()
            {
                BookId = book.BookId,
                Authors = book.Authors,
                BookName = book.BookName,
                CategoryId = book.CategoryId,
                Description = book.Description,
                ExistPhoto = book.Photo,
                Price = book.Price,
                PublishYear = book.PublishYear,
                Quantity = book.Quantity
            };
            return View(modifyBook);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyBook modifyBook)
        {
            if (ModelState.IsValid)
            {
                var book = await bookService.GetBookById(modifyBook.BookId);
                if(book != null)
                {
                    string filename = book.Photo;
                    if (modifyBook.Photo != null)
                    {
                        //Delete old photo
                        var oldFileName = filename.Split("/")[2];
                        if (string.Compare(oldFileName, "no-photo.jpg") != 0)
                        {
                            System.IO.File.Delete(Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\", oldFileName));
                        }

                        string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                        filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{modifyBook.Photo.FileName}";
                        string fullpath = Path.Combine(folderPath, filename);
                        using (var file = new FileStream(fullpath, FileMode.Create))
                        {
                            modifyBook.Photo.CopyTo(file);
                        }
                    }

                    book.Photo = modifyBook.Photo != null ? $"/images/{filename}" : filename;
                    book.Authors = modifyBook.Authors;
                    book.Description = modifyBook.Description;
                    book.BookName = modifyBook.BookName;
                    book.CategoryId = categoryId;
                    book.Price = modifyBook.Price;
                    book.PublishYear = modifyBook.PublishYear;
                    book.Quantity = modifyBook.Quantity;
                    book.BookId = modifyBook.BookId;

                    await bookService.Modify(book);
                    return RedirectToAction("Index", "Book", new { catId = categoryId });
                }
                
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View(modifyBook);
        }

        [HttpGet]
        [Route("/Book/View/{bookId}")]
        public async Task<IActionResult> View(int bookId)
        {
            var book = await bookService.GetBookById(bookId);
            var viewBook = new ViewBook()
            {
                BookId = book.BookId,
                Authors = book.Authors,
                BookName = book.BookName,
                CategoryId = book.CategoryId,
                Description = book.Description,
                ExistPhoto = book.Photo,
                Price = book.Price,
                PublishYear = book.PublishYear,
                Quantity = book.Quantity,
                Category = book.Category
            };
            return View(viewBook);
        }

        [HttpGet]
        [Route("/Book/Remove/{bookId}")]
        public async Task<IActionResult> Remove(int bookId)
        {
            await bookService.Remove(bookId);
            return RedirectToAction("Index", "Book", new { catId = categoryId });
        }

        [HttpGet]
        [Route("/Book/Restore/{bookId}")]
        public async Task<IActionResult> Restore(int bookId)
        {
            await bookService.Restore(bookId);
            return RedirectToAction("Index", "Book", new { catId = categoryId });
        }
    }
}
