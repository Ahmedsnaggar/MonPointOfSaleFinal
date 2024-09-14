using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonPointOfSaleFinal.App.Intefaces;
using MonPointOfSaleFinal.App.Models;
using MonPointOfSaleFinal.Entities.Models;

namespace MonPointOfSaleFinal.App.Controllers
{
    public class ProductsController : Controller
    {
       private  IGenericRepository<Product> _ProductRepository;
        private IGenericRepository<Category> _CategoryRepository;
        private IWebHostEnvironment _environment;
        public ProductsController(IGenericRepository<Product> ProductRepository, IGenericRepository<Category> categoryRepository, IWebHostEnvironment environment)
        {
            _ProductRepository = ProductRepository;
            _CategoryRepository = categoryRepository;
            _environment = environment;
        }

        // GET: ProductsController

        public async Task<ActionResult> Index()
        {
            var products = await _ProductRepository.GetAllAsync(inculdes: new[] { "category" });
            return View(products);
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
                    string upLoadFolder = _environment.WebRootPath + "\\Images\\ProductsImages";
                    if(Directory.Exists(upLoadFolder) == false)
                    {
                        Directory.CreateDirectory(upLoadFolder);
                    }
                    string UniqueFileName = Guid.NewGuid().ToString() + "_" + item.ImageFile.FileName;
                    string FullPath = Path.Combine(upLoadFolder, UniqueFileName);

                    using(var stream = new FileStream(FullPath, FileMode.Create))
                    {
                        await item.ImageFile.CopyToAsync(stream);
                        stream.Dispose();
                    }
                    item.ProductImage = "\\Images\\ProductsImages\\" + item.ImageFile.FileName;
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
