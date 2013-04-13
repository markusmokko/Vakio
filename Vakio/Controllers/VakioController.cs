using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Vakio.Data;
using Vakio.Helpers;

namespace Vakio.Controllers
{
    public class VakioController : Controller
    {
        public int UserId { get { return UserHelper.GetUserId(User.Identity.Name); } }   
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private int viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear+4) / 7);

        public JsonResult GetGames()
        {            
            var dic = Helpers.MiscHelper.GetPercents();
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(dic);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVoitot() { 
        
            var model = _db.Voittos.ToList();
            var culture = new CultureInfo("fi-FI");
            ViewBag.Summa = (model.Sum(x => x.Voitto1) - Convert.ToDecimal(_db.Pelits.GroupBy(x=>x.Viikko).Count() * 32)).ToString("c", culture);

        return PartialView("Voitot", model);
        }
        public ActionResult AddVoitto() { 
            ViewBag.Viikko = viikko;
            return PartialView("AddVoitto");
        }
        [HttpPost]
        public void AddVoitto(decimal summa)
        {
            _db.Voittos.InsertOnSubmit(new Voitto() { Voitto1=summa,Viikko=viikko,UserId=UserId});
            _db.SubmitChanges();            
        }
        public ActionResult Index()
        {
            //var temp = MiscHelper.GetOdds();
            var model = new Models.PelitModel();
            model.Pelit = _db.Pelits.Where(x => x.Viikko == viikko).ToList();
            model.VeikkauksetChekit = new List<Models.VeikkauksetCheckbox>();

            var currentVeikkaus = _db.Veikkauksets.FirstOrDefault(x => x.UserId == UserId  && x.Viikko==viikko);

            if (currentVeikkaus == null)
            {
                for (int i = 1; i < 14; i++)
                {
                    model.VeikkauksetChekit.Add(new Models.VeikkauksetCheckbox { Kaks = false, Numero = i, Yks = false, Risti = false });
                }
            }
            else
            {
                model.VeikkauksetChekit = Helpers.SolveVeikkaus.Solve(currentVeikkaus);
            }
            ViewBag.User = _db.Users.First(x => x.Id == UserId).Username;
            model.Muiden = new Dictionary<int, List<Models.VeikkauksetCheckbox>>();
            foreach (var item in _db.Users.Where(x => x.Id != UserId).ToList())
            {
                var muidenCurrent = _db.Veikkauksets.FirstOrDefault(x => x.UserId == item.Id && x.Viikko == viikko);
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
                
                model.Muiden.Add(item.Id, list);
            }

            model.MyTimeToShine = _db.Nakkilistas.Any(x => x.UserId == UserId && x.Viikko == viikko);
            model.Peliprosentit = Helpers.MiscHelper.GetPercents();
            return View(model);
        }

        public ActionResult InsertGames() {
            var model = new List<Data.Pelit>();
            if (_db.Pelits.Any(x => x.Viikko == viikko))
            {
                foreach (var item in _db.Pelits.Where(x=>x.Viikko==viikko).ToList())
                {
                    model.Add(item);
                }
            }
            else
            {
                for (int i = 1; i < 14; i++)
                {
                    model.Add(new Data.Pelit { Numero = i, Viikko = viikko });
                }
            }

            return View(model);        
        }

        [HttpPost]
        public ActionResult InsertGames(List<Data.Pelit> model) 
        {

            var pelit = _db.Pelits.Where(x => x.Viikko == viikko).ToList();
            if (pelit.Any())
            {
                foreach (var item in pelit)
                {
                    item.Joukkueet = model[item.Numero - 1].Joukkueet;
                }
            }
            else
            {
                foreach (var item in model)
                {
                    _db.Pelits.InsertOnSubmit(item);
                }
            }
            
            _db.SubmitChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Index(Models.PelitModel model) 
        {
            var rivi = _db.Veikkauksets.FirstOrDefault(x => x.UserId == UserId && x.Viikko == viikko);

            if (rivi==null)
            {
                rivi = new Data.Veikkaukset { UserId = UserId, Viikko = viikko, MasterPlan = model.MyTimeToShine };
                _db.Veikkauksets.InsertOnSubmit(rivi);
            }

            rivi.Peli1 = Solve(model.VeikkauksetChekit[0]);
            rivi.Peli2 = Solve(model.VeikkauksetChekit[1]);
            rivi.Peli3 = Solve(model.VeikkauksetChekit[2]);
            rivi.Peli4 = Solve(model.VeikkauksetChekit[3]);
            rivi.Peli5 = Solve(model.VeikkauksetChekit[4]);
            rivi.Peli6 = Solve(model.VeikkauksetChekit[5]);
            rivi.Peli7 = Solve(model.VeikkauksetChekit[6]);
            rivi.Peli8 = Solve(model.VeikkauksetChekit[7]);
            rivi.Peli9 = Solve(model.VeikkauksetChekit[8]);
            rivi.Peli10 = Solve(model.VeikkauksetChekit[9]);
            rivi.Peli11 = Solve(model.VeikkauksetChekit[10]);
            rivi.Peli12 = Solve(model.VeikkauksetChekit[11]);
            rivi.Peli13 = Solve(model.VeikkauksetChekit[12]);
            
            _db.SubmitChanges();
            return RedirectToAction("Index");
        }

        private int Solve(Models.VeikkauksetCheckbox veikkauksetCheckbox)
        {
            if (veikkauksetCheckbox.Yks && !veikkauksetCheckbox.Risti && !veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.Yks;
            }
            if (!veikkauksetCheckbox.Yks && veikkauksetCheckbox.Risti && !veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.Risti;
            }
            if (!veikkauksetCheckbox.Yks && !veikkauksetCheckbox.Risti && veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.Kaks;
            }
            if (veikkauksetCheckbox.Yks && veikkauksetCheckbox.Risti && veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.FullHouse;
            }
            if (!veikkauksetCheckbox.Yks && veikkauksetCheckbox.Risti && veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.RistiKaks;
            }
            if (veikkauksetCheckbox.Yks && !veikkauksetCheckbox.Risti && veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.YksKaks;
            }
            if (veikkauksetCheckbox.Yks && veikkauksetCheckbox.Risti && !veikkauksetCheckbox.Kaks)
            {
                return (int)Data.Veikkaus.YksRisti;
            }
            return 0;
        }

    }
}
