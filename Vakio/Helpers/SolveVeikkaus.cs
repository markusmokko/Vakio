using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vakio.Helpers
{
    public static class SolveVeikkaus
    {
        private static Data.DataClasses1DataContext _db = new Data.DataClasses1DataContext();
        internal static List<Models.VeikkauksetCheckbox> Solve(Data.Veikkaukset currentVeikkaus)
        {
            var ret = new List<Models.VeikkauksetCheckbox>();
            ret.Add(AutaIdioottia(currentVeikkaus.Peli1,1));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli2,2));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli3, 3));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli4, 4));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli5, 5));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli6, 6));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli7, 7));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli8, 8));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli9, 9));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli10, 10));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli11, 11));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli12, 12));
            ret.Add(AutaIdioottia(currentVeikkaus.Peli13, 13));
            return ret;
        }

        public static Models.VeikkauksetCheckbox AutaIdioottia(int p, int numero)
        {
            var foo = new Models.VeikkauksetCheckbox { Numero = numero};
            switch (p)
            {
                case (int)Data.Veikkaus.Yks:
                    foo.Yks=true;
                    return foo;
                case (int)Data.Veikkaus.Risti:
                    foo.Risti = true;
                    return foo;
                case (int)Data.Veikkaus.Kaks:
                    foo.Kaks = true;
                    return foo;
                case (int)Data.Veikkaus.YksRisti:
                    foo.Yks = true;
                    foo.Risti = true;
                    return foo;
                case (int)Data.Veikkaus.YksKaks:
                    foo.Yks = true;
                    foo.Kaks = true;
                    return foo;
                case (int)Data.Veikkaus.RistiKaks:
                    foo.Risti = true;
                    foo.Kaks = true;
                    return foo;
                case (int)Data.Veikkaus.FullHouse:
                    foo.Yks = true;
                    foo.Kaks = true;
                    foo.Risti = true;
                    return foo;                    
            }

            return foo;
        }

        internal static int GetRightCount(Data.Veikkaukset item, Data.Veikkaukset oikearivi)
        {
            if (oikearivi==null)
            {
                return 0;
            }
            int count=0;
            if (Compare(item.Peli1, oikearivi.Peli1)) {
                count++;
            }
            if (Compare(item.Peli2, oikearivi.Peli2))
            {
                count++;
            }
            if (Compare(item.Peli3, oikearivi.Peli3))
            {
                count++;
            }
            if (Compare(item.Peli4, oikearivi.Peli4))
            {
                count++;
            }
            if (Compare(item.Peli5, oikearivi.Peli5)) {
                count++;
            }
            if (Compare(item.Peli6, oikearivi.Peli6))
            {
                count++;
            }
            if (Compare(item.Peli7, oikearivi.Peli7))
            {
                count++;
            }
            if (Compare(item.Peli8, oikearivi.Peli8))
            {
                count++;
            }
            if (Compare(item.Peli9, oikearivi.Peli9))
            {
                count++;
            }
            if (Compare(item.Peli10, oikearivi.Peli10))
            {
                count++;
            }
            if (Compare(item.Peli11, oikearivi.Peli11))
            {
                count++;
            }
            if (Compare(item.Peli12, oikearivi.Peli12))
            {
                count++;
            }
            if (Compare(item.Peli13, oikearivi.Peli13))
            {
                count++;
            }
            return count;
        }

        public static bool Compare(int p1, int p2)
        {
            switch (p1)
            {
                case (int)Data.Veikkaus.Yks:
                    if (p2==(int)Data.Veikkaus.Yks)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.Risti:
                    if (p2==(int)Data.Veikkaus.Risti)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.Kaks:
                    if (p2==(int)Data.Veikkaus.Kaks)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.YksRisti:
                    if (p2==(int)Data.Veikkaus.Yks || p2==(int)Data.Veikkaus.Risti)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.YksKaks:
                    if (p2 == (int)Data.Veikkaus.Yks || p2 == (int)Data.Veikkaus.Kaks)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.RistiKaks:
                    if (p2 == (int)Data.Veikkaus.Risti || p2 == (int)Data.Veikkaus.Kaks)
                    {
                        return true;
                    }
                    break;
                case (int)Data.Veikkaus.FullHouse:
                    return true;
            }
            return false;
        }

        public static bool IsGameCorrect(int userId, int gameNumber, int viikko)
        {
            if (System.Web.HttpContext.Current.Cache["RightRow"+userId+viikko]==null)
            {
                System.Web.HttpContext.Current.Cache["RightRow" + userId + viikko] = _db.Veikkauksets.FirstOrDefault(x => x.UserId == 99 && x.Viikko == viikko);
            }
            var rightRow = System.Web.HttpContext.Current.Cache["RightRow" + userId + viikko] as Data.Veikkaukset;

            if (System.Web.HttpContext.Current.Cache["userRow" + userId + viikko] == null)
            {
                if (!_db.Veikkauksets.Any(x => x.UserId == userId && x.Viikko == viikko))
                {
                    return false;
                }
                System.Web.HttpContext.Current.Cache["userRow" + userId + viikko] = _db.Veikkauksets.FirstOrDefault(x => x.UserId == userId && x.Viikko == viikko);
            }
            var userRow = System.Web.HttpContext.Current.Cache["userRow" + userId + viikko] as Data.Veikkaukset;
            
            switch (gameNumber)
            {
                case 1:
                    return Compare(userRow.Peli1, rightRow.Peli1);
                case 2:
                    return Compare(userRow.Peli2, rightRow.Peli2);
                case 3:
                    return Compare(userRow.Peli3, rightRow.Peli3);
                case 4:
                    return Compare(userRow.Peli4, rightRow.Peli4);
                case 5:
                    return Compare(userRow.Peli5, rightRow.Peli5);
                case 6:
                    return Compare(userRow.Peli6, rightRow.Peli6);
                case 7:
                    return Compare(userRow.Peli7, rightRow.Peli7);
                case 8:
                    return Compare(userRow.Peli8, rightRow.Peli8);
                case 9:
                    return Compare(userRow.Peli9, rightRow.Peli9);
                case 10:
                    return Compare(userRow.Peli10, rightRow.Peli10);
                case 11:
                    return Compare(userRow.Peli11, rightRow.Peli11);
                case 12:
                    return Compare(userRow.Peli12, rightRow.Peli12);
                case 13:
                    return Compare(userRow.Peli13, rightRow.Peli13);
                default:
                    return false;
                    
            }
        }
    }
}