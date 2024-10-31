using Doryu.Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class TextShakeEffector : TextEffector
{
    public List<TextMoveData> shakeDatas = new List<TextMoveData>();

    public void AddShakeIndex(int index, float power, float speed)
    {
        TextMoveData textShakeData = new TextMoveData()
        {
            index = index,
            power = power,
            speed = speed,
        };
        shakeDatas.Add(textShakeData);
    }

    public override void Update(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors)
    {
        for (int idx = 0; idx < shakeDatas.Count; idx++)
        {
            TextMoveData shakeData = shakeDatas[idx];

            if (shakeData.index + 3 >= vertices.Length) break;

            if (1 / shakeData.speed + shakeData.lastMoveTime < Time.time)
            {
                shakeData.lastMoveTime = Time.time;
                shakeDatas[idx] = shakeData;

                Vector3 shakeDir = Random.insideUnitCircle * shakeData.power;
                for (int i = 0; i < 4; i++)
                {
                    vertices[shakeData.index + i] += shakeDir;
                }
            }
        }
    }
}