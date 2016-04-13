using UnityEngine;
using System.Collections.Generic;

namespace JamSuite.Audio {

    [RequireComponent(typeof(AudioSource))]
    public class SfxPlayer : MonoSingleton<SfxPlayer> {

        public AudioSource source;
        public SfxList list;
        public float throttle = 0.1f;

        private Dictionary<AudioClip, float> lastPlays = new Dictionary<AudioClip, float>();


        private void OnValidate() {
            if (!list) {
                var lists = Resources.FindObjectsOfTypeAll<SfxList>();
                if (lists.Length > 0)
                    list = lists[0];
            }
            if (!source) source = GetComponent<AudioSource>();
        }


        public void Play(string clipName) {
            Play(clipName, 1f);
        }

        public void Play(string clipName, float volumeScale) {
            var clip = TryPlaying(clipName);
            if (clip) source.PlayOneShot(clip, volumeScale);
        }

        public void Play(string clipName, Vector3 position) {
            Play(clipName, position, 1f);
        }

        public void Play(string clipName, Vector3 position, float volumeScale) {
            var clip = TryPlaying(clipName);
            if (clip) AudioSource.PlayClipAtPoint(clip, position, source.volume * volumeScale);
        }

        private AudioClip TryPlaying(string clipName) {
            var clip = list.LookupClip(clipName);
            if (!clip) return null;

            var lastPlay = 0f;
            var everPlayed = lastPlays.TryGetValue(clip, out lastPlay);

            if (lastPlay + throttle > Time.timeSinceLevelLoad) return null;

            if (everPlayed) lastPlays[clip] = Time.timeSinceLevelLoad;
            else lastPlays.Add(clip, Time.timeSinceLevelLoad);

            return clip;
        }
    }
}
