using UnityEngine;

namespace Toblerone.Toolbox {
    public class ScenePicker : MonoBehaviour {
        [SerializeField] private string sceneName = "";
        [SerializeField] private string assetPath = "";
        [SerializeField] private string guid = "";
        public string Name { get => sceneName; private set => sceneName = value; }
        public string Path { get => assetPath; private set => assetPath = value; }
        public string GUID { get => guid; private set => guid = value; }

        public ScenePicker() {
            sceneName = "";
            assetPath = "";
            guid = "";
        }

        public ScenePicker(ScenePicker other) {
            CopyValues(other);
        }

        public void CopyValues(ScenePicker other) {
            sceneName = other.sceneName;
            assetPath = other.assetPath;
            guid = other.guid;
        }
    }
}
