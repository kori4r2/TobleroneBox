namespace Toblerone.Toolbox {
    public interface IVariableObserver<T> {
        void OnValueChanged(T newValue);
    }
}
