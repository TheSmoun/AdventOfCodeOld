using System.Numerics;

namespace Advent_of_Code_2022.Extensions;

public static class NumberEx
{
    public static (TNumber, TNumber) DiffMod<TNumber>(this TNumber a, TNumber b)
        where TNumber : INumber<TNumber>, IModulusOperators<TNumber, TNumber, TNumber>
    {
        return (a / b, a.Mod(b));
    }
    
    public static TNumber Gcd<TNumber>(this TNumber a, TNumber b)
        where TNumber : INumber<TNumber>
    {
        while (true)
        {
            if (a == TNumber.Zero)
                return b;
            var a1 = a;
            a = b % a;
            b = a1;
        }
    }

    public static TNumber Mod<TNumber>(this TNumber input, TNumber mod)
        where TNumber : IModulusOperators<TNumber, TNumber, TNumber>, IAdditionOperators<TNumber, TNumber, TNumber>
    {
        return (input % mod + mod) % mod;
    }
}
