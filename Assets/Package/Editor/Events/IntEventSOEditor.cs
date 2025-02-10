using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(IntEventSO), editorForChildClasses: true)]
    public class IntEventSOEditor : GenericEventSOEditor<int> {
        protected override int GetValue(SerializedProperty debugProperty) {
            return debugProperty.intValue;
        }
    }
}
