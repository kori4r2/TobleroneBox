using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    public class UpdateScriptsInAssets {
        [MenuItem("Tools/TobleroneBox/Update All Updatable Scripts in All Assets")]
        public static void UpdateAllAssets() {
            UpdateAllScriptableObjects();
            UpdateAllPrefabs();
        }

        private static void UpdateAllScriptableObjects() {
            string[] objectsFound = AssetDatabase.FindAssets($"t:{typeof(ScriptableObject)}");
            foreach (string guid in objectsFound) {
                ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
                if (!(scriptableObject is IUpdatableScript))
                    continue;
                (scriptableObject as IUpdatableScript).UpdateThisObject();
                EditorUtility.SetDirty(scriptableObject);
            }
            AssetDatabase.SaveAssets();
        }

        private static void UpdateAllPrefabs() {
            Debug.LogWarning("Prefab update is not implemented yet");
        }
    }
}