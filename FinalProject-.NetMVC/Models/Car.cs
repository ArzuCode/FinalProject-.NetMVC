using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_a_Car_.Net.Models
{
    public class Car:BaseEntity
    {
        public string Name { get; set; }
        public int ModelYear { get; set; }
        public double DailyPrice { get; set; }
        public FuelTypes FuelType { get; set; }
        public TransmissionTypes TransmissionType { get; set; }
        public int PassengerCount { get; set; }

        [NotMappedAttribute]
        public List<IFormFile> Images { get; set; }


        #region BrandCarRelation
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        #endregion

        #region CarImageCarRelation
        public List<CarImage> CarImages { get; set; }
        #endregion

        #region CarColorRelation
        public int ColorId { get; set; }
        public Color Color { get; set; }
        #endregion

        #region CarRentalRelation
        public List<Rental> Rentals { get; set; }
        #endregion

        
        public enum FuelTypes
        {
            Benzin=1,
            Dizel,
            Qaz,
            Elektrik
        }

       
        public enum TransmissionTypes
        {
            Auto=1,
            Manual
        }

    }
}
