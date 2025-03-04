using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(StringEventSO), editorForChildClasses: true)]
    public class StringEventSOEditor : GenericEventSOEditor<string> {
        protected override string GetValue(SerializedProperty debugProperty) {
            return debugProperty.stringValue;
        }
    }
}
