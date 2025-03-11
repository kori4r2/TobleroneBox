using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObjectManagerInstantiated : UpdateManager<SpinningObjectRuntimeOnly> {
        private IRuntimeSet<SpinningObjectRuntimeOnly> runtimeSet = null;
        protected override IRuntimeSet<SpinningObjectRuntimeOnly> RuntimeSet { get => runtimeSet; set => runtimeSet = value; }
    }
}
