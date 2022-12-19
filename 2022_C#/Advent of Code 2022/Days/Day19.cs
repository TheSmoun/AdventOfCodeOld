using System.Text.RegularExpressions;
using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public partial class Day19 : DayBase<List<Day19.Blueprint>, int>
{
    private static readonly Regex Regex = ParsingRegex();
    
    public override string Name => "Day 19: Not Enough Minerals";

    public override List<Blueprint> ParseInput(IEnumerable<string> lines)
        => lines.Select(Blueprint.Parse).ToList();

    public override int RunPart1(List<Blueprint> input)
        => input.Sum(b => b.Simulate(24) * b.Id);

    public override int RunPart2(List<Blueprint> input)
        => input.Take(3).Select(b => b.Simulate(32)).Product();
    
    public struct State
    {
        public int Time { get; init; } = 0;
        public int[] Resources { get; init; } = new int[4];
        public int[] Robots { get; init; } = { 1, 0, 0, 0 };
        
        public State() { }
    }

    public readonly struct Blueprint
    {
        public required int Id { get; init; }
        public required int[][] Recipes { get; init; }
        public required int[] MaxRobots { get; init; }
        
        public static Blueprint Parse(string s)
        {
            var match = Regex.Match(s);
            var id = match.GetIntValue("id");
            var oreCosts = match.GetIntValue("oreCosts");
            var clayCosts = match.GetIntValue("clayCosts");
            var obsiCosts1 = match.GetIntValue("obsiCosts1");
            var obsiCosts2 = match.GetIntValue("obsiCosts2");
            var geodeCosts1 = match.GetIntValue("geodeCosts1");
            var geodeCosts2 = match.GetIntValue("geodeCosts2");

            var recipes = new[]
            {
                new[] { oreCosts, 0, 0, 0 },
                new[] { clayCosts, 0, 0, 0 },
                new[] { obsiCosts1, obsiCosts2, 0, 0 },
                new[] { geodeCosts1, 0, geodeCosts2, 0 }
            };

            var maxRobots = Enumerable.Range(0, 4).Select(i => recipes.Max(r => r[i])).ToArray();
            maxRobots[3] = int.MaxValue;

            return new Blueprint
            {
                Id = id,
                Recipes = recipes,
                MaxRobots = maxRobots
            };
        }

        public int Simulate(int maxTime)
        {
            var state = new State();
            var maxGeodes = 0;
            Simulate(state, maxTime, ref maxGeodes);
            return maxGeodes;
        }

        private void Simulate(State state, int maxTime, ref int maxGeodes)
        {
            var recursion = false;
            for (var i = 0; i < 4; i++)
            {
                if (state.Robots[i] == MaxRobots[i])
                    continue;
                
                var recipe = Recipes[i];
                var waitTime = -1;

                for (var oreType = 0; oreType < 4; oreType++)
                {
                    if (recipe[oreType] == 0)
                        continue;
                    if (recipe[oreType] <= state.Resources[oreType])
                        waitTime = Math.Max(waitTime, 0);
                    else if (state.Robots[oreType] == 0)
                        waitTime = Math.Max(waitTime, maxTime + 1);
                    else
                        waitTime = Math.Max(waitTime,
                            (recipe[oreType] - state.Resources[oreType] + state.Robots[oreType] - 1) /
                            state.Robots[oreType]);
                }

                var timeFinished = state.Time + waitTime + 1;
                if (timeFinished >= maxTime)
                    continue;

                var newResources = new int[4];
                var newRobots = new int[4];
                for (var oreType = 0; oreType < 4; oreType++)
                {
                    newResources[oreType] = state.Resources[oreType] + state.Robots[oreType] * (waitTime + 1) - recipe[oreType];
                    newRobots[oreType] = state.Robots[oreType] + (oreType == i ? 1 : 0);
                }

                var remainingTime = maxTime - timeFinished;
                if ((remainingTime - 1) * remainingTime / 2 + newResources[3] + remainingTime * newRobots[3] < maxGeodes)
                    continue;

                var newState = new State
                {
                    Time = timeFinished,
                    Resources = newResources,
                    Robots = newRobots
                };

                recursion = true;
                Simulate(newState, maxTime, ref maxGeodes);
            }

            if (!recursion)
            {
                maxGeodes = Math.Max(maxGeodes, state.Resources[3] + state.Robots[3] * (maxTime - state.Time));
            }
        }
    }

    [GeneratedRegex(@"^Blueprint (?<id>\d+): Each ore robot costs (?<oreCosts>\d+) ore\. Each clay robot costs (?<clayCosts>\d+) ore\. Each obsidian robot costs (?<obsiCosts1>\d+) ore and (?<obsiCosts2>\d+) clay\. Each geode robot costs (?<geodeCosts1>\d+) ore and (?<geodeCosts2>\d+) obsidian\.$")]
    private static partial Regex ParsingRegex();
}
