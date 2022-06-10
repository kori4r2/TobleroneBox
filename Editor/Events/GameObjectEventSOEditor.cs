using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(GameObjectEventSO), editorForChildClasses: true)]
    public class GameObjectEventSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Force Raise")) {
                GameObjectEventSO eventSO = target as GameObjectEventSO;
                SerializedProperty debugValue = serializedObject.FindProperty("debugValue");
                eventSO.Raise(debugValue.objectReferenceValue as GameObject);
            }
        }
    }
}
