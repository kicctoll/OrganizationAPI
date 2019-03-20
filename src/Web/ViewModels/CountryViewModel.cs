using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        [Required]
        public long Code { get; set; }
    }
}
