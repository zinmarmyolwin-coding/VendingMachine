using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineSystem.API.DbContextModels;
using VendingMachineSystem.API.Models.ProductModel;
using VendingMachineSystem.API.Models.TransactionModel;

namespace VendingMachineSystem.API.ProductAPI
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TransactionAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ProductList")]
        public async Task<IActionResult> GetProducts()
        {
            var roleList = await _dbContext.Products.AsNoTracking().Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToListAsync();

            return Ok(roleList);
        }

        [HttpPost]
        [Route("Transaction")]
        public async Task<IActionResult> Transaction(TransactionRequestModel reqModel)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    ProductId = reqModel.ProductId,
                    PurchaseDate = DateTime.Now,
                    QuantityPurchased = reqModel.Quantity,
                    TotalPrice = (reqModel.Quantity * reqModel.Price)
                };
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) { }

            return Ok("Save Sucessfully");
        }

    }
}
