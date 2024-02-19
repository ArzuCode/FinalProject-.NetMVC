using System;

namespace Rent_a_Car_.Net.Models
{
    public class Rental:BaseEntity
    {
        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string InvoiceNo { get; set; }

        public bool isPaid { get; set; }

        #region CarRentalRelation
        public int CarId { get; set; }
        public Car Car { get; set; }
        #endregion

        #region UserRentalRelation
        public string UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
