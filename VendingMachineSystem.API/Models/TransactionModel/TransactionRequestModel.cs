namespace VendingMachineSystem.API.Models.TransactionModel
{
    public class TransactionRequestModel
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
