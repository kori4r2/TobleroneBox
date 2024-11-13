using System;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [Serializable]
    public class GameObjectEventListener : GenericEventListener<GameObject> {
        public GameObjectEventListener(GenericEvent<GameObject> eventToListen, UnityAction<GameObject> response) : base(eventToListen, response) { }
    }
}
