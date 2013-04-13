using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vakio.Data.Repositories
{
    public interface IVeikkauksetRepository
    {        
        IQueryable<Veikkaukset> GetAll();
        Veikkaukset GetNewest();
    }

    public class VeikkauksetRepository : IVeikkauksetRepository
    {
        public VeikkauksetRepository() : this(new DataClasses1DataContext()) { }

        public VeikkauksetRepository(DataClasses1DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private DataClasses1DataContext _dataContext;

        public IQueryable<Veikkaukset> GetAll()
        {
            return _dataContext.Veikkauksets;
        }

        public Veikkaukset GetNewest() {
            return _dataContext.Veikkauksets.OrderByDescending(x => x.Id).First();
        }
    }
}