using HotelMVC.Models;
using HotelMVC.Services;
using System.Web.Mvc;

namespace HotelMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            var user = UserService.SignIn(userModel);
            if (user != null)
            {
                SessionServicePersister.User = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Niepoprawne dane logowania";
                return View(user);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (UserService.Register(userModel))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }
        }

        public ActionResult Logout()
        {
            SessionServicePersister.User = null;
            return RedirectToAction("Index", "Home");
        }

    }
}