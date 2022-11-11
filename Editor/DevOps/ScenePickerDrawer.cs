using UnityEngine;
using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomPropertyDrawer(typeof(ScenePicker))]
    public class ScenePickerDrawer : PropertyDrawer {
        private SerializedProperty sceneName;
        private SerializedProperty assetPath;
        private SerializedProperty guid;
        private Rect rect;
        private Object sceneObj = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            FindSerializedClassProperties(property);
            FindSerializedSceneObject();
            DrawScenePicker(position);
            SaveSceneInfo();
            EditorGUI.EndProperty();
        }

        private void FindSerializedClassProperties(SerializedProperty property) {
            sceneName = property.FindPropertyRelative("sceneName");
            assetPath = property.FindPropertyRelative("assetPath");
            guid = property.FindPropertyRelative("guid");
        }

        private void FindSerializedSceneObject() {
            bool hasSceneInfo = (!string.IsNullOrEmpty(assetPath.stringValue) || !string.IsNullOrEmpty(guid.stringValue));
            if (!sceneObj && hasSceneInfo) {
                FindSceneObjUsingGUID();
                FindSceneObjUsingPathIfNull();
            } else if (sceneObj && !hasSceneInfo) {
                sceneObj = null;
            }
        }

        private void FindSceneObjUsingGUID() {
            if (!string.IsNullOrEmpty(guid.stringValue)) {
                sceneObj = AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(guid.stringValue));
            }
        }

        private void FindSceneObjUsingPathIfNull() {
            if (!string.IsNullOrEmpty(assetPath.stringValue)) {
                sceneObj = !sceneObj ? AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath.stringValue) : sceneObj;
            }
        }

        private void DrawScenePicker(Rect position) {
            rect = new Rect(position.x, position.y, position.size.x, EditorGUIUtility.singleLineHeight);
            sceneObj = EditorGUI.ObjectField(rect, sceneObj, typeof(SceneAsset), false);
        }

        private void SaveSceneInfo() {
            sceneName.stringValue = !sceneObj ? "" : sceneObj.name;
            assetPath.stringValue = !sceneObj ? "" : AssetDatabase.GetAssetPath(sceneObj.GetInstanceID());
            guid.stringValue = !sceneObj ? "" : AssetDatabase.AssetPathToGUID(assetPath.stringValue);
        }
    }
}
