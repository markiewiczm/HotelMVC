using HotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Helpers
{
    public class RoleHelper
    {
        public static bool IsInRole(string role)
        {
            var user = HttpContext.Current.Session["user"] as UserModel;
            if (user?.Role == role)
                return true;
            return false;
        }
    }
}