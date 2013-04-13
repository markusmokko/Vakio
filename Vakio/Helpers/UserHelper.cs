using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Vakio.Helpers
{    
    public static class UserHelper
    {
       private static Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();

        public static string GetUserName(int id) {
            return _db.Users.First(x => x.Id == id).Username;
        }
        public static Data.User Maksaja(int viikko) {

            return _db.Nakkilistas.First(x => x.Viikko == viikko).User;
            
        }

        public static decimal GetHitrate(int userId) {
            var foo = _db.Veikkauksets.Where(x=>x.UserId==userId && x.Id>15).ToList();
            int count = 0;
            int viikkoCount = 0;
            foreach (var item in foo)
            {
                var oikearivi = _db.Veikkauksets.FirstOrDefault(x=>x.Viikko==item.Viikko && x.UserId==99);
                if (oikearivi!=null)
                {
                    viikkoCount++;
                    count += SolveVeikkaus.GetRightCount(item, oikearivi);
                }               
            }

            return Convert.ToDecimal(count) / Convert.ToDecimal(viikkoCount*13);
        }

        public static decimal GetMaksajaHitrate(int userId)
        {
            if (userId != 99)
            {
                var nakkilista = _db.Nakkilistas.Where(x => x.UserId == userId).Select(y => y.Viikko).ToList();
                var foo = _db.Veikkauksets.Where(x => x.UserId == userId && x.Id > 15 && nakkilista.Contains(x.Viikko)).ToList();


                int count = 0;
                int viikkoCount = 0;
                var jakocount = 0;
                foreach (var item in foo)
                {
                    var oikearivi = _db.Veikkauksets.FirstOrDefault(x => x.Viikko == item.Viikko && x.UserId == 99);
                    if (oikearivi != null)
                    {
                        viikkoCount++;
                        var temp = SolveVeikkaus.GetRightCount(item, oikearivi);
                        count += temp;
                        jakocount++;
                    }
                }

                return Convert.ToDecimal(count) / Convert.ToDecimal(jakocount * 13);
            }
            return 0;
        }


        public static int GetWeekHitCount(int userid, int week) {

            return SolveVeikkaus.GetRightCount(_db.Veikkauksets.First(x => x.UserId == userid), _db.Veikkauksets.First(x => x.UserId == 99));
        }

        public static int GetUserId(string p)
        {
            return _db.Users.First(x => x.Username == p).Id;
        }
    }
}