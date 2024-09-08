using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonPointOfSaleFinal.App.Models;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Controllers
{
    public class CategoriesController : Controller
    {
        private MyDbContext _db;
        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            var categories = _db.Categories;
            return View(categories.ToList());
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create(Category item)
        {
            try
            {
                var Isexists = _db.Categories.Any(c=> c.CategoryName == item.CategoryName);
                if (Isexists == true) 
                {
                    ViewBag.ExistsError = "Category Already exists";
                    return View();
                }
                _db.Categories.Add(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
