using BookStore.DBContexts;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BookStoreDBContext context;
        public CategoryService(BookStoreDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.Include(b => b.Books).Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await context.Categories.Include(b => b.Books).FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
