using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public string RoomNo { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public string Status { get; set; }

        public string User { get; set; }

    }

    public enum ReservationStatus
    {
        New = 1,
        Occupy = 2,
        Free = 3
    }
}