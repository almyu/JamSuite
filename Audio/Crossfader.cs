using UnityEngine;

namespace JamSuite.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Crossfader : MonoBehaviour
    {
        public const float defaultDuration = 1f;
        public static readonly AnimationCurve defaultShape = AnimationCurve.Linear(0f, 0f, 1f, 1f);


        public AudioSource ownSource;
        public AudioSource targetSource;

        public float duration = defaultDuration;
        public AnimationCurve shape = defaultShape;

        public bool fadeOnStart = false;

        public bool destroyGameObject = false;
        public bool destroySource = false;
        public bool destroySelf = false;
        public bool stopPlayback = true;

        private float timer;
        private float refVolume;


        private void Update() {
            if (!ownSource || !targetSource) return;

            timer -= Time.unscaledDeltaTime;

            if (timer <= 0f) End();
            else {
                var targetVolume = shape.Evaluate(1f - Mathf.Clamp01(timer / duration));

                targetSource.volume = targetVolume;
                ownSource.volume = refVolume * (1f - targetVolume);
            }
        }

        private void Start() {
            if (fadeOnStart) Crossfade();
        }


        public void Crossfade() {
            timer = duration;
            refVolume = ownSource.volume;

            targetSource.volume = 0f;

            if (!targetSource.isPlaying)
                targetSource.Play();
        }

        public void Crossfade(AudioSource from, AudioSource to) {
            ownSource = from;
            targetSource = to;

            Crossfade();
        }

        public AudioSource Crossfade(AudioSource source, AudioClip clip) {
            ownSource = source;
            SpawnTarget(ownSource.gameObject).clip = clip;

            Crossfade();

            return targetSource;
        }

        public AudioSource SpawnTarget(GameObject targetGameObject) {
            targetSource = targetGameObject.AddComponent<AudioSource>();
            CrossfadeUtility.CopyAudioSourceProperties(ownSource, targetSource);

            return targetSource;
        }

        public void End() {
            ownSource.volume = 0f;
            targetSource.volume = 1f;

            if (destroyGameObject) Destroy(gameObject);
            else {
                if (destroySource) Destroy(ownSource);
                else if (stopPlayback) ownSource.Stop();

                if (destroySelf) Destroy(this);
            }

            ownSource = null;
            targetSource = null;
        }
    }


    public static class AudioSourceExt
    {
        public static Crossfader Crossfade(this AudioSource from, AudioSource to) {
            return Crossfade(from, to, Crossfader.defaultDuration, Crossfader.defaultShape);
        }

        public static Crossfader Crossfade(this AudioSource from, AudioSource to, float time) {
            return Crossfade(from, to, time, Crossfader.defaultShape);
        }

        public static Crossfader Crossfade(this AudioSource from, AudioSource to, float time, AnimationCurve shape) {
            var fader = from.gameObject.AddComponent<Crossfader>();
            fader.duration = time;
            fader.shape = shape;
            fader.Crossfade(from, to);

            return fader;
        }


        public static AudioSource Crossfade(this AudioSource source, AudioClip clip) {
            return Crossfade(source, clip, Crossfader.defaultDuration, Crossfader.defaultShape);
        }

        public static AudioSource Crossfade(this AudioSource source, AudioClip clip, float time) {
            return Crossfade(source, clip, time, Crossfader.defaultShape);
        }

        public static AudioSource Crossfade(this AudioSource source, AudioClip clip, float time, AnimationCurve shape) {
            var fader = source.gameObject.AddComponent<Crossfader>();
            fader.destroySource = true;
            fader.destroySelf = true;

            fader.duration = time;
            fader.shape = shape;
            fader.Crossfade(source, clip);

            return fader.targetSource;
        }
    }


    public static class CrossfadeUtility
    {
        public static void CopyAudioSourceProperties(AudioSource from, AudioSource to, bool thoroughly = true) {
            if (thoroughly) {
                to.bypassEffects = from.bypassEffects;
                to.bypassListenerEffects = from.bypassListenerEffects;
                to.bypassReverbZones = from.bypassReverbZones;
                to.dopplerLevel = from.dopplerLevel;
                to.ignoreListenerPause = from.ignoreListenerPause;
                to.ignoreListenerVolume = from.ignoreListenerVolume;
                to.reverbZoneMix = from.reverbZoneMix;
                to.rolloffMode = from.rolloffMode;
                to.minDistance = from.minDistance;
                to.maxDistance = from.maxDistance;
                to.spread = from.spread;
                to.velocityUpdateMode = from.velocityUpdateMode;
            }

            to.outputAudioMixerGroup = from.outputAudioMixerGroup;

            to.volume = from.volume;
            to.mute = from.mute;
            to.loop = from.loop;
            to.priority = from.priority;
            to.pitch = from.pitch;
            to.panStereo = from.panStereo;
            to.spatialBlend = from.spatialBlend;
        }
    }
}
