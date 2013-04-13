using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vakio.Helpers;

namespace Vakio.Controllers
{
    public class ResultsController : Controller
    {        
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private int Viikko = Convert.ToInt32(DateTime.Now.Year+""+(DateTime.Now.DayOfYear+4) / 7);

        public ActionResult Index()
        {            
            var tulokset = _db.Veikkauksets.Where(x => x.UserId == 99).ToList();            

            if (!tulokset.Any(x=>x.Viikko==Viikko))
            {
                tulokset.Add(new Data.Veikkaukset{Viikko=Viikko,UserId=99});
            }
            return View(tulokset);
        }
    }
}
