using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace euler54
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] allLines = CardText.String.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var playerHands = from t in allLines
                              let split10 = t.Split(" ".ToArray())
                              let card10 = split10.Select(s => conCard(s))
                              select new { Player1 = card10.Take(5).ToArray(), Player2 = card10.Skip(5).ToArray() };

            var player1Win = from p in playerHands
                             let p1 = GetHandValue(p.Player1)
                             let p2 = GetHandValue(p.Player2)
                             where FirstIsGreater(p1, p2)
                             select 1;

            Console.WriteLine(player1Win.Count());
            Console.ReadLine();
        }


        static int[] GetHandValue(Tuple<int, string>[] hand)
        {


            Predicate<Tuple<int, string>[]> isFlush = c => c.Select(f => f.Item2).Distinct().Count() == 1;
            List<int> handRanks = hand.Select(c => c.Item1).OrderByDescending(c => c).ToList();
            int straightValue = IsStraight(handRanks);



            if (isFlush(hand) && straightValue != -1)
                return new int[] { 10, straightValue };

            List<int> kind4 = HasSets(handRanks, new int[] { 4, 1 });
            if (kind4 != null)
            {
                kind4.Insert(0, 9);
                return kind4.ToArray();
            }

            List<int> kind32 = HasSets(handRanks, new int[] { 3, 2 });
            if (kind32 != null)
            {
                kind32.Insert(0, 8);
                return kind32.ToArray();
            }

            if (isFlush(hand))
                return new int[] { 7, handRanks.Max() };

            if (straightValue != -1)
                return new int[] { 6, straightValue };

            List<int> kind311 = HasSets(handRanks, new int[] { 3, 1, 1 });
            if (kind311 != null)
            {
                kind311.Insert(0, 5);
                return kind311.ToArray();
            }

            List<int> kind221 = HasSets(handRanks, new int[] { 2, 2, 1 });
            if (kind221 != null)
            {
                kind221.Insert(0, 4);
                return kind221.ToArray();
            }

            List<int> kind2111 = HasSets(handRanks, new int[] { 2, 1, 1, 1 });
            if (kind2111 != null)
            {
                kind2111.Insert(0, 3);
                return kind2111.ToArray();
            }

            handRanks.Insert(0, 0);
            return handRanks.ToArray();

        }

        static List<int> HasSets(List<int> values, int[] findSet)
        {
            var setGroupings = from t in values
                               group t by t into g
                               orderby g.Count() descending, g.Key descending
                               select new { CardRank = g.Key, SetCount = g.Count() };

            var set = from s in setGroupings
                      orderby s.SetCount descending
                      select s.SetCount;

            if (Enumerable.SequenceEqual(set, findSet))
            {
                return setGroupings.Select(c => c.CardRank).ToList();
            }

            return null;
        }

        static int IsStraight(List<int> values)
        {
            int[] lowAce = { 14, 2, 3, 4, 5 };

            if (values.Distinct().Count() != 5)
                return -1;

            if (values.Intersect(lowAce).Count() == 5)
                return 5;

            if (values.Max() - values.Min() == 4)
                return values.Max();

            return -1;

        }

        static bool FirstIsGreater(int[] player1Value, int[] playerValue)
        {
            int i = 0;
            while (true)
            {
                if (player1Value[i] > playerValue[i])
                    return true;

                if (player1Value[i] < playerValue[i])
                    return false;

                i = i + 1;
            }

        }

        static Tuple<int, string> conCard(string s)
        {
            return new Tuple<int, string>(CardValue(s.Substring(0, 1)), s.Substring(1));

        }

        static int CardValue(string s)
        {
            switch (s)
            {
                case "A":
                    return 14;
                case "K":
                    return 13;
                case "Q":
                    return 12;
                case "J":
                    return 11;
                case "T":
                    return 10;
                default:
                    return int.Parse(s);
            }
        }

    }
}
