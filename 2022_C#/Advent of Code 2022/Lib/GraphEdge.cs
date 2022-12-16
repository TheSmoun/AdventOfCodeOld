namespace Advent_of_Code_2022.Lib;

public class GraphEdge<T>
{
    public GraphNode<T> Source { get; }
    public GraphNode<T> Target { get; }

    public GraphEdge(GraphNode<T> source, GraphNode<T> target)
    {
        Source = source;
        Target = target;
    }
}
