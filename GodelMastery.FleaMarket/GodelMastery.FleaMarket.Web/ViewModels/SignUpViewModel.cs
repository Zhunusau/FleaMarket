using System.ComponentModel.DataAnnotations;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [StringLength(255, ErrorMessage = "FirstName can not be less than 1 and more than 255 symbols", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [StringLength(255, ErrorMessage = "LastName can not be less than 1 and more than 255 symbols", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password can not be less than 6 and more than 100 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        public string ConfirmPassword { get; set; }
    }
}