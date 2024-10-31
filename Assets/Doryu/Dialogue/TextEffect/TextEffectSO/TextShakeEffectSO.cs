using Doryu.Dialogue;
using UnityEngine;

[CreateAssetMenu(fileName = "TextShakeEffectSO", menuName = "SO/Dialogue/TextEffect/TextShakeEffect")]
public class TextShakeEffectSO : TextEffectSO
{
    [SerializeField] private string _applyRangeText;
    [SerializeField] private float _shakePower;
    [SerializeField] private float _shakeSpeed;

    private TextEffectMachine _textMachine;
    private string _trimText;
    private string _trimApplyRangeText;

    public override void EffectStart(TextElementNodeSO textElementNodeSO)
    {
        _textMachine = textElementNodeSO.OwnerNodeSO.TextMachine;
        Debug.Log(_textMachine.Text);
        _trimText = _textMachine.Text.Replace(" ", "");
        _trimApplyRangeText = _applyRangeText.Replace(" ", "");

        int startIdx = _trimText.IndexOf(_trimApplyRangeText);
        int endIdx = startIdx + _trimApplyRangeText.Length;

        Debug.Log(_trimText);

        for (int idx = startIdx; idx < endIdx; idx++)
        {
            _textMachine.GetEffector<TextShakeEffector>().AddShakeIndex(idx * 4, _shakePower, _shakeSpeed);
        }
    }
}