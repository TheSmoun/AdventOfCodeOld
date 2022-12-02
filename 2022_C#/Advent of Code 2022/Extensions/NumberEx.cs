using System.Numerics;

namespace Advent_of_Code_2022.Extensions;

public static class NumberEx
{
    public static TNumber Mod<TNumber>(this TNumber input, TNumber mod)
        where TNumber : IModulusOperators<TNumber, TNumber, TNumber>, IAdditionOperators<TNumber, TNumber, TNumber>
    {
        return (input % mod + mod) % mod;
    }
}
