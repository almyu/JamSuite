using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;
#endif

namespace JamSuite
{
    [CreateAssetMenu(fileName = "ToDo", menuName = "To-do List", order = 230)]
    public class ToDoList : ScriptableObject
    {
        public List<string>
            todo = new List<string>(),
            done = new List<string>();
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(ToDoList))]
    public class ToDoInspector : Editor
    {
        public new ToDoList target { get { return (ToDoList) base.target; } }

        private class Styles
        {
            public GUIStyle
                inputField, inputFieldEnd,
                buttonLeft, buttonRight;
        }
        private Styles styles;

        private ReorderableList todo, done;
        private List<Action> plan = new List<Action>();

        protected override void OnHeaderGUI() {
            EditorGUILayout.Space();
        }

        public override void OnInspectorGUI() {
            SetupStyles();
            SetupList(ref todo, target.todo, target.done, new GUIContent("To Do"));
            SetupList(ref done, target.done, target.todo, new GUIContent("Done"));

            var input = DisplayInputField("Input");
            if (input != "") {
                target.todo.Insert(0, input);
                EditorUtility.SetDirty(target);
                EditorGUI.FocusTextInControl("Input");
            }
            EditorGUILayout.Space();

            todo.DoLayoutList();
            done.DoLayoutList();

            if (plan.Count != 0) {
                foreach (var action in plan) action();
                plan.Clear();
                EditorUtility.SetDirty(target);
            }

            GUI.contentColor = Color.black;
            if (GUILayout.Button("Sort", GUILayout.Height(30))) {
                target.todo.Sort();
                EditorUtility.SetDirty(target);
            }
        }

        protected bool SetupStyles() {
            if (styles != null) return false;

            styles = new Styles() {
                inputField = new GUIStyle(GUI.skin.FindStyle("SearchCancelButtonEmpty")) {
                    border = new RectOffset(0, 8, 6, 6),
                    padding = new RectOffset(),
                    font = EditorStyles.largeLabel.font,
                    alignment = TextAnchor.MiddleLeft,
                    richText = true,
                    fixedHeight = 24f,
                    fixedWidth = 0f,
                    stretchWidth = true,
                },
                inputFieldEnd = new GUIStyle(GUI.skin.FindStyle("ToolbarSeachTextField")) {
                    border = new RectOffset(6, 0, 6, 6),
                    fixedHeight = 24f,
                    fixedWidth = 6f,
                },
                buttonLeft = new GUIStyle(GUI.skin.FindStyle("LargeButtonLeft")) {
                    padding = new RectOffset(6, 0, 0, 0),
                    alignment = TextAnchor.MiddleLeft,
                    richText = true,
                },
                buttonRight = new GUIStyle(GUI.skin.FindStyle("LargeButtonRight")) {
                    padding = new RectOffset(),
                }
            };
            return true;
        }

        protected bool SetupList(ref ReorderableList list, List<string> source, List<string> dest, GUIContent label) {
            if (list != null && list.list == source) return false;

            list = new ReorderableList(source, typeof(string), true, true, false, false);
            list.footerHeight = 6f;
            list.onChangedCallback += _ => EditorUtility.SetDirty(target);
            list.drawHeaderCallback += r => EditorGUI.PrefixLabel(r, label, EditorStyles.boldLabel);
            list.drawElementCallback += (r, i, active, focus) => DisplayItem(r, i, active, focus, source, dest);
            //list.drawElementBackgroundCallback += DisplayItemBackground;
            //list.showDefaultBackground = false;
            return true;
        }

        protected string DisplayInputField(string name) {
            var r = EditorGUILayout.GetControlRect();

            GUI.contentColor = Color.black;
            GUI.Box(r, GUIContent.none, styles.inputFieldEnd);
            GUI.SetNextControlName(name);
            var input = EditorGUI.DelayedTextField(r.WithXMin(r.xMin + 6f), "", styles.inputField);

            EditorGUILayout.Space();
            return input;
        }

        protected void DisplayItemBackground(Rect r, int i, bool active, bool focus) {
            GUI.color = i % 2 == 0 ? Color.white.WithA(0.2f) : Color.black.WithA(0.05f);
            GUI.DrawTexture(r.WithXMin(0f), Texture2D.whiteTexture);
            GUI.color = Color.white;
        }

        protected void DisplayItem(Rect r, int i, bool active, bool focus, List<string> source, List<string> dest) {
            var rightWidth = 20f;
            var leftRect = r.WithXMax(r.xMax - rightWidth);
            var rightRect = r.WithXMin(r.xMax - rightWidth);

            var text = source[i];

            GUI.backgroundColor = text.EndsWith("!")
                ? new Color(1f, 0.85f, 0.7f, 0.5f)
                : Color.white.WithA(0.5f);

            GUI.contentColor = Color.black;

            //if (focus || (active && GUI.GetNameOfFocusedControl() == "Task")) {
            if (active) {
                //GUI.SetNextControlName("Task");
                source[i] = EditorGUI.TextField(leftRect, text, styles.buttonLeft);
                if (source[i] != text) EditorUtility.SetDirty(target);
                //EditorGUI.FocusTextInControl("Task");
            }
            else if (GUI.Button(leftRect, new GUIContent(text, text), styles.buttonLeft)) {
                plan.Add(() => {
                    dest.Insert(0, source[i]);
                    source.RemoveAt(i);
                });
            }
            GUI.contentColor = Color.black.WithA(0.5f);

            if (GUI.Button(rightRect, "×", styles.buttonRight))
                plan.Add(() => source.RemoveAt(i));

            GUI.backgroundColor = Color.white;
        }
    }
#endif
}
