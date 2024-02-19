using System.Collections.Generic;

namespace Rent_a_Car_.Net.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }


        #region CarColorRelation
        public List<Car> Cars { get; set; }
        #endregion
    }
}
