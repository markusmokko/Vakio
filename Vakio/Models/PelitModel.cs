using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vakio.Data;

namespace Vakio.Models
{
    public class PelitModel
    {
        public List<Data.Pelit> Pelit { get; set; }

        public List<VeikkauksetCheckbox> VeikkauksetChekit { get; set; }

        public Dictionary<int, List<VeikkauksetCheckbox>> Muiden { get; set; }
        public bool MyTimeToShine { get; set; }
        public Dictionary<string, GamePercents> Peliprosentit { get; set; }
    }

    public class VeikkauksetCheckbox{
        public int Numero { get; set; }
        public bool Yks { get; set; }
        public bool Risti { get; set; }
        public bool Kaks { get; set; }        
    }
}