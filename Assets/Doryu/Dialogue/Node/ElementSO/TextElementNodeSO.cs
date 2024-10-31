using Doryu.Dialogue;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextElementNodeSO", menuName = "SO/Dialogue/TextElementNode")]
public class TextElementNodeSO : ElementNodeSO
{
    [TextArea(10, 10)]
    [SerializeField] private string _text;
    [SerializeField] private float _writeDelay = 0.05f;
    [SerializeField] private float _fontScale = 100f;

    public List<TextEffectSO> effectSOList;

    private int _textLength;
    private int _startIndex;
    private float _lastWriteTime;

    private TextVisibleEffector _textVisibleEffector;

    public Dictionary<string, float> fontScaleDict = new Dictionary<string, float>();

    public string GetText(bool isIncludeText = false)
    {
        string tagText = _text;
        if (isIncludeText)
        {
            foreach (string targetText in fontScaleDict.Keys)
            {
                int startIdx = tagText.IndexOf(targetText);
                int endIdx = startIdx + targetText.Length;

                string sizeTag = $"<size={(int)(100 * fontScaleDict[targetText])}%>";
                tagText = tagText.Insert(startIdx, sizeTag);
                tagText = tagText.Insert(endIdx + sizeTag.Length, "</size>");
            }
        }

        return tagText;
    }

    public override void Init(TextNodeSO NodeSO)
    {
        base.Init(NodeSO);

        effectSOList.ForEach(effect => effect.EffectStart(this));
    }

    protected override void Start()
    {
        base.Start();

        _textVisibleEffector = OwnerNodeSO.TextMachine.GetEffector<TextVisibleEffector>();

        _textLength = _text.Replace(" ", "").Length;
        _startIndex = _textVisibleEffector.maxVisibleCharacters;

        _lastWriteTime = 0;
    }

    protected override void End()
    {
        base.End();
        effectSOList.ForEach(effect => effect.EffectEnd());
    }

    protected override void Update()
    {
        base.Update();

        if (_textVisibleEffector.maxVisibleCharacters - _startIndex < _textLength)
        {
            effectSOList.ForEach(effect => effect.EffectUpdate(_textVisibleEffector.maxVisibleCharacters));
            if (_writeDelay + _lastWriteTime < Time.time)
            {
                _textVisibleEffector.maxVisibleCharacters++;
                _lastWriteTime = Time.time;
            }
        }
        else
            Exit();
    }
}