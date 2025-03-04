using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomEditor(typeof(FloatEventSO), editorForChildClasses: true)]
    public class FloatEventSOEditor : GenericEventSOEditor<float> {
        protected override float GetValue(SerializedProperty debugProperty) {
            return debugProperty.floatValue;
        }
    }
}
