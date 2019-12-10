using System;
using System.Collections.Generic;

namespace AoC2019.Extensions
{
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Func<TValue> _defaultGetter;

        public DefaultDictionary() { }

        public DefaultDictionary(Func<TValue> defaultGetter)
        {
            _defaultGetter = defaultGetter;
        }

        public new TValue this[TKey key]
        {
            get
            {
                if (_defaultGetter != null && !ContainsKey(key))
                    this[key] = _defaultGetter();
                
                return TryGetValue(key, out var value) ? value : default;
            }
            set => base[key] = value;
        }
    }
}
