using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _GameOverMenu;
    [SerializeField] GameObject _game;
    MineSweeper mine;

    void Start()
    {
        mine = _game.GetComponent<MineSweeper>();
    }

    public void Quit()
    {
        _GameOverMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");

        mine.SetPause(false);
    }

    public void Reset()
    {
        _GameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mine.SetPause(false);
    }
}
