using System;
using System.Collections.Generic;

namespace VendingMachineSystem.AppDbContextModels;

public partial class AdminUserRole
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;
}
