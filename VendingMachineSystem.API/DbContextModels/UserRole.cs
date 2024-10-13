using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.AppDbContextModels
{
    public partial class UserRole
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string RoleId { get; set; }
    }
}
