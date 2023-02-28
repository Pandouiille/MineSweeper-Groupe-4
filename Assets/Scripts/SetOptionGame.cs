using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetOptionGame : MonoBehaviour 
{ 
    public void PassNbrMine(float nbr)
    {
        OptionGame.GetNbrMines((int)nbr);
    }

    public void PassGridSize(float size)
    {
        OptionGame.GetSizeGrid((int)size);
    }

    public void Launch()
    {
        Debug.Log("Launch");
        SceneManager.LoadScene("MineSweeper");
    }
}
