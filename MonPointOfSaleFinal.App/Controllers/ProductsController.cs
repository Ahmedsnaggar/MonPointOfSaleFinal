using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonPointOfSaleFinal.App.Intefaces;
using MonPointOfSaleFinal.App.Models;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
       private  IGenericRepository<Product> _ProductRepository;
        private IGenericRepository<Category> _CategoryRepository;
        private IWebHostEnvironment _environment;
        private IUploudFile _uploadFile;
        public ProductsController(IGenericRepository<Product> ProductRepository, IGenericRepository<Category> categoryRepository, IWebHostEnvironment environment, IUploudFile uploadFile)
        {
            _ProductRepository = ProductRepository;
            _CategoryRepository = categoryRepository;
            _environment = environment;
            _uploadFile = uploadFile;
        }
        [AllowAnonymous]
        // GET: ProductsController
        public async Task<ActionResult> Index(string search)
        {
            IEnumerable<Product> products;
            if (string.IsNullOrEmpty(search))
            {
                products = await _ProductRepository.GetAllAsync(inculdes: new[] { "category" });
            }
            else
            {
                ViewBag.Search = search;
                products = await _ProductRepository.GetAllAsync(p=> p.ProductName.Contains(search) ,inculdes: new[] { "category" });
            }
            
            return View(products);
        }
        [Authorize(Roles ="Admin, User")]
        public async Task<ActionResult> SearchWithAjax(string search)
        {
            IEnumerable<Product> products;
            if (string.IsNullOrEmpty(search))
            {
                products = await _ProductRepository.GetAllAsync(inculdes: new[] { "category" });
            }
            else
            {
                ViewBag.Search = search;
                products = await _ProductRepository.GetAllAsync(p => p.ProductName.Contains(search), inculdes: new[] { "category" });
            }
            return PartialView("_ProductsCards", products);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var product = await _ProductRepository.GetByIdAsync(id);
            return View(product);
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            //ViewBag.categories = await _CategoryRepository.GetAllAsync();
            var categories = await _CategoryRepository.GetAllAsync();
            var product = new Product() { categoryList  = categories.ToList()};
            return View(product);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product item)
        {
            try
            {
                
                if (item.ImageFile != null)
                {
                    string FilePath = await _uploadFile.UploadFileAsync("\\Images\\ProductsImages\\", item.ImageFile);
                    item.ProductImage = FilePath;
                }

                await _ProductRepository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _CategoryRepository.GetAllAsync();
            var product = await _ProductRepository.GetByIdAsync(id);
            product.categoryList = categories.ToList();
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product item)
        {
            try
            {
                if (item.ImageFile != null)
                {
                    string FilePath = await _uploadFile.UploadFileAsync("\\Images\\ProductsImages\\", item.ImageFile);
                    item.ProductImage = FilePath;
                }
                await _ProductRepository.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _ProductRepository.GetByIdAsync(id);
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product item)
        {
            try
            {
                await _ProductRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
