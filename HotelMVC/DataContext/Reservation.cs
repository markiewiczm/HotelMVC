using HotelMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelMVC.DataContext
{
    [Table("Reservation")]
    public partial  class Reservation
    {
        public int Id { get; set; }
        public int IdRoom { get; set; }
        public int IdUser { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public ReservationStatus Status { get; set; }

    }
}