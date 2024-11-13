using System;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [Serializable]
    public class BoolEventListener : GenericEventListener<bool> {
        public BoolEventListener(GenericEvent<bool> eventToListen, UnityAction<bool> response) : base(eventToListen, response) { }
    }
}
