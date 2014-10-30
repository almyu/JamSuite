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

    public static Color FromHSV(float h, float s, float v, float a) {
        float sector = h * 6.0f;
        
        float chroma = v * s;
        float x = chroma * (1.0f - Mathf.Abs(Mathf.Repeat(sector, 2.0f) - 1.0f));
        
        float r = 0.0f;
        float g = 0.0f;
        float b = 0.0f;
        
        switch (Mathf.FloorToInt(sector)) {
            case 0: r = chroma; g = x;      break;
            case 1: r = x;      g = chroma; break;
            case 2: g = chroma; b = x;      break;
            case 3: g = x;      b = chroma; break;
            case 4: b = chroma; r = x;      break;
            case 5: b = x;      r = chroma; break;
        }
        
        float diff = v - chroma;
        return new Color(Mathf.Clamp01(r + diff), Mathf.Clamp01(g + diff), Mathf.Clamp01(b + diff), Mathf.Clamp01(a));
    }
    public static Color32 FromHSV(byte h, byte s, byte v, byte a) { return (Color32) FromHSV(h / 255.0f, s / 255.0f, v / 255.0f, a / 255.0f); }

    public static string ToHexString(this Color clr) { return ((Color32) clr).ToHexString(); }
    public static string ToHexString(this Color32 clr) { return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", clr.r, clr.g, clr.b, clr.a); }
}
