namespace VendingMachineSystem.Models.TransactionModel
{
    public partial class TransactionResponseModel
    {
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public int Quainty { get; set; }
        public decimal Price { get; set; }
    }
}
