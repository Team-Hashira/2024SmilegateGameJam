using UnityEngine;

namespace Doryu.Dialogue
{
    //TextEffector�� Text�� mesh�� �ǵ�� �۾��� ���ִ� Ī���Դϴ�. �ٸ��� ���� ���ʼ�
    public class TextEffector
    {
        public virtual void Init(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors) { }
        public virtual void Update(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors) { }
    }
}