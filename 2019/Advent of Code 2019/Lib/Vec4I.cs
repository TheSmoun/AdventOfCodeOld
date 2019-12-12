using System;
using System.Collections.Generic;

namespace AoC2019.Lib
{
    public sealed class Vec4I : IEquatable<Vec4I>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }

        public Vec4I(IList<int> quadruple)
        {
            if (quadruple.Count != 4)
                throw new ArgumentException();

            X = quadruple[0];
            Y = quadruple[1];
            Z = quadruple[2];
            W = quadruple[3];
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                hashCode = (hashCode * 397) ^ W;
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Vec4I other && Equals(other);
        }

        public bool Equals(Vec4I other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }
    }
}
