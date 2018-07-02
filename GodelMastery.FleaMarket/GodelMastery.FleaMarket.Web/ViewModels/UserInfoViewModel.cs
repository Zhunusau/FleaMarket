namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class UserInfoViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Icon { get; set; }
    }
}