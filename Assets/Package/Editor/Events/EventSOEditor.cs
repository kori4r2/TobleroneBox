using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(EventSO), editorForChildClasses: true)]
    public class EventSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Force Raise")) {
                (target as EventSO).Raise();
            }
        }
    }
}
