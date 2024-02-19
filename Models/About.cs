using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_a_Car_.Net.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HeaderDesc { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        [NotMappedAttribute]
        public IFormFile Image { get; set; }
    }
}
