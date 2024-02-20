using System.ComponentModel.DataAnnotations;

namespace Rent_a_Car_.Net.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
