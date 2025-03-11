using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObjectManager : UpdateManager<SpinningObject> {
        [SerializeField] private SpinningObjectRuntimeSet runtimeSet = null;
        protected override IRuntimeSet<SpinningObject> RuntimeSet {
            get => runtimeSet;
            set {
                if (value != null && !(value is SpinningObjectRuntimeSet))
                    return;
                runtimeSet = value as SpinningObjectRuntimeSet;
            }
        }
    }
}
