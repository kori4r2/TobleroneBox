using UnityEngine;
using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    public static class EditorUtils {
        public static Rect DrawSeparator(Rect position, float thickness = 2f, float padding = 5f) {
            Rect lineRect = new Rect(position.x, position.y + padding + thickness, position.width, thickness);
            EditorGUI.DrawRect(lineRect, Color.grey);
            position.position = new Vector2(position.x, position.y + padding * 2f + thickness);
            return position;
        }

        public static Texture2D GetCroppedTexture(Sprite sprite) {
            Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, sprite.texture.format, false);
            Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                      (int)sprite.textureRect.y,
                                                      (int)sprite.textureRect.width,
                                                      (int)sprite.textureRect.height);
            croppedTexture.SetPixels(pixels);
            croppedTexture.Apply();
            return croppedTexture;
        }

        public static Rect NewRectBelow(Rect rect) {
            Rect returnValue = new Rect(rect);
            returnValue.y += rect.height;
            returnValue.height = EditorGUIUtility.singleLineHeight;
            return returnValue;
        }

        public static Rect CropRect(Rect rect, Rect limits) {
            if (rect.xMax > limits.xMax) {
                rect.width = Mathf.Max(0f, limits.xMax - rect.xMin);
            }
            return rect;
        }

        [MenuItem("Assets/TobleroneBox/Set Selection as Dirty", false)]
        private static void SetSelectionDirty() {
            foreach (Object obj in Selection.objects) {
                EditorUtility.SetDirty(obj);
            }
        }

        [MenuItem("Assets/TobleroneBox/Set Selection as Dirty", true)]
        private static bool SetSelectionDirtyValidation() {
            return Selection.objects != null && Selection.objects.Length > 0;
        }

        [MenuItem("Tools/TobleroneBox/Set All Scriptable Objects as Dirty")]
        private static void SetAllScriptableOjectsDirty() {
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
            foreach (string guid in guids) {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath));
            }
        }
    }
}
