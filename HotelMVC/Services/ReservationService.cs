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
            using (HotelContext ctx = new HotelContext())
            {
                var results = ctx.Database.SqlQuery<BookingModel>("EXECUTE CheckForAvaiableRooms @checkIn, @checkOut",
                    new SqlParameter("@checkIn", checkIn), new SqlParameter("@checkOut", checkOut)).ToList();
                return results;
            }
        }

        public bool CheckIn(DateTime checkIn, DateTime checkOut, int idRoom)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var results = ctx.Database.SqlQuery<BookingModel>("EXECUTE CheckForCheckIn @checkIn, @checkOut, @roomId",
                     new SqlParameter("@checkIn", checkIn), new SqlParameter("@checkOut", checkOut), new SqlParameter("@roomId", idRoom)).ToList();
                return results.Any();

            }
        }

        public void AddReservation(DateTime checkInDate, DateTime checkOutDate, int idRoom, int idUser, out int idReservation)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservationEntity = new Reservation()
                {
                    CheckIn = checkInDate,
                    CheckOut = checkOutDate,
                    IdRoom = idRoom,
                    IdUser = idUser,
                    Status = ReservationStatus.New
                };
                ctx.Reservations.Add(reservationEntity);
                ctx.SaveChanges();

                idReservation = reservationEntity.Id;
            }
        }

        public List<MyReservationModel> GetMyReservation(int idUser)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservation = ctx.Reservations.Where(x => x.IdUser == idUser);
                var reservationVM =  reservation.Select(x => new MyReservationModel
                {
                    Id = x.Id,
                    RoomNo = x.Room.RoomNo,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    Status = x.Status.ToString()

                }).ToList();
                return reservationVM;
            }
        }
    }
}