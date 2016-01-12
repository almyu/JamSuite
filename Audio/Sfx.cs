using UnityEngine;

namespace JamSuite.Audio {

    public class Sfx : MonoBehaviour {

        public static void Play(string clipName, float volumeScale = 1f) {
            var player = SfxPlayer.instance;
            if (player) player.Play(clipName, volumeScale);
        }


        public enum Mode {
            OnAwake,
            OnStart,
            OnEnable
        }

        public Mode mode;
        public string clipName;


        private void Awake() {
            if (mode == Mode.OnAwake) Play(clipName);
        }

        private void Start() {
            if (mode == Mode.OnStart) Play(clipName);
        }

        private void OnEnable() {
            if (mode == Mode.OnEnable) Play(clipName);
        }
    }
}
