using Doryu.Dialogue;
using UnityEngine;


public class TextVisibleEffector : TextEffector
{
    public int writeDelay;
    public int maxVisibleCharacters;
    private int _visibleCharacters;

    public override void Init(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors)
    {
        _visibleCharacters = 0;
        maxVisibleCharacters = 0;

        for (int i = 0; i < colors.Length; i++)
            colors[i] = new Color(0, 0, 0, 0);
    }

    public override void Update(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors)
    {
        while (_visibleCharacters < maxVisibleCharacters)
        {
            int idx = _visibleCharacters * 4;
            for (int i = 0; i < 4; i++)
            {
                colors[idx + i] = startColors[idx + i];
            }
            _visibleCharacters++;
        }
    }
}