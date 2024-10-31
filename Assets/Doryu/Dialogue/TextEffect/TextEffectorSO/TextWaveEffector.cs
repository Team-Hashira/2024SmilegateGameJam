using Doryu.Dialogue;
using System.Collections.Generic;
using UnityEngine;

public class TextWaveEffector : TextEffector
{
    public List<TextMoveData> waveDatas = new List<TextMoveData>();

    public void AddWaveIndex(int index, float power, float speed)
    {
        TextMoveData textShakeData = new TextMoveData()
        {
            index = index,
            power = power,
            speed = speed,
        };
        waveDatas.Add(textShakeData);
    }

    public override void Update(Vector3[] startVertices, Color[] startColors, ref Vector3[] vertices, ref Color[] colors)
    {
        for (int idx = 0; idx < waveDatas.Count; idx++)
        {
            TextMoveData waveData = waveDatas[idx];

            if (waveData.index + 3 >= vertices.Length) break;

            Vector3 waveVec = Vector3.up * Mathf.Sin((Time.time + waveData.index * 0.15f) * waveData.speed) * waveData.power;
            for (int i = 0; i < 4; i++)
            {
                vertices[waveData.index + i] += waveVec;
            }
        }
    }
}
