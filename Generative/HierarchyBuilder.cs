/// Version: 2017-03-23

using UnityEngine;

namespace JamSuite.Generative
{
    public enum HierarchyRebuildMode
    {
        Manual = 0,
        Awake = 1,
        EditorUpdate = 2,
        RuntimeUpdate = 4,
        AnyUpdate = EditorUpdate | RuntimeUpdate,
    }

    [ExecuteInEditMode]
    public abstract class HierarchyBuilder<T> : HierarchyBuilder<Transform, T> {}

    [ExecuteInEditMode]
    public abstract class HierarchyBuilder<Template, Data> : MonoBehaviour
    where Template : Component
    {
        public Template template;
        public HierarchyRebuildMode rebuildMode = HierarchyRebuildMode.Awake;

        protected Transform parent;
        protected Template lastSpawn;


        [ContextMenu("Rebuild")]
        public void Rebuild() {
            for (int i = transform.childCount; i-- > 0; ) {
                var child = transform.GetChild(i);
                if (child != template.transform)
                    DestroyImmediate(child.gameObject);
            }
            Build();
        }


        protected abstract void Build();

        protected virtual void Tune(Template spawn, Data data) {}


        protected Template Spawn(Data data) {
            lastSpawn = Instantiate(template);
            lastSpawn.transform.SetParent(parent ? parent : transform, false);
            Tune(lastSpawn, data);
            lastSpawn.gameObject.SetActive(true);
            return lastSpawn;
        }

        protected void Descend() {
            parent = lastSpawn ? lastSpawn.transform : null;
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
            if (!template) template = GetComponentInChildren<Template>(true);
        }
    }
}
