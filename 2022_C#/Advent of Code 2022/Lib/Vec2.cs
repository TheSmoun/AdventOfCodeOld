using System.Numerics;

namespace Advent_of_Code_2022.Lib;

public class Vec2<TNumber> : IEquatable<Vec2<TNumber>>,
    IAdditionOperators<Vec2<TNumber>, Vec2<TNumber>, Vec2<TNumber>>,
    IAdditiveIdentity<Vec2<TNumber>, Vec2<TNumber>>,
    ISubtractionOperators<Vec2<TNumber>, Vec2<TNumber>, Vec2<TNumber>>
    where TNumber : INumber<TNumber>
{
    public TNumber X { get; }
    public TNumber Y { get; }

    public Vec2() : this(TNumber.Zero) { }

    public Vec2(TNumber s) : this(s, s) { }

    public Vec2(TNumber x, TNumber y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out TNumber x, out TNumber y)
    {
        x = X;
        y = Y;
    }

    public bool Equals(Vec2<TNumber>? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        
        return EqualityComparer<TNumber>.Default.Equals(X, other.X)
               && EqualityComparer<TNumber>.Default.Equals(Y, other.Y);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((Vec2<TNumber>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }

    public static Vec2<TNumber> Parse(string s)
    {
        var parts = s.Split(',');
        var x = TNumber.Parse(parts[0], null);
        var y = TNumber.Parse(parts[1], null);
        return new Vec2<TNumber>(x, y);
    }

    public IEnumerable<Vec2<TNumber>> AllTo(Vec2<TNumber> end)
    {
        var xStart = X < end.X ? X : end.X;
        var xEnd = X > end.X ? X : end.X;
        var yStart = Y < end.Y ? Y : end.Y;
        var yEnd = Y > end.Y ? Y : end.Y;

        for (var x = xStart; x <= xEnd; x++)
        {
            for (var y = yStart; y <= yEnd; y++)
            {
                yield return new Vec2<TNumber>(x, y);
            }
        }
    }

    public static Vec2<TNumber> operator +(Vec2<TNumber> left, Vec2<TNumber> right)
    {
        return new Vec2<TNumber>(left.X + right.X, left.Y + right.Y);
    }

    public static Vec2<TNumber> AdditiveIdentity { get; } = new(TNumber.AdditiveIdentity);
    
    public static Vec2<TNumber> operator -(Vec2<TNumber> left, Vec2<TNumber> right)
    {
        return new Vec2<TNumber>(left.X - right.X, left.Y - right.Y);
    }
}
