using Doryu.Dialogue;
using UnityEngine;

[CreateAssetMenu(fileName = "TextScaleEffectSO", menuName = "SO/Dialogue/TextEffect/TextScaleEffect")]
public class TextScaleEffectSO : TextEffectSO
{
    [SerializeField] private string _targetText;
    [SerializeField] private float _fontScale = 1f;

    public override void EffectStart(TextElementNodeSO textElementNodeSO)
    {
        textElementNodeSO.fontScaleDict.Add(_targetText, _fontScale);
    }
}