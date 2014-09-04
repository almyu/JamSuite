using UnityEngine;

namespace UnityEngine {

    public static class RectTransformExt {

        public static void AddChild(this RectTransform xf, RectTransform other) {
            var initialPosition = other.anchoredPosition;
            var initialSize = other.sizeDelta;

            other.parent = xf;

            other.anchoredPosition = initialPosition;
            other.sizeDelta = initialSize;
        }
    }
}
