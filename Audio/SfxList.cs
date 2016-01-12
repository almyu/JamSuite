using UnityEngine;
using System.Collections.Generic;

namespace JamSuite.Audio {

    public class SfxList : ScriptableObject {

        [System.Serializable]
        public class ClipBinding {
            public string name;
            public AudioClip clip;
        }

        public bool reserveMissing = true;
        public List<ClipBinding> clips;


        public AudioClip LookupClip(string name) {
            foreach (var binding in clips)
                if (binding.name == name)
                    return binding.clip;

            if (reserveMissing)
                clips.Add(new ClipBinding { name = name });

            return null;
        }


#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Create/Sfx List", priority = 220)]
        public static void Create() {
            AssetUtility.CreateAssetInSelectedDirectory(CreateInstance<SfxList>(), "SfxList");
        }
#endif
    }
}
