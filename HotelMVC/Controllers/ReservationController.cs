using HotelMVC.Attributes;
using HotelMVC.Models;
using HotelMVC.Static;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HotelMVC.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult Reserve()
        {
            return View();
        }

        public JsonResult CheckForAvailableRooms(string CheckIn, string CheckOut)
        {
            var data = new List<RoomModel>
            {
                new RoomModel()
                {
                    Id = 1,
                    RoomNo = "204",
                    RoomDescription = "2-os pokój z pełnym wyposażeniem w tym Wifi i TV"
                },
                new RoomModel()
                {
                    Id = 2,
                    RoomNo = "205",
                    RoomDescription = "3-os pokój z pełnym wyposażeniem w tym Wifi i TV"
                }
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIn(int IdRoom, int IdUser, string CheckIn, string CheckOut)
        {
            var data = new { Id = 123 };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Summary(int Id)
        {
            return View(Id);
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        public ActionResult ManageReservations()
        {
            return View();
        }
    }
}