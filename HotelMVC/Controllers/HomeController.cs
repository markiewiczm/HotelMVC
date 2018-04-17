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
                SignInUser(user);
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
                    SignInUser(userModel);
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

        private void SignInUser(UserModel user)
        {
            Session.Timeout = 15;
            Session.Add("user", user);
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}