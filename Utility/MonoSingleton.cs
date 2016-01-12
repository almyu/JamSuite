namespace UnityEngine {

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

        public static T instance {
            get { return JamSuite.SingletonHelper<T>.instance; }
        }
    }
}

namespace JamSuite {

    public class SingletonHelper<T> where T : UnityEngine.Object {

        public static T instance {
            get {
                if (!_instance) _instance = UnityEngine.Object.FindObjectOfType<T>();
                return _instance;
            }
        }

        private static T _instance;
    }
}
