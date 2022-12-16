namespace Advent_of_Code_2022.Lib;

public class Graph<T>
{
    public List<GraphNode<T>> Nodes { get; } = new();
    public List<GraphEdge<T>> Edges { get; } = new();

    public GraphNode<T> GetNode(string nodeName)
    {
        for (var i = 0; i < Nodes.Count; i++)
        {
            var node = Nodes[i];
            if (node.Name == nodeName)
                return node;
        }
        
        return Nodes.First(n => n.Name == nodeName);
    }

    public T GetValue(string nodeName)
    {
        return GetNode(nodeName).Value;
    }
    
    public void AddNode(string name, T value)
    {
        Nodes.Add(new GraphNode<T>(name, value));
    }

    public void AddEdge(string from, string to)
    {
        var fromNode = Nodes.First(n => n.Name == from);
        if (fromNode.Edges.Any(e => e.Target.Name == to || e.Source.Name == to))
            return;
        
        var toNode = Nodes.First(n => n.Name == to);
        if (toNode.Edges.Any(e => e.Target.Name == from || e.Source.Name == from))
            return;

        var edge = new GraphEdge<T>(fromNode, toNode);
        fromNode.Edges.Add(edge);
        toNode.Edges.Add(edge);
        Edges.Add(edge);
    }

    public void RemoveAllNodes(Predicate<GraphNode<T>> predicate)
    {
        var nodesToRemove = Nodes.Where(n => predicate(n)).ToList();
        nodesToRemove.ForEach(RemoveNode);
    }

    public void RemoveNode(GraphNode<T> node)
    {
        Nodes.Remove(node);
        node.Edges.ForEach(e => Edges.Remove(e));
        foreach (var graphNode in Nodes)
        {
            graphNode.Distances.Remove(node.Name);
        }
    }

    public void ComputeAllDistances()
    {
        foreach (var node in Nodes)
        {
            node.Distances = ComputeDistances(node.Name);
        }
    }
    
    private Dictionary<string, int> ComputeDistances(string start)
    {
        var distances = new Dictionary<string, int>();
        var visited = new Dictionary<string, bool>();

        foreach (var node in Nodes)
        {
            distances[node.Name] = int.MaxValue;
            visited[node.Name] = false;
        }

        distances[start] = 0;

        for (var i = 0; i < Nodes.Count; i++)
        {   
            var minDistance = int.MaxValue;
            GraphNode<T> minNode = default!;

            foreach (var node in Nodes)
            {
                if (!visited[node.Name] && distances[node.Name] <= minDistance)
                {
                    minDistance = distances[node.Name];
                    minNode = node;
                }
            }

            visited[minNode.Name] = true;

            foreach (var node in Nodes.Where(n => !visited[n.Name] && n.HasEdgeTo(minNode)))
            {
                if (distances[minNode.Name] != int.MaxValue && distances[minNode.Name] + 1 < distances[node.Name])
                {
                    distances[node.Name] = distances[minNode.Name] + 1;
                }
            }
        }

        return distances;
    }
}
