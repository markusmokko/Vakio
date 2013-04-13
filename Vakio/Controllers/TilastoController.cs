using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vakio.Controllers
{
    public class TilastoController : Controller
    {
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private int Viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear+4) / 7);

        public ActionResult Get()
        {
            var model = new Models.TilastotModel();
            model.Tilastot = new Dictionary<int, Dictionary<int, List<Models.VeikkauksetCheckbox>>>();

            foreach (var item in _db.Veikkauksets.GroupBy(x=>x.Viikko).ToList())
            {                
                var foo = new Dictionary<int, List<Models.VeikkauksetCheckbox>>();
                if (_db.Veikkauksets.Any(x => x.Viikko == item.Key && x.UserId == 99))
                {
                    foreach (var user in _db.Users.ToList())
                    {
                        var muidenCurrent = _db.Veikkauksets.FirstOrDefault(x => x.UserId == user.Id && x.Viikko == item.Key);
                        var list = new List<Models.VeikkauksetCheckbox>();

                        if (muidenCurrent == null)
                        {
                            for (int i = 1; i < 14; i++)
                            {
                                list.Add(new Models.VeikkauksetCheckbox { Kaks = false, Numero = i, Yks = false, Risti = false });
                            }
                        }
                        else
                        {
                            list = Helpers.SolveVeikkaus.Solve(muidenCurrent);
                        }
                        foo.Add(user.Id, list);
                    }
                    model.Tilastot.Add(item.Key, foo);
                }
            }
            
            return View(model);
        }

    }
}
