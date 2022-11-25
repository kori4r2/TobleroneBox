using UnityEngine;
using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(GenericVariable<>), editorForChildClasses: true)]
    public class GenericVariableEditor : Editor {
        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorUtils.DrawScriptField(target);
            SerializedProperty value = serializedObject.FindProperty("value");
            EditorGUILayout.PropertyField(value);
            bool shouldNotifyChange = Application.isPlaying && serializedObject.hasModifiedProperties;
            serializedObject.ApplyModifiedProperties();
            if (shouldNotifyChange) {
                NotifyObservers((dynamic)target);
            }
        }

        private void NotifyObservers<T>(GenericVariable<T> target) {
            target.NotifyObservers();
        }
    }
}
