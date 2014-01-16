using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace euler54
{
    public static
        class euler142
    {
        public static int GetResults()
        {
            //Find the smallest x + y + z with integers x > y > z > 0 such that x + y, x − y, x + z, x − z, y + z, y − z are all perfect squares.
            var allInts = Enumerable.Range(0,int.MaxValue);

            int i = 6;
            while (true)
            {

            }



        }
        static IEnumerable<int[]> eee(int l)
        {
            return from s1 in Enumerable.Range(1, l - 1)
                   from s2 in Enumerable.Range(1, l - 1)
                   from s3 in Enumerable.Range(1, l - 1)
                   where s1 + s2 + s3 == l && s1 < s2 && s2 < s3
                   select new int[] { s1, s2, s3 };
        }

        static bool PS(int l)
        {
            return false;
        }
    }
}
