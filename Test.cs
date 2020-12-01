using System.Collections.Generic;
using System.Linq;

namespace RomanNumbers
{
    public static class Test
    {
        public static bool First()
        {
            bool pass = true;

            for (int i = 1; i <= 3999; i++)
            {
                try
                {
                    if (i.ToRoman().ToArabic() != i)
                    {
                        pass = false;
                        break;
                    }
                }
                catch
                {
                    pass = false;
                    break;
                }
            }

            return pass;
        }

        public static bool Second()
        {
            bool pass = true;

            var c = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<string> all = new List<string>();
            
            all.AddRange(c);
            all.AddRange((from f in c
                          from s in c
                          select new string[] { f, s }).Select(p => string.Join("", p)));
            all.AddRange((from f in c
                          from s in c
                          from t in c
                          select new string[] { f, s, t }).Select(p => string.Join("", p)));
            all.AddRange((from f in c
                          from s in c
                          from t in c
                          from o in c
                          select new string[] { f, s, t, o }).Select(p => string.Join("", p)));
            all.AddRange((from f in c
                          from s in c
                          from t in c
                          from o in c
                          from i in c
                          select new string[] { f, s, t, o, i }).Select(p => string.Join("", p)));

            all = all.Distinct().ToList();

            foreach (var a in all)
            {
                if (a.TryToArabic(out int arabic))
                {
                    if (arabic.TryToRoman(out string roman))
                    {
                        if (a != roman)
                        {
                            pass = false;
                        }
                    }
                    else
                    {
                        pass = false;
                    }
                }
            }

            return pass;
        }
    }
}
