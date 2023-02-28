using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _game;
    MineSweeper mine;

    private void Start()
    {
        mine = _game.GetComponent<MineSweeper>();
    }

    public void SetPause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        mine.SetPause(true);
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        mine.SetPause(false);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
