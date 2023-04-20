using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityScene = UnityEngine.SceneManagement.Scene;
using System;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    public class UpdateScriptsInScenes {
        private static Type[] typesToCheck = { typeof(MonoBehaviour) };

        [MenuItem("Tools/TobleroneBox/Update All Updatable Scripts in All Scenes")]
        public static void UpdateScriptsInAllScenes() {
            if (!EditorUtility.DisplayDialog("UpdateScriptsInAllScenes", "This will go through all MonoBehaviours in all SceneAssets in the project, and may take a very long time. Continue?", "Yes", "No")) {
                Debug.Log("UpdateScriptsInAllScenes run cancelled");
                return;
            }
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                Debug.Log("UpdateScriptsInAllScenes run cancelled");
                return;
            }
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
            Debug.Log("UpdateScriptsInAllScenes run finished");
        }

        private static void UpdateAllUpdatables() {
            List<UnityEngine.Object> objectsToCheck = new List<UnityEngine.Object>();
            foreach (Type type in typesToCheck) {
                objectsToCheck.AddRange(UnityEngine.Object.FindObjectsOfType(type, true));
            }

            foreach (UnityEngine.Object obj in objectsToCheck) {
                if (!(obj is IUpdatableScript))
                    continue;
                (obj as IUpdatableScript).UpdateThisObject();
            }
        }
    }
}