using UnityEngine;

namespace Doryu.Dialogue
{
    //TextEffector는 Text의 mesh를 건드는 작업을 해주는 칭구입니다. 다른건 하지 마십쇼
    public class TextEffector
    {
        public virtual void Init(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors) { }
        public virtual void Update(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors) { }
    }
}