using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityScene = UnityEngine.SceneManagement.Scene;
using System;

namespace Toblerone.Toolbox.EditorScripts {
    public class UpdateScriptsInScenes {
        private static Type[] typesToCheck = { };

        [MenuItem("Tools/TobleroneBox/Update All Updatable Scripts in All Scenes")]
        public static void UpdateScriptsInAllScenes() {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            string[] guidList = AssetDatabase.FindAssets($"t:SceneAsset");
            List<string> scenesToCheck = new List<string>();
            foreach (string guid in guidList) {
                scenesToCheck.Add(AssetDatabase.GUIDToAssetPath(guid));
            }

            string currentScene = EditorSceneManager.GetActiveScene().path;
            foreach (string scenePath in scenesToCheck) {
                UnityScene updatedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                UpdateAllUpdatables();
                EditorSceneManager.SaveScene(updatedScene);
            }
            EditorSceneManager.OpenScene(currentScene, OpenSceneMode.Single);
        }

        private static void UpdateAllUpdatables() {
            List<UnityEngine.Object> objectsToCheck = new List<UnityEngine.Object>();
            foreach (Type type in typesToCheck) {
                objectsToCheck.AddRange(UnityEngine.Object.FindObjectsOfType(type));
            }

            foreach (UnityEngine.Object obj in objectsToCheck) {
                if (!(obj is IUpdatableScript))
                    continue;
                (obj as IUpdatableScript).UpdateThisObject();
            }
        }
    }
}