using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;

namespace Toblerone.Toolbox.EditorScripts {
    public static class EditorUtils {
        public static Rect DrawSeparator(Rect position, float thickness = 2f, float padding = 5f) {
            Rect lineRect = new Rect(position.x, position.y + padding + thickness, position.width, thickness);
            EditorGUI.DrawRect(lineRect, Color.grey);
            position.position = new Vector2(position.x, position.y + padding * 2f + thickness);
            return position;
        }

        public static Rect NewRectBelow(Rect rect) {
            Rect returnValue = new Rect(rect);
            returnValue.y += rect.height;
            returnValue.height = EditorGUIUtility.singleLineHeight;
            return returnValue;
        }

        public static Rect CropRect(Rect rect, Rect limits) {
            float xOffset = (rect.xMin < limits.xMin) ? limits.xMin - rect.xMin : 0;
            float yOffset = (rect.yMin < limits.yMin) ? limits.yMin - rect.yMin : 0;
            if (xOffset > 0 || yOffset > 0)
                rect.position += new Vector2(xOffset, yOffset);
            if (rect.xMax > limits.xMax)
                rect.width -= rect.xMax - limits.xMax;
            if (rect.yMax > limits.yMax)
                rect.height -= rect.yMax - limits.yMax;
            return rect;
        }

        public static void DrawScriptField(Object editorTarget) {
            using (new EditorGUI.DisabledScope(true)) {
                MonoScript monoScript = GetMonoScript(editorTarget);
                EditorGUILayout.ObjectField("Script", monoScript, editorTarget.GetType(), false);
            }
        }

        private static MonoScript GetMonoScript(Object editorTarget) {
            if ((editorTarget as MonoBehaviour) != null)
                return MonoScript.FromMonoBehaviour((MonoBehaviour)editorTarget);
            else
                return MonoScript.FromScriptableObject((ScriptableObject)editorTarget);
        }
    }
}
