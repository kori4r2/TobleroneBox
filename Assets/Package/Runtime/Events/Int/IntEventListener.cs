using System;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [Serializable]
    public class IntEventListener : GenericEventListener<int> {
        public IntEventListener(GenericEvent<int> eventToListen, UnityAction<int> response) : base(eventToListen, response) { }
    }
}
