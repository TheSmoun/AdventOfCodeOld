using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;
using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day12 : DayBase<Day12.Moon[], long>
    {
        public override string Name => "Day 12: The N-Body Problem";

        public override Moon[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(l => new Moon(new Vec3I(l))).ToArray();
        }

        public override long RunPart1(Moon[] input)
        {
            for (var step = 0; step < 1000; step++)
            {
                SimulateStep(ref input);
            }

            return input.Sum(m => m.TotalEnergy);
        }

        public override long RunPart2(Moon[] input)
        {
            var xStates = new HashSet<(Vec4I, Vec4I)>();
            var yStates = new HashSet<(Vec4I, Vec4I)>();
            var zStates = new HashSet<(Vec4I, Vec4I)>();

            var x = 0;
            var y = 0;
            var z = 0;

            var xFound = false;
            var yFound = false;
            var zFound = false;

            while (true)
            {
                if (!xFound) CheckFound(input, v => v.X, ref xStates, ref xFound, ref x);
                if (!yFound) CheckFound(input, v => v.Y, ref yStates, ref yFound, ref y);
                if (!zFound) CheckFound(input, v => v.Z, ref zStates, ref zFound, ref z);

                if (xFound && yFound && zFound)
                    break;

                SimulateStep(ref input);
            }

            return GetGreatestCommonFactor(x, GetGreatestCommonFactor(y, z));
        }

        private static void SimulateStep(ref Moon[] moons)
        {
            foreach (var (a, b) in moons.Pairs())
            {
                a.Velocity += GetGravity(a, b);
            }

            foreach (var moon in moons)
            {
                moon.Position += moon.Velocity;
            }
        }

        private static Vec3I GetGravity(Moon a, Moon b)
        {
            var posDiff = b.Position - a.Position;
            return new Vec3I(Math.Min(1, Math.Max(-1, posDiff.X)), Math.Min(1, Math.Max(-1, posDiff.Y)), Math.Min(1, Math.Max(-1, posDiff.Z)));
        }

        private static void CheckFound(Moon[] moons, Func<Vec3I, int> axis, ref HashSet<(Vec4I, Vec4I)> states, ref bool found, ref int count)
        {
            var positions = new Vec4I(moons.Select(m => axis(m.Position)).ToArray());
            var velocities = new Vec4I(moons.Select(m => axis(m.Velocity)).ToArray());
            var state = (positions, velocities);
            if (!states.Add(state))
            {
                found = true;
                return;
            }

            count++;
        }

        private static long GetGreatestCommonFactor(long a, long b)
        {
            return a * b / GetGreatestCommonDivisor(a, b);
        }

        private static long GetGreatestCommonDivisor(long a, long b)
        {
            while (b > 0)
            {
                (a, b) = (b, a % b);
            }

            return a;
        }

        public class Moon
        {
            public Vec3I Position { get; set; }
            public Vec3I Velocity { get; set; }

            public Moon(Vec3I position)
            {
                Position = position;
                Velocity = new Vec3I();
            }

            public int TotalEnergy
            {
                get
                {
                    var potentialEnergy = Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
                    var kineticEnergy = Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
                    return potentialEnergy * kineticEnergy;
                }
            }
        }
    }
}
