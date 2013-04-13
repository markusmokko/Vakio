using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Data;
using Vakio.Helpers;

namespace Vakio.Models
{
    public class UserModel
    {

        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();        
        public string UserName { get; set; }
        public string Password { get; set; }

        public int MaksettuCount
        {
            get
            {
                var pelit = _db.Veikkauksets.OrderByDescending(x => x.Id).First();
                var maks = _db.Nakkilistas.First(x => x.Viikko == pelit.Viikko).Id;
                var temp =  _db.Nakkilistas.Where(c => c.Id <= maks).Count(x => x.User.Username == this.UserName);
                return temp;
            }
        }

        public List<Nakkilista> MaksetutPelit
        {
            get
            {
                var pelit = _db.Veikkauksets.OrderByDescending(x => x.Id).First();
                var maks = _db.Nakkilistas.First(x => x.Viikko == pelit.Viikko).Id;
                var temp = _db.Nakkilistas.Where(c => c.Id <= maks && c.User.Username == this.UserName).ToList();
                return temp;
            }
        }

        public decimal Osumaprosentti
        {
            get
            {
                return UserHelper.GetHitrate(UserHelper.GetUserId(this.UserName));                
            }
        }

        public decimal Maksajaprosentti
        {
            get
            {
                return UserHelper.GetMaksajaHitrate(UserHelper.GetUserId(this.UserName));
            }
        }
    }
}