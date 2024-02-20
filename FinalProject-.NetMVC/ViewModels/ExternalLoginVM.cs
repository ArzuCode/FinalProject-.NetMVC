using System.ComponentModel.DataAnnotations;

namespace Rent_a_Car_.Net.ViewModels
{
    public class ExternalLoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
