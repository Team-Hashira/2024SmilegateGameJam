using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _bulidCanvas;
    [SerializeField] private Canvas _defaultCanvas;
    [SerializeField] private Canvas _dieCanvas;

    [SerializeField] private Image _escPanel;
    [SerializeField] private Image _unitManagementPanel;
    [SerializeField] private Image _unitInfomationPanel;

    private bool _isBulidCanvas = false;
    private bool _isEscPanel = false;
    private bool _isDie = false;

    public void BulidCanvas(bool state)
    {
        if (_isEscPanel || _isDie)
            return;

        _bulidCanvas.enabled = state;
        _isBulidCanvas = state;
        _defaultCanvas.enabled = !state;
    }

    public void EscPanel(bool state)
    {
        if (_isBulidCanvas || _isDie)
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

    public void DiePanel()
    {
        _dieCanvas.enabled = true;
        _isDie = true;
    }

    public void Build()
    {
        // 건물을 설치하는 화면으로 이동
        Debug.Log("되나?");
    }

    public void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
