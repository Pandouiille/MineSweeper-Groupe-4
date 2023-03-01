using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class OptionGame
{
    private static int _nbrMines = 1;
    private static int _sizeGrid = 2;
    private static bool _isTimer = false;

    public static int NbrMines { get => _nbrMines; set =>_nbrMines = value; }

    public static int SizeGrid { get => _sizeGrid; set => _sizeGrid = value; }

    public static bool IsTimer { get => _isTimer; set => _isTimer = value; }
}
