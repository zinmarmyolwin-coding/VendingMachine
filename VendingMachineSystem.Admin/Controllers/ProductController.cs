using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachineSystem.AppDbContextModels;
using VendingMachineSystem.Models.Admin;
using VendingMachineSystem.Models.Product;

namespace VendingMachineSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(int pageIndex = 1, int pageSize = 10)
        {
            PaginatedList<Product> model = null;
            try
            {
                var productlist = await _dbContext.Products.AsNoTracking()
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var count = await _dbContext.Products.CountAsync();

                model = new PaginatedList<Product>(productlist, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                model = null;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestModel reqModel)
        {
            try
            {
                Product product = new Product()
                {
                    Name = reqModel.Name,
                    Price = reqModel.Price,
                    QuantityAvailable = reqModel.Quantity,
                };

                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return View(reqModel);
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                ViewBag.ErrorMessage = "This Product is not found!";
            }
            ProductRequestModel reqModel = new ProductRequestModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (decimal)product.Price,
                Quantity = (int)product.QuantityAvailable,
            };
            return View(reqModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductRequestModel reqModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _dbContext.Products.Where(x => x.Id == reqModel.Id).AsNoTracking().FirstOrDefaultAsync();
                    if (product == null)
                    {
                        ViewBag.ErrorMessage = "This Product is not found!";
                    }

                    product.Name = reqModel.Name;
                    product.Price = reqModel.Price;
                    product.QuantityAvailable = reqModel.Quantity;

                    _dbContext.Products.Update(product);
                    _dbContext.SaveChanges();

                }
            }
            catch (Exception)
            {
                return View(reqModel);
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                ViewBag.ErrorMessage = "This Product is not found!";
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("ProductList");
        }

    }
}
