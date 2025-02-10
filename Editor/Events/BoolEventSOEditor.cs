using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(BoolEventSO), editorForChildClasses: true)]
    public class BoolEventSOEditor : GenericEventSOEditor<bool> {
        protected override bool GetValue(SerializedProperty debugProperty) {
            return debugProperty.boolValue;
        }
    }
}
