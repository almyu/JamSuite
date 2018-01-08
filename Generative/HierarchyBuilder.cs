/// Version: 2018-01-08

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

        protected Transform root, parent;
        protected Template lastSpawn;


        public virtual void Rebuild() {
            for (int i = root.childCount; i-- > 0; ) {
                var child = root.GetChild(i);
                if (child == template.transform) break;

                DestroyImmediate(child.gameObject);
            }
            Build();
        }


        protected abstract void Build();

        protected virtual void Tune(Template spawn, Data data) {}


        protected Template Spawn(Data data) {
            lastSpawn = Instantiate(template);
            lastSpawn.transform.SetParent(parent ? parent : root, false);
            Tune(lastSpawn, data);
            lastSpawn.gameObject.SetActive(true);
            return lastSpawn;
        }

        protected void Descend() {
            parent = lastSpawn ? lastSpawn.transform : null;
        }

        protected void Ascend() {
            if (parent != root)
                parent = parent ? parent.parent : root;
        }

        protected void AscendToTop() {
            parent = transform;
        }


        protected virtual void Awake() {
            root = template ? template.transform.parent : transform;

            if (!Application.isPlaying) return;

            if (template.gameObject.activeSelf)
                template.gameObject.SetActive(false);

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
