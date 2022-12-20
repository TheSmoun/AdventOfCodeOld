namespace Advent_of_Code_2022.Days;

public class Day20 : DayBase<(LinkedList<long>, List<LinkedListNode<long>>), long>
{
    public override string Name => "Day 20: Grove Positioning System";

    public override (LinkedList<long>, List<LinkedListNode<long>>) ParseInput(IEnumerable<string> lines)
    {
        var list = new LinkedList<long>(lines.Select(long.Parse));
        var nodes = new List<LinkedListNode<long>>();

        var node = list.First!;
        do
        {
            nodes.Add(node);
            node = node.Next;
        } while (node is not null);

        return (list, nodes);
    }

    public override long RunPart1((LinkedList<long>, List<LinkedListNode<long>>) input)
    {
        var (list, nodes) = input;
        Mix(list, nodes);
        return FindNumbers(nodes, new HashSet<int> { 1000, 2000, 3000 }).Sum();
    }

    public override long RunPart2((LinkedList<long>, List<LinkedListNode<long>>) input)
    {
        const int decryptionKey = 811589153;
        var (list, nodes) = input;

        foreach (var node in nodes)
        {
            node.Value *= decryptionKey;
        }

        for (var i = 0; i < 10; i++)
        {
            Mix(list, nodes);
        }
        
        return FindNumbers(nodes, new HashSet<int> { 1000, 2000, 3000 }).Sum();
    }

    private static void Mix(LinkedList<long> list, List<LinkedListNode<long>> nodes)
    {
        foreach (var node in nodes)
        {
            var moves = node.Value % (list.Count - 1);
            if (moves < 0)
                MoveForward(list, node, -moves);
            else
                MoveBack(list, node, moves);
        }
    }

    private static void MoveForward(LinkedList<long> list, LinkedListNode<long> node, long amount)
    {
        for (var i = 0L; i < amount; i++)
        {
            var reference = node.Previous ?? node.List!.Last;
            list.Remove(node);
            list.AddBefore(reference!, node);
        }
    }

    private static void MoveBack(LinkedList<long> list, LinkedListNode<long> node, long amount)
    {
        for (var i = 0L; i < amount; i++)
        {
            var reference = node.Next ?? node.List!.First;
            list.Remove(node);
            list.AddAfter(reference!, node);
        }
    }

    private static IEnumerable<long> FindNumbers(List<LinkedListNode<long>> nodes, HashSet<int> searchIndices)
    {
        var node = nodes.First(n => n.Value == 0);
        var max = searchIndices.Max();

        for (var i = 0; i <= max; i++)
        {
            if (searchIndices.Contains(i))
                yield return node.Value;
            
            node = node.Next ?? node.List!.First!;
        }
    }
}
