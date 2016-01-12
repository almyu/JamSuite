using UnityEngine;
using System.Collections.Generic;

namespace JamSuite.Audio {

    [System.Serializable]
    public class Playlist {
        public string name;
        //public bool shuffle = true;
        public List<AudioClip> tracks = new List<AudioClip>();

        public AudioClip GetNextTrack(AudioClip current) {
            return tracks.Count != 0
                ? tracks[(tracks.IndexOf(current) + 1) % tracks.Count]
                : null;
        }
    }

    public class BgmList : ScriptableObject {

        public bool reserveMissing = true;
        public List<Playlist> playlists;


        public Playlist LookupPlaylist(string name) {
            foreach (var playlist in playlists)
                if (playlist.name == name)
                    return playlist;

            if (!reserveMissing) return null;

            var newPlaylist = new Playlist { name = name };
            playlists.Add(newPlaylist);

            return newPlaylist;
        }


#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/Create/Bgm List", priority = 220)]
        public static void Create() {
            AssetUtility.CreateAssetInSelectedDirectory(CreateInstance<BgmList>(), "BgmList");
        }
#endif
    }
}
