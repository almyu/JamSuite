using UnityEngine;

public static class VectorExt {

    public static Vector2 WithX(this Vector2 vec, float x) { return new Vector2(x, vec.y); }
    public static Vector2 WithY(this Vector2 vec, float y) { return new Vector2(vec.x, y); }
    public static Vector3 WithZ(this Vector2 vec, float z) { return new Vector3(vec.x, vec.y, z); }

    public static Vector3 WithX(this Vector3 vec, float x) { return new Vector3(x, vec.y, vec.z); }
    public static Vector3 WithY(this Vector3 vec, float y) { return new Vector3(vec.x, y, vec.z); }
    public static Vector3 WithZ(this Vector3 vec, float z) { return new Vector3(vec.x, vec.y, z); }
    public static Vector4 WithW(this Vector3 vec, float w) { return new Vector4(vec.x, vec.y, vec.z, w); }

    public static Vector4 WithX(this Vector4 vec, float x) { return new Vector4(x, vec.y, vec.z, vec.w); }
    public static Vector4 WithY(this Vector4 vec, float y) { return new Vector4(vec.x, y, vec.z, vec.w); }
    public static Vector4 WithZ(this Vector4 vec, float z) { return new Vector4(vec.x, vec.y, z, vec.w); }
    public static Vector4 WithW(this Vector4 vec, float w) { return new Vector4(vec.x, vec.y, vec.z, w); }
}
