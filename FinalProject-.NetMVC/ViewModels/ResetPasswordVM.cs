using System.ComponentModel.DataAnnotations;

namespace Rent_a_Car_.Net.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifrə")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifrəni təstiqləyin")]
        [Compare("Password",
            ErrorMessage = "Şifrələr eyni deyil!")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
