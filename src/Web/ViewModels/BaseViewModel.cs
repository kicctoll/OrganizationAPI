using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BaseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
