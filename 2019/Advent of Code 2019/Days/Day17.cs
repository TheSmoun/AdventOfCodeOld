using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;
using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day17 : IntCodeDayBase<long>
    {
        public override string Name => "Day 17: Set and Forget";

        public override long RunPart1(long[] input)
        {
            var map = GetMap(input);
            var neighbors = new (int X, int Y)[] {(0, -1), (0, 1), (-1, 0), (1, 0)};
            var intersections = new List<(int X, int Y)>();
            foreach (var (coords, type) in map)
            {
                if (type == '.')
                    continue;

                if (neighbors.Select(n => (X: n.X + coords.X, Y: n.Y + coords.Y)).All(c => map[c] != '.'))
                    intersections.Add(coords);
            }

            return intersections.Select(c => c.X * c.Y).Sum();
        }

        public override long RunPart2(long[] input)
        {
            var map = GetMap(input);
            var pos = map.Single(kv => kv.Value != '.' && kv.Value != '#').Key;
            var scaffold = map.Where(kv => kv.Value == '#').Select(kv => kv.Key).ToHashSet();
            var remaining = scaffold.ToHashSet();

            var path = new List<string>();
            var heading = new Dictionary<char, (int X, int Y)>{{'^', (0, -1)}, {'v', (0, 1)}, {'<', (-1, 0)}, { '>', (1, 0)}}[map[pos]];
            var rotateRight = new Dictionary<(int X, int Y), (int X, int Y)>{{(0, -1), (1, 0)}, {(1, 0), (0, 1)}, {(0, 1), (-1, 0)}, {(-1, 0), (0, -1)}};
            var rotateLeft = new Dictionary<(int X, int Y), (int X, int Y)>{{(0, -1), (-1, 0)}, {(-1, 0), (0, 1)}, {(0, 1), (1, 0)}, {(1, 0), (0, -1)}};

            while (remaining.Count > 0)
            {
                var rightHeading = rotateRight[heading];
                var right = (pos.X + rightHeading.X, pos.Y + rightHeading.Y);
                if (scaffold.Contains(right))
                {
                    path.Add("R");
                    heading = rightHeading;
                }

                var leftHeading = rotateLeft[heading];
                var left = (pos.X + leftHeading.X, pos.Y + leftHeading.Y);
                if (scaffold.Contains(left))
                {
                    path.Add("L");
                    heading = leftHeading;
                }

                var count = 0;
                while (scaffold.Contains((pos.X + heading.X, pos.Y + heading.Y)))
                {
                    pos = (pos.X + heading.X, pos.Y + heading.Y);
                    count++;
                    remaining.Remove(pos);
                }

                path.Add(count.ToString());
            }

            var fullPath = string.Join(',', path);

            for (var lengthA = 10; lengthA > 1; lengthA -= 2)
            {
                var a = string.Join(',', path.GetRange(0, lengthA));
                if (a.Length > 20)
                    continue;

                var nextA = a.Length + 1;
                var countA = 1;
                while (fullPath.Substring(nextA, a.Length) == a)
                {
                    nextA += a.Length + 1;
                    countA++;
                }

                for (var lengthB = 10; lengthB > 1; lengthB -= 2)
                {
                    var b = string.Join(',', path.GetRange(lengthA * countA, lengthB));
                    if (b.Length > 20)
                        continue;

                    var nextB = nextA + b.Length + 1;
                    var countB = 1;
                    var countAB = 0;

                    var match = false;
                    do
                    {
                        if (fullPath.Substring(nextB, a.Length) == a)
                        {
                            match = true;
                            nextB += a.Length + 1;
                            countAB++;
                        }

                        if (fullPath.Substring(nextB, b.Length) == b)
                        {
                            match = true;
                            nextB += b.Length + 1;
                            countB++;
                        }
                    } while (match);

                    for (var lengthC = 10; lengthC > 1; lengthC -= 2)
                    {
                        var c = string.Join(',', path.GetRange(lengthA * (countA + countAB) + lengthB * countB, lengthC));
                        if (c.Length > 20)
                            continue;

                        var sequence = new List<string>();
                        for (var i = 0; i < fullPath.Length;)
                        {
                            if (i + a.Length <= fullPath.Length && fullPath.Substring(i, a.Length) == a)
                            {
                                i += a.Length + 1;
                                sequence.Add("A");
                            }
                            else if (i + b.Length <= fullPath.Length && fullPath.Substring(i, b.Length) == b)
                            {
                                i += b.Length + 1;
                                sequence.Add("B");
                            }
                            else if (i + c.Length <= fullPath.Length && fullPath.Substring(i, c.Length) == c)
                            {
                                i += c.Length + 1;
                                sequence.Add("C");
                            }
                            else
                            {
                                sequence.Clear();
                                break;
                            }
                        }

                        var s = string.Join(',', sequence);
                        var r = s.Length > 20 ? null : s;
                        if (!string.IsNullOrEmpty(r))
                        {
                            input[0] = 2;
                            var computerInput = r.Select(Convert.ToInt64).Append(10L)
                                .Concat(a.Select(Convert.ToInt64)).Append(10L)
                                .Concat(b.Select(Convert.ToInt64)).Append(10L)
                                .Concat(c.Select(Convert.ToInt64)).Append(10L)
                                .Append(Convert.ToInt64('n')).Append(10L).ToArray();

                            return new IntComputer(input, computerInput).Run().Last();
                        }
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private static DefaultDictionary<(int X, int Y), char> GetMap(IEnumerable<long> input)
        {
            var map = new DefaultDictionary<(int X, int Y), char>(() => '.', false);
            var x = 0;
            var y = 0;

            foreach (var c in new IntComputer(input).Run().Select(Convert.ToChar))
            {
                if (c == '\n')
                {
                    y++;
                    x = 0;
                }
                else
                {
                    map[(x++, y)] = c;
                }
            }

            return map;
        }
    }
}
