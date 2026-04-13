using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;
    참조 0개
    public void GameStart()
    {
        SceneManager.LoadScene("PlayScene_Door1");
    }
    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    // Update is called once per frame
    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
