using UnityEngine;

namespace JamSuite.Persistent {

    public static class AutoSpawn {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SpawnAll() {
            foreach (var prefab in Resources.LoadAll<GameObject>("AutoSpawn"))
                GameObject.DontDestroyOnLoad(GameObject.Instantiate(prefab));
        }
    }
}
