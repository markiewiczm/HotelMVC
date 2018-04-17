using HotelMVC.Helpers;
using HotelMVC.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelMVC.Attributes
{
    public class RoleAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            }
            else
            {
                if (!RoleHelper.IsInRole(Roles.Split(',')[0]))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
                }
            }
        }
    }
}