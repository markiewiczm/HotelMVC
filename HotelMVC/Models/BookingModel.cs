using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Models
{
    public class BookingModel
    {
        public int IdReservation { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }


    }
}