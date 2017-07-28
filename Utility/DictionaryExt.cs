/// Version: 2017-07-21

using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace JamSuite
{
    public static partial class DictionaryExt
    {
        public static V EnsureValue<K, V>(this IDictionary<K, V> dict, K key, Func<V> factory) {
            V value;
            if (!dict.TryGetValue(key, out value))
                dict.Add(key, value = factory());

            return value;
        }
        public static V EnsureValue<K, V>(this IDictionary<K, V> dict, K key, Func<K, V> factory) {
            return dict.EnsureValue(key, () => factory(key));
        }
        public static V EnsureValue<K, V>(this IDictionary<K, V> dict, K key) where V : new() {
            return dict.EnsureValue(key, () => new V());
        }

        public static V EnsureObject<K, V>(this IDictionary<K, V> dict, K key, Func<V> factory) where V : Object {
            V value;
            if (!dict.TryGetValue(key, out value))
                dict.Add(key, value = factory());

            if (!value)
                dict[key] = value = factory();

            return value;
        }
        public static V EnsureObject<K, V>(this IDictionary<K, V> dict, K key, Func<K, V> factory) where V : Object {
            return dict.EnsureObject(key, () => factory(key));
        }
    }
}
