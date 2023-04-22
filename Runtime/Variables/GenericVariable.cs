using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class GenericVariable<T> : ScriptableObject {
        protected List<IVariableObserver<T>> observers = new List<IVariableObserver<T>>();
        [SerializeField] protected T value;

        public T Value {
            get => value;
            set {
                this.value = value;
                NotifyObservers();
            }
        }

        public void CopyValue(GenericVariable<T> otherVariable) {
            if (!otherVariable)
                return;
            Value = otherVariable.Value;
        }

        public void NotifyObservers() {
            foreach (IVariableObserver<T> observer in observers.ToArray()) {
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
