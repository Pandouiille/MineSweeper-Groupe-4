using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NbMinesLeft : MonoBehaviour
{
    public Text NbMinesLeftText;

    [SerializeField] GameObject _game;
    MineSweeper mine;

    // Start is called before the first frame update
    void Start()
    {
        mine = _game.GetComponent<MineSweeper>();
    }

    // Update is called once per frame
    void Update()
    {
        NbMinesLeftText.text = mine.GetNbMinesLeft().ToString();
    }
}
