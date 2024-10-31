using Doryu.Dialogue;
using UnityEngine;

[CreateAssetMenu(fileName = "TextWaveEffectSO", menuName = "SO/Dialogue/TextEffect/TextWaveEffect")]
public class TextWaveEffectSO : TextEffectSO
{
    [SerializeField] private string _applyRangeText;
    [SerializeField] private float _wavePower;
    [SerializeField] private float _waveSpeed;

    private TextEffectMachine _textMachine;
    private string _trimText;
    private string _trimApplyRangeText;

    public override void EffectStart(TextElementNodeSO textElementNodeSO)
    {
        _textMachine = textElementNodeSO.OwnerNodeSO.TextMachine;
        _trimText = textElementNodeSO.GetText().Replace(" ", "");
        _trimApplyRangeText = _applyRangeText.Replace(" ", "");

        int startIdx = _trimText.IndexOf(_trimApplyRangeText);
        int endIdx = startIdx + _trimApplyRangeText.Length;

        for (int idx = startIdx; idx < endIdx; idx++)
        {
            _textMachine.GetEffector<TextWaveEffector>().AddWaveIndex(idx * 4, _wavePower, _waveSpeed);
        }
    }
}