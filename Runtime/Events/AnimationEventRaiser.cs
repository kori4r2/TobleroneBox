using UnityEngine;

namespace Toblerone.Toolbox {
    public class AnimationEventRaiser : MonoBehaviour {
        public void RaiseEvent(EventSO eventToRaise) {
            if (eventToRaise)
                eventToRaise.Raise();
        }
    }
}
