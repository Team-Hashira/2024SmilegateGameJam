using UnityEngine;

public class CallUI : MonoBehaviour
{
    private bool _isEsc = false;
    private bool _isBulidCanvas = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_isEsc)
            {
                UIManager.Instance.EscPanel(true);
                _isEsc = true;
            }
            else
            {
                UIManager.Instance.EscPanel(false);
                _isEsc = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!_isBulidCanvas)
            {
                UIManager.Instance.BulidCanvas(true);
                _isBulidCanvas = true;
            }
            else
            {
                UIManager.Instance.BulidCanvas(false);
                _isBulidCanvas = false;
            }
        }
    }
}
