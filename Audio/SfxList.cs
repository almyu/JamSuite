/// Version: 2017-03-30

using System.Collections.Generic;
using UnityEngine;

namespace JamSuite.Audio
{
    [CreateAssetMenu(order = 220)]
    public class SfxList : ScriptableObject
    {
        [System.Serializable]
        public class ClipBinding
        {
            public string name;

            [Range(0f, 4f)]
            public float volumeScale = 1f;

            public AudioClip[] variants;

            [System.NonSerialized]
            public float lastPlay;
        }

        public bool reserveMissing = true;
        public List<ClipBinding> clips;


        public ClipBinding Lookup(string name) {
            foreach (var binding in clips)
                if (binding.name == name)
                    return binding;

            if (reserveMissing) {
                clips.Add(new ClipBinding { name = name, variants = new AudioClip[0] });

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            return null;
        }
    }
}
