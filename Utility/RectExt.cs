using UnityEngine;

namespace JamSuite
{
    public static class RectExt
    {
        public static Rect WithPosition(this Rect rect, Vector2 position) { rect.position = position; return rect; }
        public static Rect WithCenter(this Rect rect, Vector2 center) { rect.center = center; return rect; }
        public static Rect WithSize(this Rect rect, Vector2 size) { rect.size = size; return rect; }
        public static Rect WithWidth(this Rect rect, float width) { rect.width = width; return rect; }
        public static Rect WithHeight(this Rect rect, float height) { rect.height = height; return rect; }
        public static Rect WithMin(this Rect rect, Vector2 min) { rect.min = min; return rect; }
        public static Rect WithMax(this Rect rect, Vector2 max) { rect.max = max; return rect; }
        public static Rect WithX(this Rect rect, float x) { rect.x = x; return rect; }
        public static Rect WithXMin(this Rect rect, float xMin) { rect.xMin = xMin; return rect; }
        public static Rect WithXMax(this Rect rect, float xMax) { rect.xMax = xMax; return rect; }
        public static Rect WithY(this Rect rect, float y) { rect.y = y; return rect; }
        public static Rect WithYMin(this Rect rect, float yMin) { rect.yMin = yMin; return rect; }
        public static Rect WithYMax(this Rect rect, float yMax) { rect.yMax = yMax; return rect; }

        public static Rect WithXMinMax(this Rect rect, float xMin, float xMax) { rect.xMin = xMin; rect.xMax = xMax; return rect; }
        public static Rect WithYMinMax(this Rect rect, float yMin, float yMax) { rect.yMin = yMin; rect.yMax = yMax; return rect; }

        public static Rect Move(this Rect rect, Vector2 offset) { rect.position += offset; return rect; }
        public static Rect GrowMin(this Rect rect, Vector2 delta) { rect.min += delta; return rect; }
        public static Rect GrowMax(this Rect rect, Vector2 delta) { rect.max += delta; return rect; }
    }
}
