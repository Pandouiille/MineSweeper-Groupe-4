using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NbMinesLeft : MonoBehaviour
{
    [SerializeField] GameObject _game;

    MineSweeper mine;

    public Text nbMinesLeft;

    private void Start()
    {
        mine = _game.GetComponent<MineSweeper>();
        DisplayNbMinesLeft();
    }

    private void Update()
    {
        DisplayNbMinesLeft();
    }

    void DisplayNbMinesLeft()
    {
        nbMinesLeft.text = mine.GetNbMinesLeft().ToString();
    }
}
