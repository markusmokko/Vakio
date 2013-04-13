using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vakio.Data
{
    public enum Veikkaus { 
        Nothing = 0,
        Yks = 1,
        Risti = 2,
        Kaks = 3,
        YksRisti = 4,
        YksKaks = 5,
        RistiKaks = 6,
        FullHouse = 7
    };
}