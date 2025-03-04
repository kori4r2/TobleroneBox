using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    public abstract class GenericEventSOEditor<T> : Editor {
        protected abstract T GetValue(SerializedProperty debugProperty);
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Force Raise")) {
                GenericEvent<T> eventSO = target as GenericEvent<T>;
                SerializedProperty debugValue = serializedObject.FindProperty("debugValue");
                eventSO.Raise(GetValue(debugValue));
            }
        }
    }
}
