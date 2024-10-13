using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VendingMachineSystem.AppDbContextModels;
using VendingMachineSystem.Models.Admin;
using VendingMachineSystem.Models.TransactionModel;

namespace VendingMachineSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> TransactionList(int pageIndex = 1, int pageSize = 10)
        {
            PaginatedList<TransactionResponseModel>? model = null;
            try
            {
                var result = (from tran in _dbContext.Transactions
                              join product in _dbContext.Products on tran.ProductId equals product.Id into productGroup
                              from product in productGroup.DefaultIfEmpty()
                              select new TransactionResponseModel
                              {
                                  ProductName = product != null ? product.Name : "Unknown",
                                  Quainty = (int)(tran.QuantityPurchased ?? 0),
                                  Price = (decimal)(tran.TotalPrice ?? 0)
                              })
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .ToList();


                var count = await _dbContext.Transactions.CountAsync();

                model = new PaginatedList<TransactionResponseModel>(result, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                model = null;
            }

            return View(model);
        }
    }
}
