using HotelMVC.Attributes;
using HotelMVC.Helpers;
using HotelMVC.Models;
using HotelMVC.Services;
using HotelMVC.Static;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HotelMVC.Controllers
{
    public class ReservationController : Controller
    {
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult Reserve()
        {
            return View();
        }
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public JsonResult CheckForAvailableRooms(string CheckIn, string CheckOut)
        {
            var checkInDate = DateHelper.ParseDateFromString(CheckIn);
            var checkOutDate = DateHelper.ParseDateFromString(CheckOut);

            var rawData = new ReservationService().GetAvaiableRooms(checkInDate, checkOutDate);
            var data = rawData.Select(x => new RoomModel
            {
                Id = x.IdRoom.Value,
                RoomDescription = x.Description,
                Beds = x.Beds,
                DailyPrice = x.DailyPrice

            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retrieving number or reservation or error if occured
        /// </summary>
        /// <param name="IdRoom"></param>
        /// <param name="IdUser"></param>
        /// <param name="CheckIn"></param>
        /// <param name="CheckOut"></param>
        /// <returns></returns>
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public JsonResult CheckIn(int IdRoom, int IdUser, string CheckIn, string CheckOut)
        {
            var checkInDate = DateHelper.ParseDateFromString(CheckIn);
            var checkOutDate = DateHelper.ParseDateFromString(CheckOut);

            try
            {
                var reservationSucceded = new ReservationService().CheckIn(checkInDate, checkOutDate, IdRoom);
                if (reservationSucceded)
                {
                    new ReservationService().AddReservation(checkInDate, checkOutDate, IdRoom, IdUser, out int idReservation);
                    return Json(new { Id = idReservation }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { msg = "Pokój nie dostępny" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //TODO: log exception
                return Json(new { msg = "Coś poszło nie tak" }, JsonRequestBehavior.AllowGet);
            }

        }
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult Success(int idReservation)
        {
            ViewBag.Id = idReservation;
            return View();
        }

        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult Failure(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult MyReservation()
        {
            var viewModel = new ReservationService().GetMyReservation(SessionHelper.GetUserId());
            return View(viewModel);
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        public ActionResult ManageReservations()
        {
            var viewModel = new ReservationService().GetAllReservation();
            return View(viewModel);
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        public ActionResult Edit(int id)
        {
            var viewModel = new ReservationService().GetSingleReservation(id);
            return View(viewModel);
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        [HttpPost]
        public ActionResult Edit(ReservationModel model)
        {
            new ReservationService().UpdateReservation(model);
            return RedirectToAction(nameof(ManageReservations));
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        public ActionResult Delete(int id)
        {
            var viewModel = new ReservationService().GetSingleReservation(id);
            return View(viewModel);
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        [HttpPost]
        public ActionResult Delete(ReservationModel model)
        {
            new ReservationService().DeleteReservation(model.Id);
            return RedirectToAction(nameof(ManageReservations));
        }
    }
}