using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool pause = false;

    public void SetPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
