using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using Vakio.Models;
namespace Vakio.Controllers
{
    public class HomeController : Controller
    {
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private int viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear + 4) / 7);
        public ActionResult Index()
        {            
            if (HttpContext.Request.IsAuthenticated) {
                return RedirectToAction("Index", "Vakio");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {

            var user = _db.Users.FirstOrDefault(x => x.Password == password && x.Username == username);
            if (user!=null)
            {
                string userData = user.Username;
                FormsAuthentication.SetAuthCookie(userData, true);
                return RedirectToAction("Index", "Vakio");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            var myCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            myCookie.Expires = DateTime.Now.AddDays(-1D);
            Response.Cookies.Add(myCookie);
            return View("Index");
        }
    }
}
