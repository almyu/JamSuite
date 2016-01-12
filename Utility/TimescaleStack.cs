using UnityEngine;
using System.Collections.Generic;

public static class TimescaleStack {

    private static Stack<float> stack = new Stack<float>();

    public static void Push(float scale) {
        stack.Push(Time.timeScale);
        Time.timeScale = scale;
    }

    public static float Pop() {
        var last = Time.timeScale;
        Time.timeScale = stack.Pop();
        return last;
    }
}
