using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _bulidCanvas;
    [SerializeField] private Canvas _defaultCanvas;
    [SerializeField] private Image _escPanel;
    [SerializeField] private Image _unitManagementPanel;
    [SerializeField] private Image _unitInfomationPanel;

    private bool _isBulidCanvas = false;
    private bool _isEscPanel = false;

    public void BulidCanvas(bool state)
    {
        if (_isEscPanel)
            return;

        _bulidCanvas.enabled = state;
        _isBulidCanvas = state;
        _defaultCanvas.enabled = !state;
    }

    public void EscPanel(bool state)
    {
        if (_isBulidCanvas)
            return;

        _escPanel.gameObject.SetActive(state);
        _isEscPanel = state;
    }

    public void UnitManagementPanelOn()
    {
        _unitManagementPanel.gameObject.SetActive(true);
    }

    public void UnitManagementPanelOff()
    {
        _unitManagementPanel?.gameObject.SetActive(false);
    }

    public void UnitInfomationPanelOn()
    {
        _unitInfomationPanel.gameObject.SetActive(true);
    }

    public void UnitInfomationPanelOff()
    {
        _unitInfomationPanel.gameObject.SetActive(false);
    }

    public void Build()
    {
        // �ǹ��� ��ġ�ϴ� ȭ������ �̵�
        Debug.Log("�ǳ�?");
    }

    public void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
