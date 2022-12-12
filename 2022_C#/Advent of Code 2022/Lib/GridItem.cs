namespace Advent_of_Code_2022.Lib;

public class GridItem<T>
{
    public int Row { get; }
    public int Col { get; }
    public T Value { get; set; }

    public GridItem<T>? Top { get; set; }
    public GridItem<T>? Right { get; set; }
    public GridItem<T>? Bottom { get; set; }
    public GridItem<T>? Left { get; set; }

    public GridItem(int row, int col, T value)
    {
        Row = row;
        Col = col;
        Value = value;
    }

    public IEnumerable<GridItem<T>> Neighbors()
    {
        if (Top is not null) yield return Top;
        if (Right is not null) yield return Right;
        if (Bottom is not null) yield return Bottom;
        if (Left is not null) yield return Left;
    }

    public IEnumerable<GridItem<T>> Tops() => ItemsInDirection(i => i.Top, false);
    public IEnumerable<GridItem<T>> Rights() => ItemsInDirection(i => i.Right, false);
    public IEnumerable<GridItem<T>> Bottoms() => ItemsInDirection(i => i.Bottom, false);
    public IEnumerable<GridItem<T>> Lefts() => ItemsInDirection(i => i.Left, false);

    public IEnumerable<GridItem<T>> ItemsInDirection(Func<GridItem<T>, GridItem<T>?> nextItemSelector,
        bool includeThis)
    {
        var item = this;

        if (includeThis)
        {
            yield return item;
        }
        
        item = nextItemSelector(item);
        
        while (item is not null)
        {
            yield return item;
            item = nextItemSelector(item);
        }
    }

    public bool Equals(GridItem<T>? other)
    {
        return other is not null && Row == other.Row && Col == other.Col;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((GridItem<T>) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }

    public override string ToString()
    {
        return $"[{Row}, {Col}]: {Value}";
    }
}
