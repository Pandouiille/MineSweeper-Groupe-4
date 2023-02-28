using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using Mono.Cecil;

public class MainMenu : MonoBehaviour
{
    private GameObject instance;

    // Start is called before the first frame update
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OptionsButton()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToogleFullScreen(bool isFullScreen)
    {
            Screen.fullScreen = isFullScreen;
    }
}
