using UnityEngine;

namespace JamSuite.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxPlayer : MonoSingleton<SfxPlayer>
    {
        public AudioSource source;
        public SfxList list;
        public float throttle = 0.1f;


        private void Reset() {
            source = GetComponent<AudioSource>();

            var lists = Resources.FindObjectsOfTypeAll<SfxList>();
            if (lists.Length > 0) list = lists[0];
        }


        public void Play(string clipName) {
            Play(clipName, 1f);
        }

        public void Play(string clipName, float volumeScale) {
            var clip = TryPlaying(clipName, ref volumeScale);
            if (clip) source.PlayOneShot(clip, volumeScale);
        }

        public void Play(string clipName, Vector3 position) {
            Play(clipName, position, 1f);
        }

        public void Play(string clipName, Vector3 position, float volumeScale) {
            var clip = TryPlaying(clipName, ref volumeScale);
            if (clip) AudioSource.PlayClipAtPoint(clip, position, source.volume * volumeScale);
        }

        private AudioClip TryPlaying(string clipName, ref float volumeScale) {
            var binding = list.Lookup(clipName);
            if (binding == null) return null;

            if (binding.lastPlay + throttle > Time.unscaledTime) return null;
            binding.lastPlay = Time.unscaledTime;

            volumeScale *= binding.volumeScale;
            return binding.variants.Roll();
        }
    }
}
