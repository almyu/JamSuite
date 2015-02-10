﻿using UnityEngine;

namespace JamSuite {

    public static class Clipboard {

        public static bool ContainsText() {
            return new TextEditor().CanPaste();
        }

        public static string GetText() {
            var ed = new TextEditor();
            ed.Paste();

            return ed.content.text;
        }

        public static void SetText(string text) {
            if (string.IsNullOrEmpty(text)) return;

            var ed = new TextEditor();
            ed.content = new GUIContent(text);
            ed.SelectAll();
            ed.Copy();
        }
    }
}
