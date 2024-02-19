using System.ComponentModel.DataAnnotations;

namespace Rent_a_Car_.Net.Models
{
    public class Payment
    {
        [Required(ErrorMessage = "Kart Sahibini qeyd edin")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Kart nömrəsini daxil edin")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Son istifadə ayını qeyd edin")]
        public string Month { get; set; }
        [Required(ErrorMessage = "Son istifadə ilini qeyd edin")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Cvc nömrəsini daxil edin")]
        public string Cvc { get; set; }
        public int Value { get; set; }
    }
}
