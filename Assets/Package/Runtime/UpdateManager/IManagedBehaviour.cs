namespace Toblerone.Toolbox {
    public interface IManagedBehaviour {
        public bool ShouldUpdate { get; }
        public abstract void ManagedUpdate(float deltaTime);
    }
}
