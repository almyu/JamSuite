using System;
using UnityEngine;

namespace JamSuite
{
    public static class Clipboard
    {
        [Obsolete("Use GUIUtility.systemCopyBuffer")]
        public static bool ContainsText() {
            return GUIUtility.systemCopyBuffer.Length != 0;
        }

        [Obsolete("Use GUIUtility.systemCopyBuffer")]
        public static string GetText() {
            return GUIUtility.systemCopyBuffer;
        }

        [Obsolete("Use GUIUtility.systemCopyBuffer")]
        public static void SetText(string text) {
            GUIUtility.systemCopyBuffer = text ?? "";
        }
    }
}
