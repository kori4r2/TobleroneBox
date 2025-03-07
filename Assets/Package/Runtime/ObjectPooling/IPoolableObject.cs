using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public interface IPoolableObject {
        public GameObject GameObject { get; }
        public void SetDespawnCallback(UnityAction<IPoolableObject> callback);
        public void Despawn();
        public void ResetObject();
    }
}
