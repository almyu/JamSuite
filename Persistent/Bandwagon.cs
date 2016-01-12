using UnityEngine;
using System;
using System.Linq;

namespace JamSuite.Persistent {

    public static class Bandwagon {

        [AttributeUsage(AttributeTargets.Class)]
        public class Jumper : Attribute {}

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Jump() {
            var scripts = ReflectionUtility.GetScriptsWithAttribute<Jumper>(false).ToArray();
            if (scripts.Length != 0) new GameObject("_Bandwagon", scripts);
        }
    }
}
