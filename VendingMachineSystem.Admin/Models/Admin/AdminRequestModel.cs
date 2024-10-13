using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.Models.Admin
{
    public class AdminRequestModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "User Role is required")]
        public string UserRole { get; set; }
    }
}
