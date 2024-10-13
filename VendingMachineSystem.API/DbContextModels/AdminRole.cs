using System;
using System.Collections.Generic;

namespace VendingMachineSystem.API.DbContextModels;

public partial class AdminRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }
}
