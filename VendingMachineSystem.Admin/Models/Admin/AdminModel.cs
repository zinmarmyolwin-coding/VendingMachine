namespace VendingMachineSystem.Models.Admin
{
    public class AdminModel
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SkipRowCount => (PageNo - 1) * PageSize;
    }
}
