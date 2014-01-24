
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

            edgeVisited.Add(edges.OrderBy(f => f.Item3).First());


            var allNodes = edges.Select(f => f.Item1).Distinct();
            var connectedNodes = edgeVisited.Select(f => f.Item1).Union(edgeVisited.Select(f => f.Item2)).Distinct();
            var unConnectedNodes = allNodes.Except(connectedNodes);

            while (unConnectedNodes.Any())
            {
                var addConnection = from e in edges
                                    where connectedNodes.Contains(e.Item1) && unConnectedNodes.Contains(e.Item2)
                                    orderby e.Item3
                                    select e;
                var addEdge = addConnection.First();
                edgeVisited.Add(addEdge);
            }

            return edgeVisited.Select(f=> f.Item3).Sum();
        }

        static int NodeParse(string s)
        {
            int ret = 0;
            if(int.TryParse(s,out ret))
                return ret;
            else
                return -1;
        }
    }
}
