using BookStore.Entities;
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
        [Route("/Book/Index/{catId=1}")]
        public async Task<IActionResult> Index(int catId)
        {
            categoryId = catId;
            var category = await categoryService.GetCategoryById(catId);
            categoryName = category.CategoryName;
            return View(category);
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
                    PublishYear = createBook.PublishYear
                };
                await bookService.Create(newBook);
                return RedirectToAction("Index","Book", new { catId = categoryId });
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }
    }
}
