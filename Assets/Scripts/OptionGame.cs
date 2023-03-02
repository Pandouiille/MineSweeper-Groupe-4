using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class OptionGame
{
    private static int _nbrMines = 1;
    private static int _sizeGrid = 2;
    private static bool _isTimer = false;

    static public int NbrMines { get => _nbrMines; set => _nbrMines = value; }

    static public int GridSize { get => _sizeGrid; set => _sizeGrid = value; }

    static public bool IsTimer { get => _isTimer; set => _isTimer = value; }
}
