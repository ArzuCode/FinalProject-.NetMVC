using System;

namespace Rent_a_Car_.Net.Extentions
{
    public static class DateExtention
    {
            public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
            {
                return dateToCheck >= startDate && dateToCheck < endDate;
            }
    }
}
