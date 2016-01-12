using UnityEngine;

namespace JamSuite.Persistent {

    public class ShamelessPlug : MonoBehaviour {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Deploy() {
            new GameObject("_ShamelessPlug").AddComponent<ShamelessPlug>().destroyOnStart = true;
        }

        public GameObject payload;
        public bool destroyOnStart;

        private void Start() {
            if (payload) DontDestroyOnLoad(Instantiate(payload));
            if (destroyOnStart) Destroy(gameObject);
        }
    }
}
