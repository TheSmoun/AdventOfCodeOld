namespace Advent_of_Code_2022.Lib;

public class GraphNode<T>
{
    public string Name { get; }
    public T Value { get; }
    public List<GraphEdge<T>> Edges { get; }
    public Dictionary<string, int> Distances { get; set; }

    public GraphNode(string name, T value)
    {
        Name = name;
        Value = value;
        Edges = new List<GraphEdge<T>>();
        Distances = new Dictionary<string, int>();
    }

    public bool HasEdgeTo(GraphNode<T> other)
    {
        return Edges.Any(e => e.Target == other || e.Source == other);
    }
}
