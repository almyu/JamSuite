using UnityEngine;

namespace JamSuite.Audio {

    public class Sfx : MonoBehaviour {

        public static void Play(string clipName, float volumeScale = 1f) {
            var player = SfxPlayer.instance;
            if (player) player.Play(clipName, volumeScale);
        }

        public static void Play(string clipName, Vector3 position, float volumeScale = 1f) {
            var player = SfxPlayer.instance;
            if (player) player.Play(clipName, position, volumeScale);
        }


        public enum Mode {
            OnAwake,
            OnStart,
            OnEnable
        }

        public Mode mode;
        public bool playAtPosition;
        public string clipName;


        private void Awake() {
            if (mode == Mode.OnAwake) TriggerPlay();
        }

        private void Start() {
            if (mode == Mode.OnStart) TriggerPlay();
        }

        private void OnEnable() {
            if (mode == Mode.OnEnable) TriggerPlay();
        }

        private void TriggerPlay() {
            if (playAtPosition) Play(clipName, transform.position);
            else Play(clipName);
        }
    }
}
