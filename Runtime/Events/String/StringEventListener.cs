using System;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [Serializable]
    public class StringEventListener : GenericEventListener<string> {
        public StringEventListener(GenericEvent<string> eventToListen, UnityAction<string> response) : base(eventToListen, response) { }
    }
}
