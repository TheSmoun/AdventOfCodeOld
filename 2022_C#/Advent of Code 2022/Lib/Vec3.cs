using System.Numerics;

namespace Advent_of_Code_2022.Lib;

public class Vec3<TNumber> : IEquatable<Vec3<TNumber>>,
    IAdditionOperators<Vec3<TNumber>, Vec3<TNumber>, Vec3<TNumber>>,
    IAdditiveIdentity<Vec3<TNumber>, Vec3<TNumber>>,
    ISubtractionOperators<Vec3<TNumber>, Vec3<TNumber>, Vec3<TNumber>>
    where TNumber : INumber<TNumber>
{
    public TNumber X { get; }
    public TNumber Y { get; }
    public TNumber Z { get; }

    public Vec3() : this(TNumber.Zero) { }

    public Vec3(TNumber s) : this(s, s, s) { }

    public Vec3(TNumber x, TNumber y, TNumber z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void Deconstruct(out TNumber x, out TNumber y, out TNumber z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    public bool Equals(Vec3<TNumber>? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        
        return EqualityComparer<TNumber>.Default.Equals(X, other.X)
               && EqualityComparer<TNumber>.Default.Equals(Y, other.Y)
               && EqualityComparer<TNumber>.Default.Equals(Z, other.Z);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((Vec3<TNumber>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}, Z: {Z}";
    }

    public static Vec3<TNumber> operator +(Vec3<TNumber> left, Vec3<TNumber> right)
    {
        return new Vec3<TNumber>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Vec3<TNumber> AdditiveIdentity { get; } = new(TNumber.AdditiveIdentity);
    
    public static Vec3<TNumber> operator -(Vec3<TNumber> left, Vec3<TNumber> right)
    {
        return new Vec3<TNumber>(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }
}
