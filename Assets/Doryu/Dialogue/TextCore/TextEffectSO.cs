using UnityEngine;

namespace Doryu.Dialogue
{
    public abstract class TextEffectSO : ScriptableObject
    {
        public virtual void EffectStart(TextElementNodeSO textElementNodeSO) { }
        public virtual void EffectUpdate(int textIdx) { }
        public virtual void EffectEnd() { }
    }

}