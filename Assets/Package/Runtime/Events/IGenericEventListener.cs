namespace Toblerone.Toolbox {
    public interface IGenericEventListener<T> {
        void OnEventRaised(T value);
    }
}
