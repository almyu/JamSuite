/// Version: 2017-07-28

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace JamSuite
{
    public static class ListExt
    {
        public static T Roll<T>(this IList<T> list) {
            return list.Count != 0 ? list[Random.Range(0, list.Count)] : default(T);
        }

        public static T Roll<T>(this IEnumerable<T> seq, Func<T, float> weightSelector) {
            var roll = Random.value * seq.Sum(weightSelector);

            foreach (var e in seq) {
                var weight = weightSelector(e);
                if (weight > roll) return e;

                roll -= weight;
            }
            return default(T);
        }

        public static void Sort<T, Key>(this T[] list, Func<T, Key> keySelector) where Key : IComparable<Key> {
            Array.Sort(list, (a, b) => keySelector(b).CompareTo(keySelector(a)));
        } 
        public static void Sort<T, Key>(this List<T> list, Func<T, Key> keySelector) where Key : IComparable<Key> {
            list.Sort((a, b) => keySelector(b).CompareTo(keySelector(a)));
        }

        public static void Shuffle<T>(this IList<T> list) {
            for (int i = list.Count; --i > 0; ) {
                var randomIndex = Random.Range(0, i + 1);
                var tmp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = tmp;
            }
        }

        public static bool AddUnique<T>(this IList<T> list, T value) {
            if (list.Contains(value)) return false;

            list.Add(value);
            return true;
        }

        public static T RemoveLast<T>(this IList<T> list) {
            var lastIndex = list.Count - 1;
            var value = list[lastIndex];

            list.RemoveAt(lastIndex);
            return value;
        }

        public static void SwapRemoveAt<T>(this IList<T> list, int index) {
            list[index] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }

        public static bool SwapRemove<T>(this IList<T> list, T item) {
            var index = list.IndexOf(item);
            if (index == -1) return false;

            list.SwapRemoveAt(index);
            return true;
        }

        public static T FindByName<T>(this IEnumerable<T> seq, string name, bool ignoreCase = false) where T : Object {
            var cmp = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            foreach (var obj in seq)
                if (obj && obj.name.Equals(name, cmp))
                    return obj;

            return null;
        }

        public static T FindBest<T>(this IEnumerable<T> seq, Func<T, float> qualitySelector) {
            var best = default(T);
            var bestQuality = float.MinValue;

            foreach (var e in seq) {
                var quality = qualitySelector(e);
                if (quality <= bestQuality) continue;

                bestQuality = quality;
                best = e;
            }
            return best;
        }

        public static T FindWorst<T>(this IEnumerable<T> seq, Func<T, float> qualitySelector) {
            return FindBest(seq, e => -qualitySelector(e));
        }

        public static T FindClosest<T>(this IEnumerable<T> seq, Vector3 point) where T : Component {
            return FindBest(seq, e => -(point - e.transform.position).sqrMagnitude);
        }

        public static string ToString<T>(this IEnumerable<T> seq, string separator, string prefix = "", string suffix = "") {
            var it = seq.GetEnumerator();
            if (!it.MoveNext()) return prefix + suffix;

            var str = new StringBuilder(prefix).Append(it.Current);

            while (it.MoveNext())
                str.Append(separator).Append(it.Current);

            return str.Append(suffix).ToString();
        }
    }
}
