using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/Variable/FloatVariable")]
    public class FloatVariable : GenericVariable<float> {
        public void IncrementValue(float increment) {
            Value += increment;
        }

        public void DecrementValue(float increment) {
            Value -= increment;
        }

        public void MultiplyValue(float factor) {
            Value *= factor;
        }

        public void DivideValue(float divisor) {
            Value /= divisor;
        }
    }
}
