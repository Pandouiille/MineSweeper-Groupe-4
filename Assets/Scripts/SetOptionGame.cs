using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetOptionGame : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _GridSizeLabel;
    [SerializeField] private TextMeshProUGUI _NbrMinesLabel;

    public void PassNbrMine(float nbr)
    {
        OptionGame.NbrMines = (int)nbr;
        DisplayNbrMines((int)nbr);
    }

    public void PassGridSize(float size)
    {
        OptionGame.SizeGrid = (int)size;
        DisplayGridSize((int)size);
    }

    public void PassIsTimer(bool isTimer)
    {
        OptionGame.IsTimer = !OptionGame.IsTimer;
    }

    public void Launch()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void DisplayGridSize(int size) => _GridSizeLabel.text = size.ToString();

    public void DisplayNbrMines(int size) => _NbrMinesLabel.text = size.ToString();
}
