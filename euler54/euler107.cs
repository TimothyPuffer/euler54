
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace euler54
{
    class euler107
    {
        public static int GetResults()
        {

            string[] allLines = CardText.File107.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            List<Tuple<int, int, int>> edges = new List<Tuple<int, int, int>>();

            int i = 0;
            foreach (string s in allLines)
            {
                int j = 0;
                List<Tuple<int, int>> connection = new List<Tuple<int, int>>();
                int parse;
                foreach (string e in s.Split(",".ToCharArray()))
                {
                    if (int.TryParse(e, out parse))
                        edges.Add(new Tuple<int, int, int>(i, j, parse));
                    j = j + 1;
                }
                i = i + 1;
            }

            List<Tuple<int, int, int>> edgeVisited = new List<Tuple<int, int, int>>();

            Func<int, IEnumerable<int>> AllConnectedNodes = delegate(int a)
            {
                int lastConnectCount = -1;
                List<int> con = new List<int>(new int [] {a});
                while (lastConnectCount != con.Count)
                {
                    lastConnectCount = con.Count;
                    var eee = from e in edgeVisited
                            where con.Contains(e.Item1) || con.Contains(e.Item2)
                            select new int[] { e.Item1, e.Item2 };
                    foreach (var e in eee)
                        con.AddRange(e);
                    con = con.Distinct().ToList();
                }
                return con;
            };

            Func<int, int, bool> CreateCycle = delegate(int a, int b)
            {
                return AllConnectedNodes(a).Contains(b);
            };


            while (true)
            {
                edgeVisited.Add(edges.OrderBy(f=>f.Item3).First(f => !CreateCycle(f.Item1, f.Item2)));
            }

            return edgeVisited.Select(f => f.Item3).Sum();
        }

    }
}
