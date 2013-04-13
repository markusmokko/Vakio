using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Helpers;

namespace Vakio.Models
{
    public class Chat : Hub
    {
        private int viikko = Convert.ToInt32(DateTime.Now.Year + "" + (DateTime.Now.DayOfYear + 2) / 7);
        Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        public void Send(string message, int userid)
        {
            var addtime = DateTime.UtcNow.AddHours(2);
            _db.Shouts.InsertOnSubmit(new Data.Shout(){Created=addtime,CreatedBy=userid,Kommentti=message, Viikko=viikko});
            _db.SubmitChanges();
            var time = addtime.ToString("dd.MM.yyyy hh:mm:ss");
            Clients.All.addMessage(message, UserHelper.GetUserName(userid), time, userid);
        }
    }
}