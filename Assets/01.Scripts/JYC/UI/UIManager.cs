using System.Net.NetworkInformation;
using System.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _bulidCanvas;
    [SerializeField] private Canvas _defaultCanvas;
    [SerializeField] private Canvas _dieCanvas;
    [SerializeField] private Canvas _upgradeCanvas;

    [SerializeField] private Image _escPanel;
    [SerializeField] private Image _unitManagementPanel;
    [SerializeField] private Image _unitInfomationPanel;

    private bool _isBulidCanvas = false;
    private bool _isEscPanel = false;
    private bool _isDie = false;
    private bool _isUpgrade = false;

    public void BulidCanvas(bool state)
    {
        if (_isEscPanel || _isDie || _isUpgrade)
            return;

        _bulidCanvas.enabled = state;
        _isBulidCanvas = state;
        _defaultCanvas.enabled = !state;
    }

    public void EscPanel(bool state)
    {
        if (_isBulidCanvas || _isDie || _isUpgrade)
            return;

        _escPanel.gameObject.SetActive(state);
        _isEscPanel = state;
    }

    public void UpgradeCanvas(bool state)
    {
        if (_isBulidCanvas || _isEscPanel || _isDie)
            return;

        _upgradeCanvas.enabled = state;
        _isBulidCanvas = state;
    }


    public void UnitManagementPanelOn(string name, string description, Sprite animalSprite)
    {
        _unitManagementPanel.gameObject.SetActive(true);

        Transform childImage = _unitInfomationPanel.transform.Find("Image_Unit");
        Transform childName = _unitInfomationPanel.transform.Find("Text_UnitName");
        Transform childDescription = _unitInfomationPanel.transform.Find("Text_UnitDescription");

        if (childImage != null)
        {
            Image imageComponent = childImage.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = animalSprite;
            }
        }
        
        if (childName != null)
        {
            TextMeshProUGUI nameComponent = childName.GetComponent<TextMeshProUGUI>();
            if (nameComponent != null)
            {
                nameComponent.text = name;
            }
        }
        
        if (childDescription != null)
        {
            TextMeshProUGUI descriptionComponent = childDescription.GetComponent<TextMeshProUGUI>();
            if (descriptionComponent != null)
            {
                descriptionComponent.text = description;
            }
        }
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

    public void DieCanvas()
    {
        _dieCanvas.enabled = true;
        _isDie = true;
    }

    public void Build()
    {
        // 건물을 설치하는 화면으로 이동
        Debug.Log("되나?");
    }

    public void ReTry()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
