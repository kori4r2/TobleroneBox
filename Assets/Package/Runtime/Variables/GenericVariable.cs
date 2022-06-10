using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class GenericVariable<T> : ScriptableObject {
        private List<IVariableObserver<T>> observers = new List<IVariableObserver<T>>();
        [SerializeField] private T value;

        public T Value {
            get => value;
            set {
                this.value = value;
                NotifyObservers();
            }
        }

        private void NotifyObservers() {
            foreach (IVariableObserver<T> observer in observers) {
                if (observer != null)
                    observer.OnValueChanged(Value);
            }
        }

        public void AddObserver(IVariableObserver<T> newObserver) {
            if (!observers.Contains(newObserver)) {
                observers.Add(newObserver);
            }
        }

        public void RemoveObserver(IVariableObserver<T> newObserver) {
            if (observers.Contains(newObserver)) {
                observers.Remove(newObserver);
            }
        }
    }
}
