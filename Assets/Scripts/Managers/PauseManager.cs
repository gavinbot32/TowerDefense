using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool paused;

    public void OnPause()
    {
        if (!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnResume()
    {
        if (paused)
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void TogglePause()
    {
        if(!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
