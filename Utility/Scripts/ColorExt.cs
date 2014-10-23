using UnityEngine;

public static class ColorExt {

    public static Color WithR(this Color clr, float r) { return new Color(r, clr.g, clr.b, clr.a); }
    public static Color WithG(this Color clr, float g) { return new Color(clr.r, g, clr.b, clr.a); }
    public static Color WithB(this Color clr, float b) { return new Color(clr.r, clr.g, b, clr.a); }
    public static Color WithA(this Color clr, float a) { return new Color(clr.r, clr.g, clr.b, a); }

    public static Color32 WithR(this Color32 clr, byte r) { return new Color32(r, clr.g, clr.b, clr.a); }
    public static Color32 WithG(this Color32 clr, byte g) { return new Color32(clr.r, g, clr.b, clr.a); }
    public static Color32 WithB(this Color32 clr, byte b) { return new Color32(clr.r, clr.g, b, clr.a); }
    public static Color32 WithA(this Color32 clr, byte a) { return new Color32(clr.r, clr.g, clr.b, a); }

    public static string ToHexString(this Color clr) { return ((Color32) clr).ToHexString(); }
    public static string ToHexString(this Color32 clr) { return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", clr.r, clr.g, clr.b, clr.a); }
}
