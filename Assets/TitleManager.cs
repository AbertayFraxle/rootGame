using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject settingsScreen;

    public void startGame()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void settingsPanelState(bool open)
    {
        settingsScreen.SetActive(open);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
