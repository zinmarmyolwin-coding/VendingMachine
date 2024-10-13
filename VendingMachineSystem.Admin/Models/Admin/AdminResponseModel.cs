namespace VendingMachineSystem.Models.Admin
{
    public class AdminResponseModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<AdminUserModel> AdminUserList { get; set; }
    }

    public class AdminUserModel
    {
        public string? UserName { get; set; }
    }
}
