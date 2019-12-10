using System.Collections.Generic;

namespace AoC2019.Extensions
{
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get => TryGetValue(key, out var value) ? value : default;
            set => base[key] = value;
        }
    }
}
