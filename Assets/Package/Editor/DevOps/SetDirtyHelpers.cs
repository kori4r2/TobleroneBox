using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    public static class SetDirtyHelpers {
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
