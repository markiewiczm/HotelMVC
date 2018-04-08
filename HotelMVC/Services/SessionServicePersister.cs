using HotelMVC.Models;
using System.Web;

namespace HotelMVC.Services
{
    public class SessionServicePersister
    {
        private static string _user = "user";

        public static UserModel User
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                var sessionVar = HttpContext.Current.Session[_user];
                if (sessionVar != null)
                    return sessionVar as UserModel;
                return null;
            }
            set
            {
                HttpContext.Current.Session.Timeout = 10;
                HttpContext.Current.Session[_user] = value;
            }
        }
    }
}