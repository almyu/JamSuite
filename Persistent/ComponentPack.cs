using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace JamSuite.Persistent {

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentPack : Attribute {

        public static readonly Type[] all = ReflectionUtility.GetScriptsWithAttribute<ComponentPack>(true).ToArray();

        public static IEnumerable<Type> Filter<T>() where T : ComponentPack {
            return all.Where(t => Attribute.IsDefined(t, typeof(T), true));
        }
    }


    public static class GameObjectComponentPackExt {

        public static List<Component> Add<T>(this GameObject obj) where T : ComponentPack {
            return ComponentPack.Filter<T>().Select(t => obj.AddComponent(t)).ToList();
        }
    }


    public static class ReflectionUtility {

        public static readonly Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        public static readonly Type[] scripts = assemblies
            .SelectMany(asm => asm.GetTypes())
            .Where(t => typeof(MonoBehaviour).IsAssignableFrom(t))
            .ToArray();

        public static IEnumerable<Type> GetScriptsWithAttribute<T>(bool inherit) where T : Attribute {
            return scripts.Where(t => Attribute.IsDefined(t, typeof(T), inherit));
        }
    }
}
