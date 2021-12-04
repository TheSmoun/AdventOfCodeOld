using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day22 : DayBase<Day22.Technique[], long>
    {
        public override string Name => "Day 22: Slam Shuffle";

        public override Technique[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Select<string, Technique>(l =>
            {
                if (l.StartsWith("deal with increment "))
                    return new DealIncrementTechnique(int.Parse(l.Split(" ").Last()));
                if (l.StartsWith("cut "))
                    return new CutTechnique(int.Parse(l.Split(" ").Last()));
                return new DealTechnique();
            }).ToArray();
        }

        public override long RunPart1(Technique[] input)
        {
            return input.Aggregate(new Stack<int>(Enumerable.Range(0, 10007).Reverse()), (d, t) => t.Apply(d)).IndexOf(2019);
        }

        public override long RunPart2(Technique[] input)
        {
            const long d = 119315717514047L;
            const long n = 101741582076661L;

            Func<long, long, long, long> power = DealIncrementTechnique.Power;
            Func<long, long, long> modinv = DealIncrementTechnique.PrimeModInverse;

            /*
             * X = 2020
             * Y = f(x)
             * Z = f(Y)
             * A = (Y - Z) * modinv(X - Y + D, D) % D
             * B = (Y - A * X) % D
             * R = (pow(A, N, D) * X + (pow(A, N, D) - 1) * modinv(A - 1, D) * B) % D
             */

            const long x = 2020L;
            var y = input.Reverse().Aggregate(x, (i, t) => t.ApplyInverse(i, d));
            var z = input.Reverse().Aggregate(y, (i, t) => t.ApplyInverse(i, d));
            var a = (y - z) * modinv(x - y + d, d) % d;
            var b = (y - a * x) % d;
            
            return (power(a, n, d) * x + (power(a, n, d) - 1) * modinv(a - 1, d) * b) % d;
        }

        public abstract class Technique
        {
            public abstract Stack<int> Apply(Stack<int> deck);
            public abstract long ApplyInverse(long cardPos, long deckSize);
        }

        public sealed class DealTechnique : Technique
        {
            public override Stack<int> Apply(Stack<int> deck)
            {
                return new Stack<int>(deck);
            }

            public override long ApplyInverse(long cardPos, long deckSize)
            {
                return deckSize - 1 - cardPos;
            }
        }

        public sealed class CutTechnique : Technique
        {
            private readonly int _n;

            public CutTechnique(int n)
            {
                _n = n;
            }

            public override Stack<int> Apply(Stack<int> deck)
            {
                var n = (_n + deck.Count) % deck.Count;
                return new Stack<int>(deck.Take(n).Reverse().Concat(deck.Skip(n).Reverse()));
            }

            public override long ApplyInverse(long cardPos, long deckSize)
            {
                return (cardPos + _n + deckSize) % deckSize;
            }
        }

        public sealed class DealIncrementTechnique : Technique
        {
            private readonly int _n;

            public DealIncrementTechnique(int n)
            {
                _n = n;
            }

            public override Stack<int> Apply(Stack<int> deck)
            {
                var n = (_n + deck.Count) % deck.Count;
                var newDeck = new int[deck.Count];

                var i = 0;
                foreach (var card in deck)
                {
                    newDeck[i] = card;
                    i = (i + n) % deck.Count;
                }

                return new Stack<int>(newDeck.Reverse());
            }

            public override long ApplyInverse(long cardPos, long deckSize)
            {
                return PrimeModInverse(_n, deckSize) * cardPos % deckSize;
            }

            public static long PrimeModInverse(long n, long d)
            {
                return Power(n, n - 2, d);
            }

            public static long Power(long x, long y, long m)
            {
                if (y == 0)
                    return 1;
                var p = Power(x, y / 2, m) % m;
                p = p * p % m;
                return y % 2 == 0 ? p : x * p % m;
            }
        }
    }
}
