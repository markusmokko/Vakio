using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Data.Repositories;
using Vakio.Models;

namespace Vakio.Data.Providers
{
    public class UserProvider
    {
        private IVeikkauksetRepository repo;

        public UserProvider():this (new VeikkauksetRepository()){}
        public UserProvider(IVeikkauksetRepository rep)
        {
            repo = rep;
        }

        public List<Veikkaukset> GetAll(string name) {
            return repo.GetAll().ToList();
        }

        public Veikkaukset GetNewest(string name)
        {
            return repo.GetNewest();
        }
    }
}