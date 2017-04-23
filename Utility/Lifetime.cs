/// Version: 2017-03-30

using UnityEngine;

namespace JamSuite
{
    public sealed class Lifetime : MonoBehaviour
    {
        public float lifetime = 1f;

        public void InferFromParticles() {
            lifetime = 1f;

            foreach (var ps in GetComponentsInChildren<ParticleSystem>()) {
                var time = ps.main.duration + ps.main.startLifetime.constantMax;
                if (lifetime < time) lifetime = time;
            }
        }

        void Reset() {
            InferFromParticles();
        }

        void Start() {
            Destroy(gameObject, lifetime);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(Lifetime))]
    [UnityEditor.CanEditMultipleObjects]
    public class LifetimeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (GUILayout.Button("Infer From Particles"))
                foreach (Lifetime target in targets)
                    target.InferFromParticles();
        }
    }
#endif
}
