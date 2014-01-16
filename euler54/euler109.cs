using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace euler54
{
    class euler109
    {
        public static int GetResults()
        {
            var values = from h in Enumerable.Range(1, 20)
                         from m in Enumerable.Range(1, 3)
                         select new Tuple<int, int>(h * m, m);

            List<Tuple<int, int>> boardHits = new List<Tuple<int, int>>();

            boardHits.Add(new Tuple<int, int>(25, 1));
            boardHits.Add(new Tuple<int, int>(50, 2));
            boardHits.AddRange(values);

            var Results = from s in Enumerable.Range(1, 99)
                          let h1 = DartsUsed1(boardHits, s)
                          let h2 = DartsUsed2(boardHits, s)
                          let h3 = DartsUsed3(boardHits, s)
                          select h1 + h2 + h3;

            return Results.Sum();
        }

        static int DartsUsed1(List<Tuple<int, int>> boardHits, int score)
        {
            return (from h in boardHits
                    where h.Item2 == 2 && h.Item1 == score
                    select h).Distinct().Count();
        }

        static int DartsUsed2(List<Tuple<int, int>> boardHits, int score)
        {
            return (from h1 in boardHits
                    from h2 in boardHits
                    where h2.Item2 == 2 && h1.Item1 + h2.Item1 == score
                    select new Tuple<Tuple<int, int>, Tuple<int, int>>(h1, h2)).Distinct().Count();
        }

        static int DartsUsed3(List<Tuple<int, int>> boardHits, int score)
        {
            var Suc = from h1 in boardHits
                      from h2 in boardHits
                      from h3 in boardHits
                      where h3.Item2 == 2 && h1.Item1 + h2.Item1 + h3.Item1 == score
                      select new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(h1, h2, h3);

            List<Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>> test = new List<Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>>();

            foreach (var t in Suc)
            {
                if (!test.Contains(t) && !test.Contains(new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(t.Item2, t.Item1, t.Item3)))
                {
                    test.Add(t);
                }
            }
            return test.Count;
        }



    }
}
