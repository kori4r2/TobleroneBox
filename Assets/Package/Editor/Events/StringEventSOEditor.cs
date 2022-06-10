using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(StringEventSO), editorForChildClasses: true)]
    public class StringEventSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Force Raise")) {
                StringEventSO eventSO = target as StringEventSO;
                SerializedProperty debugValue = serializedObject.FindProperty("debugValue");
                eventSO.Raise(debugValue.stringValue);
            }
        }
    }
}
