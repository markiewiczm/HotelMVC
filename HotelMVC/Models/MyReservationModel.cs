using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Models
{
    public class MyReservationModel
    {
        public int Id { get; set; }

        public string RoomNo { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public string Status { get; set; }


    }

    public enum ReservationStatus
    {
        New = 1,
        Occupy = 2,
        Free = 3
    }
}