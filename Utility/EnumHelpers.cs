using System;
using System.Linq;

namespace JamSuite
{
    public static class Enum<E>
    {
        public static readonly string[] names = Enum.GetNames(typeof(E));
        public static readonly E[] values = Enum.GetValues(typeof(E)).Cast<E>().ToArray();
        public static readonly int length = names.Length;

        public static T[] ByIndex<T>(Func<int, T> pred) {
            return Enumerable.Range(0, length).Select(pred).ToArray();
        }

        public static T[] ByValue<T>(Func<E, T> pred) {
            return values.Select(pred).ToArray();
        }

        public static T[] ByName<T>(Func<string, T> pred) {
            return names.Select(pred).ToArray();
        }
    }

    public static class EnumExt
    {
        public static T Dispatch<T>(this Enum value, params T[] options) {
            return options[Convert.ToInt32(value)];
        }
    }
}
