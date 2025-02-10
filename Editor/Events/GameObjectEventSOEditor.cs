using UnityEditor;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(GameObjectEventSO), editorForChildClasses: true)]
    public class GameObjectEventSOEditor : GenericEventSOEditor<GameObject> {
        protected override GameObject GetValue(SerializedProperty debugProperty) {
            return debugProperty.objectReferenceValue as GameObject;
        }
    }
}
