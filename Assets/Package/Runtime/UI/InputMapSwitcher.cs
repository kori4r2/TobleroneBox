using UnityEngine;
using UnityEngine.InputSystem;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/InputMapSwitcher")]
    public class InputMapSwitcher : ScriptableObject {
        [SerializeField] private InputActionAsset inputs;
        [SerializeField] private string actionMapName;

        public void SwitchToMap() {
            InputActionMap activeMap = null;
            foreach (InputActionMap map in inputs.actionMaps) {
                if (map.name == actionMapName)
                    activeMap = map;
                else
                    map.Disable();
            }
            if (activeMap != null)
                activeMap.Enable();
            else
                Debug.LogWarning($"[InputMapSwitcher]: Action map {actionMapName} was not found in InputActionAsset {inputs.name}");
        }
    }
}
