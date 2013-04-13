using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vakio.Models
{
    public class TilastotModel
    {

        public Dictionary<int, Dictionary<int, List<VeikkauksetCheckbox>>> Tilastot { get; set; }


        public Dictionary<int, List<VeikkauksetCheckbox>> Tilasto { get; set; }

    }
}