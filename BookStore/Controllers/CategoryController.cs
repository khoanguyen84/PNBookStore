using BookStore.Models;
using BookStore.Models.Category;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int? pageNumber, int? pageSize, string keyword)
        {
            var categories = await categoryService.GetCategories(); ;
            var pagination = new Pagination(categories.Count, pageNumber, pageSize, null);
            var catView = new CategoryView()
            {
                Categories = categories.Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList(),
                Pagination = pagination
            };
            return View(catView);
        }
    }
}
