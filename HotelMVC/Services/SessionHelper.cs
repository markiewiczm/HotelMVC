using HotelMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Services
{
    public static class SessionHelper
    {
        public static int GetUserId()
        {
            var user = HttpContext.Current.Session["user"] as UserModel;
            return user.Id;
        }
    }
}