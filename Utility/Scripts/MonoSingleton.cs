using UnityEngine;

namespace UnityEngine {

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

        public static T instance {
            get {
                if (!_instance)
                    _instance = FindObjectOfType<T>();

                return _instance;
            }
        }

        protected static T _instance;
    }
}
