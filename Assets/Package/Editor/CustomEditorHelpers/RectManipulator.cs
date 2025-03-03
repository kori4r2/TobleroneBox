using System;
using UnityEngine;

namespace Toblerone.Toolbox.EditorScripts {
    public class RectManipulator {
        private float xPosition = 0f;
        private float yPosition = 0f;
        private float xSize = 0f;
        private float ySize = 0f;
        private float leftMargin = 0f;
        private float rightMargin = 0f;
        private float topMargin = 0f;
        private float bottomMargin = 0f;

        public RectManipulator() {
            Reset();
        }

        private void Reset() {
            xPosition = 0f;
            yPosition = 0f;
            xSize = 0f;
            ySize = 0f;
            ResetMargins();
        }

        public void ResetMargins() {
            leftMargin = 0;
            rightMargin = 0;
            topMargin = 0;
            bottomMargin = 0;
        }

        public RectManipulator(Vector2 position, Vector2 size) : this() {
            xPosition = position.x;
            yPosition = position.y;
            xSize = size.x;
            ySize = size.y;
        }

        public RectManipulator(Rect rect) : this(rect.position, rect.size) { }

        public RectManipulator(Rect rect, Vector2? xAnchors, Vector2? yAnchors) : this(rect) {
            ApplyValidAnchors(xAnchors, yAnchors);
        }

        public void ApplyValidAnchors(Vector2? xAnchors, Vector2? yAnchors) {
            if (!xAnchors.HasValue) {
                ValidateAnchors(xAnchors.Value);
                ApplyHorizontalAnchors(xAnchors.Value);
            }
            if (!yAnchors.HasValue) {
                ValidateAnchors(yAnchors.Value);
                ApplyVerticalAnchors(yAnchors.Value);
            }
        }

        public void ApplyHorizontalAnchors(Vector2 xAnchors) {
            xPosition += xAnchors.x * xSize;
            xSize *= xAnchors.y - xAnchors.x;
        }

        public void ApplyVerticalAnchors(Vector2 yAnchors) {
            yPosition += yAnchors.x * ySize;
            ySize *= yAnchors.y - yAnchors.x;
        }

        private void ValidateAnchors(Vector2 anchors) {
            if (anchors.x < 0)
                throw new ArgumentException("[RectManipulator]: Anchor min value cannot be lower than 0");
            if (anchors.y < 0)
                throw new ArgumentException("[RectManipulator]: Anchor max value cannot be lower than 0");
            if (anchors.x > 1.0f)
                throw new ArgumentException("[RectManipulator]: Anchor min value cannot be greater than 1.0");
            if (anchors.y > 1.0f)
                throw new ArgumentException("[RectManipulator]: Anchor max value cannot be greater than 1.0");
            if (Mathf.Abs(anchors.y - anchors.x) <= Mathf.Epsilon)
                throw new ArgumentException("[RectManipulator]: Anchor min and max values must be different");
            if (anchors.y < anchors.x)
                throw new ArgumentException("[RectManipulator]: Anchor max value must be greater than min value");
        }

        public void ResetToRect(Rect rect) {
            Reset();
            xPosition = rect.position.x;
            yPosition = rect.position.y;
            xSize = rect.size.x;
            ySize = rect.size.y;
        }

        public void ResetToRect(Rect rect, Vector2? xAnchors, Vector2? yAnchors) {
            ResetToRect(rect);
            ApplyValidAnchors(xAnchors, yAnchors);
        }

        public void SetAllMargins(float top, float bottom, float left, float right) {
            SetVerticalMargins(top, bottom);
            SetHorizontalMargins(left, right);
        }

        public void SetVerticalMargins(float top, float bottom) {
            topMargin = top;
            bottomMargin = bottom;
        }

        public void SetHorizontalMargins(float left, float right) {
            leftMargin = left;
            rightMargin = right;
        }

        public void MoveToLeftOf(Rect rect, bool centerVertical = false) {
            xPosition = rect.position.x - xSize;
            yPosition = centerVertical ? rect.position.y + rect.size.y / 2 - ySize / 2 : rect.position.y;
        }

        public void MoveToRightOf(Rect rect, bool centerVertical = false) {
            xPosition = rect.position.x + rect.size.x;
            yPosition = centerVertical ? rect.position.y + rect.size.y / 2 - ySize / 2 : rect.position.y;
        }

        public void MoveToAbove(Rect rect, bool centerHorizontal = false) {
            yPosition = rect.position.y - ySize;
            xPosition = centerHorizontal ? rect.position.x + rect.size.x / 2 - xSize / 2 : rect.position.x;
        }

        public void MoveToBelow(Rect rect, bool centerHorizontal = false) {
            yPosition = rect.position.y + rect.size.y;
            xPosition = centerHorizontal ? rect.position.x + rect.size.x / 2 - xSize / 2 : rect.position.x;
        }

        public void OffsetPosition(float verticalOffset, float horizontalOffset) {
            OffsetVerticalPosition(verticalOffset);
            OffsetHorizontalPosition(horizontalOffset);
        }

        public void OffsetVerticalPosition(float offset) {
            yPosition += offset;
        }

        public void OffsetHorizontalPosition(float offset) {
            xPosition += offset;
        }

        public void SetSize(Vector2 size) {
            xSize = size.x;
            ySize = size.y;
        }

        public void SetSize(float? xSize, float? ySize) {
            if (xSize.HasValue)
                this.xSize = xSize.Value;
            if (ySize.HasValue)
                this.ySize = ySize.Value;
        }

        public Rect GetRect() {
            Vector2 position = new Vector2(xPosition + leftMargin, yPosition + topMargin);
            Vector2 size = new Vector2(xSize - leftMargin - rightMargin, ySize - topMargin - bottomMargin);
            return new Rect(position, size);
        }
    }
}
