namespace Advent_of_Code_2022.Extensions;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly Func<TValue>? _defaultGetter;
    private readonly bool _add;

    public DefaultDictionary() { }

    public DefaultDictionary(Func<TValue>? defaultGetter, bool add = true)
    {
        _defaultGetter = defaultGetter;
        _add = add;
    }

    public new TValue? this[TKey key]
    {
        get
        {
            var defaultValue = _defaultGetter != null ? _defaultGetter() : default;

            if (_add && !ContainsKey(key))
                this[key] = defaultValue;
                
            return TryGetValue(key, out var value) ? value : defaultValue;
        }
        set => base[key] = value!;
    }
}
