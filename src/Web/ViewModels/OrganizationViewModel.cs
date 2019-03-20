using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class OrganizationViewModel : BaseViewModel
    {
        [Required]
        public long Code { get; set; }

        [Required]
        public string OrganizationType { get; set; }

        [Required]
        public string Owner { get; set; }
    }
}
