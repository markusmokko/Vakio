using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vakio.Models;

namespace Vakio.Controllers
{
    public class UserController : Controller
    {
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        public ActionResult Index()
        {
            var users = _db.Users.Where(x=>x.Id!=99).ToList();

            var model = new List<UserModel>();
            foreach (var item in users)
            {
                model.Add(new UserModel()
                {
                    UserName = item.Username,
                    Password=item.Password
                });
            }
            return View(model);
        }

    }
}
