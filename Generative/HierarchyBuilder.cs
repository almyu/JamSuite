using UnityEngine;

namespace JamSuite.Generative {

    public enum HierarchyRebuildMode {
        Manual = 0,
        Awake = 1,
        EditorUpdate = 2,
        RuntimeUpdate = 4,
        AnyUpdate = EditorUpdate | RuntimeUpdate,
    }

    [ExecuteInEditMode]
    public abstract class HierarchyBuilder<T> : MonoBehaviour {

        public Transform template;
        public HierarchyRebuildMode rebuildMode = HierarchyRebuildMode.Awake;

        protected Transform parent, lastSpawn;


        public void Rebuild() {
            for (int i = transform.childCount; i-- > 0; ) {
                var child = transform.GetChild(i);
                if (child != template) DestroyImmediate(child.gameObject);
            }
            Build();
        }


        protected abstract void Build();

        protected virtual void Tune(Transform spawn, T data) {}


        protected Transform Spawn(T data) {
            lastSpawn = Instantiate(template);
            lastSpawn.SetParent(parent ? parent : transform, false);
            Tune(lastSpawn, data);
            lastSpawn.gameObject.SetActive(true);
            return lastSpawn;
        }

        protected void Descend() {
            parent = lastSpawn;
        }

        protected void Ascend() {
            if (parent != transform)
                parent = parent ? parent.parent : transform;
        }

        protected void AscendToTop() {
            parent = transform;
        }


        protected virtual void Awake() {
            if (!Application.isPlaying) return;
            if ((rebuildMode & HierarchyRebuildMode.Awake) != 0) Rebuild();
        }

        protected virtual void Update() {
            var modeBit = Application.isPlaying
                ? HierarchyRebuildMode.RuntimeUpdate
                : HierarchyRebuildMode.EditorUpdate;

            if ((rebuildMode & modeBit) != 0) Rebuild();
        }

        protected virtual void Reset() {
            if (!template) template = transform.Find("Template");
        }
    }
}
