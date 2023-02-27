using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class OptionGame
{
    public static int _nbrMines = 1;
    public static int _sizeGrid = 2;

    static public void GetNbrMines(int nbrMines)
    {
        _nbrMines = nbrMines;
        Debug.Log(_nbrMines);
    }

    static public void GetSizeGrid(int sizeGrid)
    {
        _sizeGrid = sizeGrid;
        Debug.Log(_sizeGrid);
    }
}
