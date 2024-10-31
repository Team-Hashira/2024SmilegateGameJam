using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] string _titleName;

    public void StartGame()
    {
        SceneManager.LoadScene(_titleName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
