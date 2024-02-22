using Rent_a_Car_.Net.Helpers;
using Rent_a_Car_.Net.Models;
using System.Collections.Generic;

namespace Rent_a_Car_.Net.ViewModels
{
    public class ElectricVM
    {
        public List<Car> Cars { get; set; }
        public PagedList<Car> PagedLists { get; set; }
    }
}
