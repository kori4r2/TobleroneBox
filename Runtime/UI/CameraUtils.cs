using UnityEngine;

namespace Toblerone.Toolbox {
    public static class CameraUtils {
        public static Vector2 GetWorldSpaceCameraSize(Camera camera) {
            Vector3 screenSpaceSize = new Vector3(Screen.width, Screen.height, 0f);
            Vector2 worldSpaceSize = 2 * camera.ScreenToWorldPoint(screenSpaceSize);
            return worldSpaceSize;
        }

        public static Vector2 GetWorldSpaceCameraMinPosition(Camera camera) {
            return camera.ScreenToWorldPoint(Vector2.zero);
        }

        public static Vector2 GetWorldSpaceCameraMaxPosition(Camera camera) {
            return camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        }

        public static bool IsInsideLimits(Vector2 currentPosition, Vector2 minPosition, Vector2 maxPosition) {
            return currentPosition.x <= maxPosition.x && currentPosition.x >= minPosition.x &&
                   currentPosition.y <= maxPosition.y && currentPosition.y >= minPosition.y;
        }
    }
}
