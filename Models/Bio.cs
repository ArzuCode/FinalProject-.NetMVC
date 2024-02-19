using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_a_Car_.Net.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string WorkTime { get; set; }

        [NotMappedAttribute]
        public IFormFile Image { get; set; }
    }
}
