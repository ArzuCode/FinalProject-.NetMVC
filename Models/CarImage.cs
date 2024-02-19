
namespace Rent_a_Car_.Net.Models
{
    public class CarImage: BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }



        #region CarImageCarRelation
        public int CarId { get; set; }
        public Car Car { get; set; }
        #endregion

    }
}
