using System.Text.RegularExpressions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public partial class Day16 : DayBase<Day16.SearchOptimizedGraph, int>
{
    private static readonly Regex Regex = ParsingRegex();
    
    public override string Name => "Day 16: Proboscidea Volcanium";
    
    public override SearchOptimizedGraph ParseInput(IEnumerable<string> lines)
    {
        var graph = new Graph<int>();
        var matches = lines.Select(l => Regex.Match(l)).ToList();

        foreach (var match in matches)
        {
            var name = match.Groups["name"].Value;
            var flowRate = int.Parse(match.Groups["rate"].Value);
            graph.AddNode(name, flowRate);
        }

        foreach (var match in matches)
        {
            var name = match.Groups["name"].Value;
            var targets = match.Groups["targets"].Value.Split(", ");
            foreach (var target in targets)
            {
                graph.AddEdge(name, target);
            }
        }
        
        graph.ComputeAllDistances();
        graph.RemoveAllNodes(n => n.Value == 0 && n.Name != "AA");

        return new SearchOptimizedGraph(graph);
    }

    public override int RunPart1(SearchOptimizedGraph input)
    {
        var score = 0;
        Dfs(ref score, 0, input, "AA", new HashSet<string>(), 0, 30, false);
        return score;
    }

    public override int RunPart2(SearchOptimizedGraph input)
    {
        var score = 0;
        Dfs(ref score, 0, input, "AA", new HashSet<string>(), 0, 26, true);
        return score;
    }

    private static void Dfs(ref int score, int currentScore, SearchOptimizedGraph graph, string node,
        HashSet<string> visited, int time, int totalTime, bool withElephant)
    {
        score = Math.Max(score, currentScore);
        foreach (var (nodeName, distance) in graph.Distances[node])
        {
            if (!visited.Contains(nodeName) && time + distance + 1 < totalTime)
            {
                Dfs(ref score, currentScore + (totalTime - time - distance - 1) * graph.Values[nodeName],
                    graph, nodeName, new HashSet<string>(visited) { nodeName },
                    time + distance + 1, totalTime, withElephant);
            }
        }

        if (withElephant)
        {
            Dfs(ref score, currentScore, graph, "AA", visited, 0, 26, false);
        }
    }
    
    public class SearchOptimizedGraph
    {
        public Dictionary<string, int> Values { get; }
        public Dictionary<string, (string, int)[]> Distances { get; }

        public SearchOptimizedGraph(Graph<int> graph)
        {
            Values = graph.Nodes.ToDictionary(n => n.Name, n => n.Value);
            Distances = graph.Nodes.ToDictionary(n => n.Name, n => n.Distances
                .Select(kvp => (kvp.Key, kvp.Value)).ToArray());
        }
    }

    [GeneratedRegex(@"^Valve (?<name>[A-Z]{2}) has flow rate=(?<rate>\d+); tunnel[s]{0,1} lead[s]{0,1} to valve[s]{0,1} (?<targets>.+)$")]
    private static partial Regex ParsingRegex();
}
