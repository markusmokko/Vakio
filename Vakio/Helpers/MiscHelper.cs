using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Data;

namespace Vakio.Helpers
{
    public static class MiscHelper
    {
        private static Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        private static int Viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear+4) / 7);
        public static bool NakkilistaShortage() {
            if (_db.Nakkilistas.Count(x=>x.Viikko>Viikko)<6)
            {
                return true;
            }
            return false;
        }

        /*internal static Dictionary<string, GameOdds> GetOdds() {

            var baseurl = "https://www.veikkaus.fi";
            var aloitusUrl = baseurl + "/mobile?area=wagering&game=sport&op=frontpage";
            var dic = new Dictionary<string, GameOdds>();

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(aloitusUrl);
            var link = doc.GetElementbyId("linkto.fixedodds.frontpage");

            var url = new Uri(baseurl + link.GetAttributeValue("href", null).Replace("amp;", ""));
            doc = hw.Load(url.AbsoluteUri);




            if (link == null)
            {
                return null;
            }

            return dic;
        }
        */

        internal static Dictionary<string, GamePercents> GetPercents()
        {
            try
            {
                var baseurl = "https://www.veikkaus.fi";
                var aloitusUrl = baseurl + "/mobile?area=wagering&game=sport&op=frontpage";

                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load(aloitusUrl);
                var link = doc.GetElementbyId("linkto.sport1.betpercentages");
                if (link == null)
                {
                    return null;
                }
                var url = new Uri(baseurl + link.GetAttributeValue("href", null).Replace("amp;", ""));

                doc = hw.Load(url.AbsoluteUri);

                var gamelist = new List<string>();

                foreach (HtmlNode cell in doc.DocumentNode.SelectNodes("//tr/td"))
                {
                    gamelist.Add(cell.InnerText);
                    if (gamelist.Count == 52)
                    {
                        break;
                    }
                }

                var dic = new Dictionary<string, GamePercents>();
                var n = 1;
                for (int i = 0; i < gamelist.Count; i = i + 4)
                {
                    dic.Add("t" + n.ToString(), new GamePercents()
                    {
                        Joukkueet = gamelist[i],
                        Prosentti1 = gamelist[i + 1],
                        ProsenttiX = gamelist[i + 2],
                        Prosentti2 = gamelist[i + 3]
                    });
                    n++;

                }
                return dic;
            }
            catch (Exception)
            {
                return null;

            }
        }
    }
}