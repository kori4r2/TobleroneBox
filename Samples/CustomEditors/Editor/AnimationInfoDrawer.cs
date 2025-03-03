using UnityEngine;
using UnityEditor;
using Toblerone.Toolbox.EditorScripts;
using log4net.Util;
using UnityEditor.Sprites;

namespace Toblerone.Toolbox.CustomEditorsSample.Editor {
    [CustomPropertyDrawer(typeof(AnimationInfo))]
    public class AnimationInfoDrawer : PropertyDrawer {
        private RectManipulator rectManipulator = new RectManipulator();
        private SerializedProperty duration;
        private SerializedProperty sprites;
        private bool draggingOver = false;
        private float sliderValue = 0f;
        private int currentIndex = 0;
        private const float marginSize = 10f;
        private Rect durationRect, spriteRect, animationRect;
        public const float TEXTURE_SIZE = 48f;
        public static float AnimationSliderHeight => 2f * EditorGUIUtility.singleLineHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;
            bool isNull = (property.FindPropertyRelative("sprites")?.arraySize ?? 0) < 1;
            return (3f * marginSize) + EditorGUIUtility.singleLineHeight + TEXTURE_SIZE + (isNull ? 0f : AnimationSliderHeight + 0.5f);
        }

        public override void OnGUI(Rect propertyRect, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(propertyRect, label, property);
            DrawFoldout(propertyRect, property, label);
            if (property.isExpanded)
                DrawPropertyFields(propertyRect, property);
            EditorGUI.EndProperty();
        }

        private void DrawFoldout(Rect propertyRect, SerializedProperty property, GUIContent label) {
            Vector2 foldoutPosition = new Vector2(propertyRect.position.x, propertyRect.position.y);
            Vector2 foldoutSize = new Vector2(propertyRect.size.x, EditorGUIUtility.singleLineHeight);
            Rect foldoutRect = new Rect(foldoutPosition, foldoutSize);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
        }

        private void DrawPropertyFields(Rect propertyRect, SerializedProperty property) {
            CalculateFieldRects(propertyRect);
            FindSerializedFieldProperties(property);
            duration.floatValue = Mathf.Max(0.1f, EditorGUI.FloatField(durationRect, new GUIContent("Duration"), duration.floatValue));
            DrawObjectPicker();
            if (sprites.arraySize > 0) {
                DrawAnimationSlider();
                DrawAnimationSprite();
            } else {
                sliderValue = 0f;
                currentIndex = -1;
            }
        }

        private void FindSerializedFieldProperties(SerializedProperty property) {
            duration = property.FindPropertyRelative("duration");
            sprites = property.FindPropertyRelative("sprites");
        }

        private void CalculateFieldRects(Rect propertyRect) {
            rectManipulator.ResetToRect(propertyRect);
            rectManipulator.OffsetVerticalPosition(EditorGUIUtility.singleLineHeight);
            rectManipulator.SetHorizontalMargins(marginSize, marginSize);
            rectManipulator.SetSize(null, EditorGUIUtility.singleLineHeight);
            durationRect = rectManipulator.GetRect();
            rectManipulator.SetSize(null, TEXTURE_SIZE);
            rectManipulator.OffsetVerticalPosition(durationRect.size.y + marginSize / 2f);
            spriteRect = rectManipulator.GetRect();
            rectManipulator.OffsetVerticalPosition(spriteRect.size.y + marginSize / 2f);
            rectManipulator.SetSize(null, 2 * EditorGUIUtility.singleLineHeight);
            animationRect = rectManipulator.GetRect();
        }

        private void DrawObjectPicker() {
            Event evt = Event.current;
            switch (evt.type) {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!spriteRect.Contains(evt.mousePosition)) {
                        if (draggingOver) {
                            DragAndDrop.visualMode = DragAndDropVisualMode.None;
                        }
                        draggingOver = false;
                        return;
                    }
                    draggingOver = true;

                    foreach (Object obj in DragAndDrop.objectReferences) {
                        if (!(obj is Sprite)) {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                            return;
                        }
                    }

                    if (evt.type == EventType.DragPerform) {
                        DragAndDrop.AcceptDrag();
                        sprites.ClearArray();
                        for (int index = 0; index < DragAndDrop.objectReferences.Length; index++) {
                            sprites.InsertArrayElementAtIndex(index);
                            sprites.GetArrayElementAtIndex(index).objectReferenceValue = DragAndDrop.objectReferences[index];
                        }
                        DragAndDrop.visualMode = DragAndDropVisualMode.None;
                        draggingOver = false;
                    } else {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                    }
                    evt.Use();
                    break;
                case EventType.DragExited:
                    if (draggingOver) {
                        draggingOver = false;
                        DragAndDrop.visualMode = DragAndDropVisualMode.None;
                    }
                    break;
            }
        }

        private void DrawAnimationSlider() {
            rectManipulator.ResetToRect(animationRect);
            rectManipulator.SetSize(null, EditorGUIUtility.singleLineHeight);
            Rect timerRect = rectManipulator.GetRect();
            rectManipulator.OffsetVerticalPosition(EditorGUIUtility.singleLineHeight);
            Rect sliderRect = rectManipulator.GetRect();
            sliderValue = EditorGUI.Slider(sliderRect, sliderValue, 0f, 1f);
            if (sliderValue == 1)
                currentIndex = sprites.arraySize - 1;
            else
                currentIndex = Mathf.FloorToInt(sliderValue * sprites.arraySize);
            GUIStyle timerStyle = EditorStyles.miniLabel;
            timerStyle.alignment = TextAnchor.LowerCenter;
            EditorGUI.LabelField(timerRect, $"{sliderValue * duration.floatValue:0.###} s", timerStyle);
        }

        private void DrawAnimationSprite() {
            rectManipulator.ResetToRect(spriteRect);
            rectManipulator.SetSize(TEXTURE_SIZE, TEXTURE_SIZE);
            rectManipulator.OffsetHorizontalPosition(spriteRect.width / 2f - TEXTURE_SIZE / 2f);
            Rect textureRect = EditorUtils.CropRect(rectManipulator.GetRect(), spriteRect);
            Sprite animationSprite = sprites.GetArrayElementAtIndex(currentIndex).objectReferenceValue as Sprite;
            float aspect = animationSprite.textureRect.width / animationSprite.textureRect.height;
            EditorGUI.DrawTextureTransparent(textureRect, AssetPreview.GetAssetPreview(animationSprite), ScaleMode.ScaleToFit, aspect);
        }
    }
}
