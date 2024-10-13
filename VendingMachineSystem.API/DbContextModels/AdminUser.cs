using System;
using System.Collections.Generic;

namespace VendingMachineSystem.API.DbContextModels;

public partial class AdminUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? PasswordHash { get; set; }

    public int AccessFailedCount { get; set; }

    public bool? Delflag { get; set; }
}
