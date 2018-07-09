using System.ComponentModel.DataAnnotations;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class FilterViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [Required (ErrorMessage = "The field is required")]
        [StringLength(16, ErrorMessage = "Name can not be more than 16 symbols")]
        public string FilterName { get; set; }

        [Required (ErrorMessage = "The field is required")]
        [StringLength(50, ErrorMessage = "Content can not be less than 4 and more than 50 symbols", MinimumLength = 4)]
        public string Content { get; set; }
        
    }
}