using System;

namespace HotelMVC.Models
{
    public class BookingModel
    {
        public int? IdReservation { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? IdRoom { get; set; }
        public string RoomNo { get; set; }
        public int Beds { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

    }
}