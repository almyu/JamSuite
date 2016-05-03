using UnityEngine;
using System.Collections.Generic;
using System.Text;

public static class ListExt {

    public static T Roll<T>(this IList<T> list) {
        return list.Count != 0 ? list[Random.Range(0, list.Count)] : default(T);
    }

    public static void Shuffle<T>(this IList<T> list) {
        for (int i = list.Count; --i > 0; ) {
            var randomIndex = Random.Range(0, i + 1);
            var tmp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = tmp;
        }
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

    public static string ToString<T>(this IEnumerable<T> seq, string separator, string prefix = "", string suffix = "") {
        var str = new StringBuilder(prefix);
        var first = true;

        foreach (var e in seq) {
            if (first) first = false;
            else str.Append(separator);

            str.Append(e);
        }
        return str.Append(suffix).ToString();
    }
}
