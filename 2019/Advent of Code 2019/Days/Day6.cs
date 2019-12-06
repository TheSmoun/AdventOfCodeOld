using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Days
{
    public sealed class Day6 : DayBase<Dictionary<string, Day6.Orbit>, int>
    {
        protected override string Name => "Day 6: Universal Orbit Map";

        protected override Dictionary<string, Orbit> ParseInput(IEnumerable<string> lines)
        {
            var orbits = new Dictionary<string, Orbit>();
            foreach (var (parent, orbiter) in lines.Select(l => l.Split(")")).Select(l => (l[0], l[1])))
            {
                var parentOrbit = orbits.TryGetValue(parent, out var p) ? p : new Orbit(parent);
                orbits[parent] = parentOrbit;

                var orbit = orbits.TryGetValue(orbiter, out var o) ? o : new Orbit(orbiter);
                orbit.SetParent(parentOrbit);
                orbits[orbiter] = orbit;
            }

            return orbits;
        }

        protected override int RunPart1(Dictionary<string, Orbit> input)
        {
            return input["COM"].CountIndirectOrbits();
        }

        protected override int RunPart2(Dictionary<string, Orbit> input)
        {
            var sanPath = input["SAN"].Path;
            var youPath = input["YOU"].Path;
            var common = sanPath.Intersect(youPath).First();
            return sanPath.IndexOf(common) + youPath.IndexOf(common);
        }

        public class Orbit
        {
            public string Name { get; }
            public Orbit Parent { get; private set; }
            public List<Orbit> Children { get; } = new List<Orbit>();

            public Orbit(string name)
            {
                Name = name;
            }

            public List<string> Path
            {
                get
                {
                    var path = new List<string>();

                    var orbit = Parent;
                    while (orbit != null)
                    {
                        path.Add(orbit.Name);
                        orbit = orbit.Parent;
                    }

                    return path;
                }
            }

            public int CountIndirectOrbits(int parentOrbits = default)
            {
                var count = 0;
                foreach (var child in Children)
                {
                    count += parentOrbits + 1;
                    count += child.CountIndirectOrbits(parentOrbits + 1);
                }

                return count;
            }

            public void SetParent(Orbit parent)
            {
                Parent = parent;
                Parent.Children.Add(this);
            }
        }
    }
}
