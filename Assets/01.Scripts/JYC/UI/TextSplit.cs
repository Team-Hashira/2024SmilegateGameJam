using TMPro;
using UnityEngine;

public class TextSplit : MonoBehaviour
{
    private int _count; // 값 가져오기
    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _tmpText.text = AddCommas(_tmpText.text);
    }

    private string AddCommas(string input)
    {
        if (long.TryParse(input, out long number))
        {
            return string.Format("{0:N0}", number);
        }
        return input;
    }
}
