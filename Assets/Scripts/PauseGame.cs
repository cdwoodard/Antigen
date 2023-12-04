using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PausedMessage;
    public GameObject UnPauseButton;

    public void PauseTheGame()
    {
        PausePanel.SetActive(true);
        PausedMessage.SetActive(true);
        UnPauseButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        PausedMessage.SetActive(false);
        UnPauseButton.SetActive(false);
        Time.timeScale = 1;
    }
}
