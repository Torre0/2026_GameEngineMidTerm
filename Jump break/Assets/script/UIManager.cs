using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject RankPanel;
    public GameObject HelpPanel;
    public GameObject scoreBorad;


    public void GameStartButtonAction()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenHelpPanel()
    {
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
    }

    public void OpenScorePanel()
	{
		scoreBorad.SetActive(true);
	}

	public void CloseScorePanel()
	{
		scoreBorad.SetActive(false);
    }

    public void OpenRankPanel()
    {
        RankPanel.SetActive(true);
    }

    public void CloseRankPanel()
    {
        RankPanel.SetActive(false);
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameExitButtonAction()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
