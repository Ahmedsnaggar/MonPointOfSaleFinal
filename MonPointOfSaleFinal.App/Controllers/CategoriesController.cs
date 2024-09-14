using Microsoft.AspNetCore.Mvc;
using MonPointOfSaleFinal.App.Intefaces;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Controllers
{
    public class CategoriesController : Controller
    {
        //private MyDbContext _db;
        //public CategoriesController(MyDbContext db)
        //{
        //    _db = db;
        //}
        private IGenericRepository<Category> _repository;

        public CategoriesController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var categories = await _repository.GetAllAsync();
            return View(categories);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }
        [HttpGet]
        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            try
            {
                var Isexists = _repository.GetAllAsync().Result.Any(c=> c.CategoryName == item.CategoryName);
                if (Isexists == true) 
                {
                    ViewBag.ExistsError = "Category Already exists";
                    return View();
                }
               await  _repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category item)
        {
            try
            {
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
