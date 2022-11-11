using UnityEngine;
using UnityEngine.EventSystems;

namespace Toblerone.Toolbox {
    public class RaiseEventOnRectChange : UIBehaviour {
        [SerializeField] private EventSO eventToRaise;
        protected override void OnRectTransformDimensionsChange() {
            base.OnRectTransformDimensionsChange();
            if (Application.isPlaying && eventToRaise)
                eventToRaise.Raise();
        }
    }
}
