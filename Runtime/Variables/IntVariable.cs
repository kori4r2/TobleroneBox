using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/Variable/IntVariable")]
    public class IntVariable : GenericVariable<int> {
        public void IncrementValue(int increment) {
            Value += increment;
        }

        public void DecrementValue(int decrement) {
            Value -= decrement;
        }

        public void MultiplyValue(int factor) {
            Value *= factor;
        }

        public void DivideValue(int divisor) {
            Value /= divisor;
        }
    }
}
