using UnityEngine;

public static class ColorExt {

    public static Color ReplaceR(this Color clr, float r) {
        return new Color(r, clr.g, clr.b, clr.a);
    }

    public static Color ReplaceG(this Color clr, float g) {
        return new Color(clr.r, g, clr.b, clr.a);
    }

    public static Color ReplaceB(this Color clr, float b) {
        return new Color(clr.r, clr.g, b, clr.a);
    }

    public static Color ReplaceA(this Color clr, float a) {
        return new Color(clr.r, clr.g, clr.b, a);
    }

    public static string ToHexString(this Color clr) {
        return ((Color32) clr).ToHexString();
    }


    public static Color32 ReplaceR(this Color32 clr, byte r) {
        return new Color32(r, clr.g, clr.b, clr.a);
    }

    public static Color32 ReplaceG(this Color32 clr, byte g) {
        return new Color(clr.r, g, clr.b, clr.a);
    }

    public static Color32 ReplaceB(this Color32 clr, byte b) {
        return new Color(clr.r, clr.g, b, clr.a);
    }

    public static Color32 ReplaceA(this Color32 clr, byte a) {
        return new Color(clr.r, clr.g, clr.b, a);
    }

    public static string ToHexString(this Color32 clr) {
        return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", clr.r, clr.g, clr.b, clr.a);
    }
}
