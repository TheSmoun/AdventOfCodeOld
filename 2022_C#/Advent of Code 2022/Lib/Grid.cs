﻿namespace Advent_of_Code_2022.Lib;

public class Grid<T>
{
    public int RowCount { get; }
    public int ColCount { get; }
    
    private readonly GridItem<T>[,] _items;
    
    public Grid(int rows, int cols)
    {
        RowCount = rows;
        ColCount = cols;
        
        _items = new GridItem<T>[rows, cols];

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                _items[r, c] = new GridItem<T>(r, c, default!);
            }
        }

        if (rows > 1)
        {
            for (var r = 1; r < rows - 1; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    var top = _items[r - 1, c];
                    var bottom = _items[r + 1, c];
                    var item = _items[r, c];
                    
                    item.Top = top;
                    top.Bottom = item;
                    
                    item.Bottom = bottom;
                    bottom.Top = item;
                }
            }
        }

        if (cols > 1)
        {
            for (var r = 0; r < rows; r++)
            {
                for (var c = 1; c < cols - 1; c++)
                {
                    var left = _items[r, c - 1];
                    var right = _items[r, c + 1];
                    var item = _items[r, c];

                    item.Left = left;
                    left.Right = item;

                    item.Right = right;
                    right.Left = item;
                }
            }
        }
    }

    public T this[int row, int col]
    {
        get => _items[row, col].Value;
        set => _items[row, col].Value = value;
    }
    
    public IEnumerable<GridItem<T>> All()
    {
        for (var r = 0; r < RowCount; r++)
        {
            for (var c = 0; c < ColCount; c++)
            {
                yield return _items[r, c];
            }
        }
    }

    public IEnumerable<GridItem<T>> Inner()
    {
        for (var r = 1; r < RowCount - 1; r++)
        {
            for (var c = 1; c < ColCount - 1; c++)
            {
                yield return _items[r, c];
            }
        }
    }

    public IEnumerable<GridItem<T>> Column(int col)
    {
        var item = _items[0, col];
        return item.ItemsInDirection(i => i.Bottom, true);
    }

    public IEnumerable<GridItem<T>> ColumnReversed(int col)
    {
        var item = _items[RowCount - 1, col];
        return item.ItemsInDirection(i => i.Top, true);
    }

    public IEnumerable<GridItem<T>> Row(int row)
    {
        var item = _items[row, 0];
        return item.ItemsInDirection(i => i.Right, true);
    }

    public IEnumerable<GridItem<T>> RowReversed(int row)
    {
        var item = _items[row, ColCount - 1];
        return item.ItemsInDirection(i => i.Left, true);
    }

    public class GridItem<TValue>
    {
        public int Row { get; }
        public int Col { get; }
        public TValue Value { get; set; }

        public GridItem<TValue>? Top { get; set; }
        public GridItem<TValue>? Right { get; set; }
        public GridItem<TValue>? Bottom { get; set; }
        public GridItem<TValue>? Left { get; set; }

        public GridItem(int row, int col, TValue value)
        {
            Row = row;
            Col = col;
            Value = value;
        }

        public IEnumerable<GridItem<TValue>> Tops() => ItemsInDirection(i => i.Top, false);
        public IEnumerable<GridItem<TValue>> Rights() => ItemsInDirection(i => i.Right, false);
        public IEnumerable<GridItem<TValue>> Bottoms() => ItemsInDirection(i => i.Bottom, false);
        public IEnumerable<GridItem<TValue>> Lefts() => ItemsInDirection(i => i.Left, false);

        public IEnumerable<GridItem<TValue>> ItemsInDirection(Func<GridItem<TValue>, GridItem<TValue>?> nextItemSelector,
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

        public bool Equals(GridItem<TValue>? other)
        {
            return other is not null && Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((GridItem<TValue>) obj);
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
}
