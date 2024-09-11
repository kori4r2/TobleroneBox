using UnityEngine;
using UnityEngine.UI;

namespace Toblerone.Toolbox.SceneManagement {
    public class BasicSceneChangeController : SceneChangeController {
        private bool isActive = false;
        private bool showProgress = false;
        private AsyncOperation currentLoadOperation = null;
        [SerializeField] private SceneChangeControllerVariable reference = null;
        [SerializeField] private Image fillImage = null;
        [SerializeField] private GameObject rootObject = null;

        private void Awake() {
            ResetParameters();
            reference.Value = this;
        }

        private void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
        }

        private void ResetParameters() {
            isActive = false;
            showProgress = false;
            if (fillImage)
                fillImage.fillAmount = 0;
            currentLoadOperation = null;
        }

        private void Update() {
            if (!isActive || currentLoadOperation == null)
                return;

            if (fillImage)
                fillImage.fillAmount = showProgress ? currentLoadOperation.progress / 0.9f : 0;
        }

        public override void Activate() {
            ResetParameters();
            rootObject.SetActive(true);
        }

        public override void Deactivate() {
            ResetParameters();
            rootObject.SetActive(false);
        }

        public override void ManageSceneLoadOperation(AsyncOperation loadOperation) {
            if (isActive) {
                Debug.LogWarning("[BasicSceneChangeController]: Tried to manage a new load operation while already active");
                return;
            }
            currentLoadOperation = loadOperation;
            isActive = true;
            showProgress = true;
        }

        public override void ManageSceneUnloadOperation(AsyncOperation unloadOperation) {
            if (isActive) {
                Debug.LogWarning("[BasicSceneChangeController]: Tried to manage a new unload operation while already active");
                return;
            }
            currentLoadOperation = unloadOperation;
            isActive = true;
            showProgress = false;
        }
    }
}
