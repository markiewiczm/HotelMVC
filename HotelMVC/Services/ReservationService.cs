using HotelMVC.DataContext;
using HotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public static List<SelectListItem> GetStatuses()
        {
        
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "New",
                    Value = "1"
                },
                 new SelectListItem()
                {
                    Text = "Occupy",
                    Value = "2"
                },
                  new SelectListItem()
                {
                    Text = "Free",
                    Value = "3"
                },

            };

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

        public List<ReservationModel> GetMyReservation(int idUser)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservation = ctx.Reservations.Where(x => x.IdUser == idUser).ToList();
                var reservationVM = reservation.Select(x => new ReservationModel
                {
                    Id = x.Id,
                    RoomNo = x.Room.RoomNo,
                    CheckIn = x.CheckIn.ToShortDateString(),
                    CheckOut = x.CheckOut.ToShortDateString(),
                    Status = x.Status.ToString()

                }).ToList();
                return reservationVM;
            }
        }

        public List<ReservationModel> GetAllReservation()
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservation = ctx.Reservations.ToList();
                var reservationVM = reservation.Select(x => new ReservationModel
                {
                    Id = x.Id,
                    RoomNo = x.Room.RoomNo,
                    CheckIn = x.CheckIn.ToShortDateString(),
                    CheckOut = x.CheckOut.ToShortDateString(),
                    Status = x.Status.ToString(),
                    User = x.User.UserName

                }).ToList();
                return reservationVM;
            }
        }

        public void UpdateReservation(ReservationModel model)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservationToEdit = ctx.Reservations.FirstOrDefault(x => x.Id == model.Id);
                reservationToEdit.Status = (ReservationStatus) Enum.Parse(typeof(ReservationStatus), model.Status);
                ctx.SaveChanges();
            }

        }

        public void DeleteReservation(int id)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservationToDelete = ctx.Reservations.FirstOrDefault(x => x.Id == id);
                ctx.Reservations.Remove(reservationToDelete);
                ctx.SaveChanges();
            }
        }

        public ReservationModel GetSingleReservation(int id)
        {
            using (HotelContext ctx = new HotelContext())
            {
                var reservation = ctx.Reservations.FirstOrDefault(x => x.Id == id);
                var reservationVM = new ReservationModel
                {
                    Id = reservation.Id,
                    RoomNo = reservation.Room.RoomNo,
                    CheckIn = reservation.CheckIn.ToShortDateString(),
                    CheckOut = reservation.CheckOut.ToShortDateString(),
                    Status = reservation.Status.ToString(),
                    User = reservation.User.UserName
                };

                return reservationVM;
            }
        }
    }
}