using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Helpers
{
    public static class DateHelper
    {
        public static DateTime ParseDateFromString(string input)
        {
            var array = Array.ConvertAll(input.Split('-'), item => int.Parse(item));            
            return new DateTime(array[2],array[1],array[0]);
        }
    }
}