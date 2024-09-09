using Microsoft.EntityFrameworkCore;
using MonPointOfSaleFinal.App.Intefaces;
using MonPointOfSaleFinal.App.Models;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyDbContext _db;

        public CategoryRepository(MyDbContext db)
        {
            _db = db;
        }
        
        public async Task<Category> AddCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
          await   _db.SaveChangesAsync();
            return category;
        }
    }
}
