using System;
using System.Collections.Generic;

namespace VendingMachineSystem.API.DbContextModels;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityAvailable { get; set; }
}
