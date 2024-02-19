using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_a_Car_.Net.Models
{
    public class Team:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        [NotMappedAttribute]
        public IFormFile Image { get; set; }
    }
}
