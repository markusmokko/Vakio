using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vakio.Helpers;
using Vakio.Models;

namespace Vakio.Controllers
{
    public class ShoutController : Controller
    {

        public int UserId { get { return UserHelper.GetUserId(User.Identity.Name); } }
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private int viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear+4) / 7);

        public ActionResult Index()
        {
            var model = new ShoutModel
            {                
                Shouts = _db.Shouts.ToList()
            };
            
            return PartialView(model);
        }
    }
}
