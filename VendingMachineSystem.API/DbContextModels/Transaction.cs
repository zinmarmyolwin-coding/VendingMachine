using System;
using System.Collections.Generic;

namespace VendingMachineSystem.API.DbContextModels;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? QuantityPurchased { get; set; }

    public decimal? TotalPrice { get; set; }
}
