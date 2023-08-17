using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isOpen = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void OpenMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isOpen = true;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isOpen = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}