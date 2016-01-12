// #define DEBUG_BGM

using UnityEngine;
using System.Collections.Generic;

namespace JamSuite.Audio {

    [RequireComponent(typeof(AudioSource))]
    public class BgmPlayer : MonoSingleton<BgmPlayer> {

        public float crossfadeTime = 1f;
        public AnimationCurve crossfadeShape = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public BgmList playlists;

        private AudioSource source;

        private class Snapshot {
            public AudioClip track;
            public float trackTime;
        }

        private Dictionary<Playlist, Snapshot> snapshots = new Dictionary<Playlist, Snapshot>();
        private List<Playlist> stack = new List<Playlist>();

        public Playlist activePlaylist {
            get { return stack.Count != 0 ? stack[stack.Count - 1] : null; }
        }


        private void OnValidate() {
            if (!playlists) {
                var lists = Resources.FindObjectsOfTypeAll<BgmList>();
                if (lists.Length > 0)
                    playlists = lists[0];
            }
        }

        private void Awake() {
            if (instance && instance != this) {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
        }

        private void Update() {
            if (stack.Count == 0) return;
            if (source.clip == null) return;

            if (source.time + crossfadeTime >= source.clip.length) {
                var playlist = stack[stack.Count - 1];

                var snapshot = GetSnapshot(playlist);
                snapshot.track = playlist.GetNextTrack(snapshot.track);
                snapshot.trackTime = 0f;

                Play(snapshot);
            }
        }


        public void PushPlaylist(string playlistName) {
            PushPlaylist(playlists.LookupPlaylist(playlistName));
        }

        public void PopPlaylist(string playlistName) {
            PopPlaylist(playlists.LookupPlaylist(playlistName));
        }


        public void PushPlaylist(Playlist playlist) {
            ChangePlaylist(activePlaylist, playlist);
            stack.Add(playlist);
        }

        public void PopPlaylist(Playlist playlist) {
            var index = stack.LastIndexOf(playlist);
            if (index == -1) return;

            stack.RemoveAt(index);

            if (index == stack.Count) // was active
                ChangePlaylist(playlist, activePlaylist);
        }

        private void ChangePlaylist(Playlist from, Playlist to) {
            if (from != null) {
                var snapshot = GetSnapshot(from);
                snapshot.track = source.clip;
                snapshot.trackTime = source.time;
            }
            if (to != null) {
                var snapshot = GetSnapshot(to);

#if DEBUG_BGM
                Debug.LogFormat("Now playing: {0} — {1} at {2:c}",
                    to.name, snapshot.track.name,
                    System.TimeSpan.FromSeconds(snapshot.trackTime));
#endif

                Play(snapshot);
            }
            else {
#if DEBUG_BGM
                Debug.LogFormat("Now playing: silence");
#endif
                Stop();
            }
        }

        private void Play(Snapshot snapshot) {
            source = source.Crossfade(snapshot.track, crossfadeTime, crossfadeShape);
            source.time = snapshot.trackTime;
        }

        private void Stop() {
            source = source.Crossfade(default(AudioClip), crossfadeTime, crossfadeShape);
        }

        private Snapshot GetSnapshot(Playlist playlist) {
            var snapshot = default(Snapshot);
            if (snapshots.TryGetValue(playlist, out snapshot)) return snapshot;

            snapshot = new Snapshot {
                track = playlist.GetNextTrack(null),
                trackTime = 0f
            };
            snapshots.Add(playlist, snapshot);

            return snapshot;
        }
    }
}
