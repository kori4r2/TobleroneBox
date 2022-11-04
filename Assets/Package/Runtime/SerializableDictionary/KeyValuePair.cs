namespace Toblerone.Toolbox {
    [System.Serializable]
    public struct KeyValuePair<Tkey, TValue> {
        public Tkey key;
        public TValue value;

        public KeyValuePair(Tkey newKey, TValue newValue) {
            key = newKey;
            value = newValue;
        }
    }
}
