using UnityEngine;

namespace JamSuite.Audio {

    public class Bgm : MonoBehaviour {

        public static void PushPlaylist(string playlistName) {
            var player = BgmPlayer.instance;
            if (player) player.PushPlaylist(playlistName);
        }

        public static void PopPlaylist(string playlistName) {
            var player = BgmPlayer.instance;
            if (player) player.PopPlaylist(playlistName);
        }


        public string playlistName;

        private void OnEnable() {
            PushPlaylist(playlistName);
        }

        private void OnDisable() {
            PopPlaylist(playlistName);
        }
    }
}
