using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;
    public void GameStart()
    {
        SceneManager.LoadScene("PlayScene_Door1");
    }
    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
