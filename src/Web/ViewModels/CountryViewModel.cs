using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;

namespace Web.ViewModels
{
    public class CountryViewModel : BaseChildViewModel<Organization>
    {
        [Required]
        public long Code { get; set; }
    }
}
