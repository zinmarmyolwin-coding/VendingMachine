using System.ComponentModel.DataAnnotations;

namespace VendingMachineSystem.API.Models.ProductModel
{
    public class ProductRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity field is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
    }
}
