using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;
    public GameObject pauseCanvas;
    public GameObject pausePP;
    
    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Pause))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        pausePP.SetActive(!pausePP.activeSelf);
        Time.timeScale = pauseCanvas.activeSelf ? 0 : 1;
        if (pauseCanvas.activeSelf)
        {
            CameraManager.Instance.SetInputModeUI();
        }
        else
        {
            CameraManager.Instance.SetInputModeGameplay();
        }
    }
    
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        pausePP.SetActive(false);
        Time.timeScale = 1;
        CameraManager.Instance.SetInputModeGameplay();
    }
    
    public void QuitMenu()
    {
        Time.timeScale = 1;
        CameraManager.Instance.SetInputModeUI();
        SceneManager.LoadScene("_TitleScreen");
    }
}
