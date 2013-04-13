using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Models;

namespace Vakio.Data.Providers
{
    public class VeikkauksetProvider
    {
        private IUserRepository repo;

        public VeikkauksetProvider():this (new UserRepository()){}
        public VeikkauksetProvider(IUserRepository rep)
        {
            repo = rep;
        }

        public UserModel GetUserByName(string name) {
            var user = repo.GetByUserName(name);
            return new UserModel
            {
                Password = user.Password,
                UserName = user.Username
            };
            
        }
        public UserModel GetUserById(int id) {
            var user = repo.GetById(id);
            return new UserModel
            {
                Password = user.Password,
                UserName = user.Username
            };
        }
    }
}