using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonPointOfSaleFinal.App.Intefaces;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Controllers
{
    public class StoresController : Controller
    {
        private IGenericRepository<Store> _StoreRpository;

        public StoresController(IGenericRepository<Store> storeRpository)
        {
            _StoreRpository = storeRpository;
        }

        // GET: StoresController
        public async Task<ActionResult> Index()
        {
            var Store = await _StoreRpository.GetAllAsync();
            return View("StoresList", Store);
        }

        // GET: StoresController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var store = await _StoreRpository.GetByIdAsync(id);
            return View("StoreDetails", store);
        }

        // GET: StoresController/Create
        public ActionResult Create()
        {
            return View("CreateStore");
        }

        // POST: StoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Store item)
        {
            try
            {
                await _StoreRpository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("CreateStore");
            }
        }

        // GET: StoresController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var store = await _StoreRpository.GetByIdAsync(id);
            return View("EditStore", store);
        }

        // POST: StoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Store item)
        {
            try
            {
                await _StoreRpository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditStore");
            }
        }

        // GET: StoresController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var store = await _StoreRpository.GetByIdAsync(id);
            return View("DeleteStore", store);
        }

        // POST: StoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _StoreRpository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DeleteStore");
            }
        }
    }
}
