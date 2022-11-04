using UnityEngine;
using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    [CustomPropertyDrawer(typeof(KeyValuePair<,>))]
    public class KeyValuePairPropertyDrawer : PropertyDrawer {
        private const float middleLabelWidth = 45f;
        private const string keyPropertyName = "key";
        private const string valuePropertyName = "value";
        private SerializedProperty keyProperty;
        private SerializedProperty valueProperty;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            FindRelativeProperties(property);
            float keyHeight = EditorGUI.GetPropertyHeight(keyProperty);
            float valueHeight = EditorGUI.GetPropertyHeight(valueProperty);
            return Mathf.Max(keyHeight, valueHeight);
        }

        private void FindRelativeProperties(SerializedProperty property) {
            keyProperty = property.FindPropertyRelative(keyPropertyName);
            valueProperty = property.FindPropertyRelative(valuePropertyName);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            FindRelativeProperties(property);
            float halfWidth = (position.width - middleLabelWidth) / 2f;
            DrawKeyProperty(position, halfWidth);
            DrawMiddleLabel(position, halfWidth);
            DrawValueProperty(position, halfWidth);
            EditorGUI.EndProperty();
        }

        private Rect DrawKeyProperty(Rect position, float keyWidth) {
            float keyHeight = EditorGUI.GetPropertyHeight(keyProperty);
            Rect keyRect = new Rect(position.x, position.y + ((position.height - keyHeight) / 2f), keyWidth, keyHeight);
            EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none, true);
            return position;
        }

        private static Rect DrawMiddleLabel(Rect position, float halfWidth) {
            Rect labelRect = new Rect(position.x + halfWidth, position.y, middleLabelWidth, position.height);
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUI.LabelField(labelRect, "=>", labelStyle);
            return position;
        }

        private void DrawValueProperty(Rect position, float valueWidth) {
            float valueHeight = EditorGUI.GetPropertyHeight(valueProperty);
            Rect valueRect = new Rect(position.xMax - valueWidth, position.y + ((position.height - valueHeight) / 2f), valueWidth, valueHeight);
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none, true);
        }
    }
}
