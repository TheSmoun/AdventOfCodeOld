using System.Text.RegularExpressions;

namespace Advent_of_Code_2022.Extensions;

public static class MatchExtensions
{
    public static string GetValue(this Match match, string groupName)
        => match.Groups[groupName].Value;

    public static int GetIntValue(this Match match, string groupName)
        => int.Parse(match.GetValue(groupName));
}
