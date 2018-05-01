using HotelMVC.DataContext;
using HotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelMVC.Services
{
    public class ReservationService
    {
        public List<BookingModel> GetAvaiableRooms(DateTime checkIn, DateTime checkOut)
        {
            using(HotelContext ctx = new HotelContext())
            {
                var results = ctx.Database.SqlQuery<BookingModel>("EXECUTE CheckForAvaiableRooms @checkIn, @checkOut",
                    new SqlParameter("@checkIn", checkIn), new SqlParameter("@checkOut", checkOut)).ToList();
                return results;
            }
        }

        public bool CheckIn(DateTime checkIn, DateTime checkOut,int idRoom)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var results =  ctx.Database.SqlQuery<BookingModel>("EXECUTE CheckForCheckIn @checkIn, @checkOut, @roomId",
                     new SqlParameter("@checkIn", checkIn), new SqlParameter("@checkOut", checkOut), new SqlParameter("@roomId", idRoom)).ToList();
                return results.Any();

            }
        }

        internal int AddReservation(DateTime checkInDate, DateTime checkOutDate, int idRoom, int idUser)
        {
            return 1;
        }
    }
}