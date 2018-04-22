using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMVC.DataContext
{
    [Table("Room")]
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }
        public int Id { get; set; }
        public string RoomNo { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}