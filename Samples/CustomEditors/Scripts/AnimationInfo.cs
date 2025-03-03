using System;
using UnityEngine;

namespace Toblerone.Toolbox.CustomEditorsSample {
    [Serializable]
    public class AnimationInfo {
        [SerializeField] private float duration;
        public float Duration => duration;
        [SerializeField] private Sprite[] sprites;
        public Sprite GetSprite(float time) {
            if (sprites == null || sprites.Length < 1)
                return null;
            if (time < 0)
                time += Mathf.CeilToInt(-time / duration) * duration;
            time = Mathf.Repeat(time, duration) / duration;
            return sprites[Mathf.Min(Mathf.FloorToInt(time * sprites.Length), sprites.Length - 1)];
        }
    }
}
