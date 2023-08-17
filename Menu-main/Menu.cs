using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void BackGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void VideoOptions()
    {
        SceneManager.LoadScene("VideoOptions");
    }

    public void AudioOptions()
    {
        SceneManager.LoadScene("AudioOptions");
    }

    public void ControlOptions()
    {
        SceneManager.LoadScene("ControlOptions");
    }
}
