using HotelMVC.Attributes;
using HotelMVC.Static;
using System.Web.Mvc;

namespace HotelMVC.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        [RoleAuth(Roles = ConstValues.NormalUser)]
        public ActionResult Index()
        {
            return View();
        }

        [RoleAuth(Roles = ConstValues.Admin)]
        public ActionResult ManageReservations()
        {
            return View();
        }
    }
}