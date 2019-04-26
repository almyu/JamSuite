/// Version: 2018-01-14

using System;
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
    public abstract class HierarchyBuilder<Template> : MonoBehaviour
    where Template : Component
    {
        public delegate void TuneDelegate(Template spawn);

        public Template template;
        public HierarchyRebuildMode rebuildMode = HierarchyRebuildMode.Awake;

        protected Transform root, parent;
        protected Template lastSpawn;


        public virtual void Rebuild() {
            if (!root) FindRoot();

            for (int i = root.childCount; i-- > 0; ) {
                var child = root.GetChild(i);
                if (child == template.transform) break;

                DestroyImmediate(child.gameObject);
            }
            Build();
        }

        protected abstract void Build();


        protected Template Spawn(TuneDelegate tune = null) {
            lastSpawn = Instantiate(template);
            lastSpawn.transform.SetParent(parent ? parent : root, false);
            if (tune != null) tune(lastSpawn);
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


        void FindRoot() {
            root = template ? template.transform.parent : transform;
        }

        protected virtual void Awake() {
            FindRoot();

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
