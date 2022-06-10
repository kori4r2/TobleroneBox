using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(IntEventSO), editorForChildClasses: true)]
    public class IntEventSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Force Raise")) {
                IntEventSO eventSO = target as IntEventSO;
                SerializedProperty debugValue = serializedObject.FindProperty("debugValue");
                eventSO.Raise(debugValue.intValue);
            }
        }
    }
}
