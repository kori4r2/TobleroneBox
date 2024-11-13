using System;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [Serializable]
    public class FloatEventListener : GenericEventListener<float> {
        public FloatEventListener(GenericEvent<float> eventToListen, UnityAction<float> response) : base(eventToListen, response) { }
    }
}
