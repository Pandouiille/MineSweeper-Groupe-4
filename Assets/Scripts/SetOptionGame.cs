using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetOptionGame : MonoBehaviour 
{

    [SerializeField] private TextMeshProUGUI _gridSizeDisplay;
    [SerializeField] private TextMeshProUGUI _nbrMinesDisplay;

    public void PassGridSize(float size)
    {
        OptionGame.GridSize = (int)size;
        DisplayGridSize((int)size);
    }

    public void PassNbrMine(float nbr)
    {
        OptionGame.NbrMines = (int)nbr;
        DisplayNbrMines((int)nbr);
    }

    public void PassIsTimer(bool isTimer)
    {
        OptionGame.IsTimer = isTimer;
    }

    public void DisplayGridSize(int gridSize)
    {
        _gridSizeDisplay.text = gridSize.ToString();
    }

    public void DisplayNbrMines(int nbr)
    {
        _nbrMinesDisplay.text = nbr.ToString();
    }

    public void Launch()
    {
        SceneManager.LoadScene("GameScene");
    }
}
