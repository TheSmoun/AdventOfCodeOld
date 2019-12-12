using System;
using System.Linq;

namespace AoC2019.Lib
{
    public sealed class Vec3I : IEquatable<Vec3I>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Vec3I()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vec3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3I(string s)
        {
            var parts = s[1..^1].Split(",").Select(p => int.Parse(p.Split("=")[1])).ToArray();
            X = parts[0];
            Y = parts[1];
            Z = parts[2];
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Vec3I other && Equals(other);
        }

        public bool Equals(Vec3I other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }

        public static Vec3I operator +(Vec3I left, Vec3I right)
        {
            return new Vec3I(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vec3I operator -(Vec3I left, Vec3I right)
        {
            return new Vec3I(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
    }
}
