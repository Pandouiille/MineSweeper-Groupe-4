using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineSweeper : MonoBehaviour
{
    [SerializeField] GameObject _GameOverMenu;
    [SerializeField] GameObject _WinMenu;

    [SerializeField] private int _GridSize;
    [SerializeField] private int _NbMines;
    [SerializeField] private int _NbMinesLeft;

    [SerializeField] private Sprite _HiddenCase;
    [SerializeField] private Sprite _MineCase;
    [SerializeField] private Sprite _FlagCase;

    [SerializeField] private Sprite[] _ProxyMinesCaseSprite;

    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audio;

    private int[,] _Grid;
    private bool[,] _RevealedCases;
    private bool[,] _CasesWithFlags;
    private bool _GameOver = true;
    private bool _isPaused = false;

    private int clicked = 0;
    private float clicktime = 0;
    private float clickdelay = 0.35f;

    // Start is called before the first frame update
    void Start()
    {        
        _GridSize = OptionGame.GridSize;
        _NbMines = OptionGame.NbrMines;
        _NbMinesLeft = _NbMines;
        _audio = GetComponent<AudioSource>();
        //Debug.Log($"{_GridSize} / {_NbMines} / {_NbMinesLeft}");
        InitGrid();
        PlaceMines();
        CreateSpriteGrid();

    }

    // Update is called once per frame
    void Update()
    {
        if (_isPaused == true)
        {
            return;
        }
        if (_GameOver && Input.GetMouseButtonDown(0))
        {
            _NbMinesLeft = _NbMines;
            _GameOver = false;
            InitGrid();
            PlaceMines();
            UpdateGrid();
        }

        if (DoubleClick())
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(MousePos.x);
            int y = Mathf.RoundToInt(MousePos.y);

            if (x >= 0 && x < _GridSize && y >= 0 && y < _GridSize && _RevealedCases[x, y])
            {
                int flagCounter = 0;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (_CasesWithFlags[i,j])
                        {
                            flagCounter += 1;
                        }
                    }
                }

                if (flagCounter == _Grid[x,y])
                {
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            RevealCase(i, j);
                            if (_Grid[i, j] == -1 && !_CasesWithFlags[i,j])
                            {
                                _GameOver = true;
                                DisplayMines();
                                _audio.PlayOneShot(_audioClip, 1);
                                Debug.Log("Boom");
                            }
                            else if (CaseIsSafe())
                            {
                                _GameOver = true;
                                DisplayMines();
                                Debug.Log("GG");
                            }
                        }
                    }

                    UpdateGrid();
                }
            }
        }

        if (!_GameOver && Input.GetMouseButtonDown(0)) 
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(MousePos.x);
            int y = Mathf.RoundToInt(MousePos.y);

            if (x >= 0 && x < _GridSize && y >= 0 && y < _GridSize && !_CasesWithFlags[x,y])
            {
                RevealCase(x, y);
                UpdateGrid();

                if (_Grid[x,y] == -1)
                {
                    _GameOver = true;
                    DisplayMines();
                    _audio.PlayOneShot(_audioClip, 1);
                    SetPause(true);
                    _GameOverMenu.SetActive(true);
                }
                else if (CaseIsSafe())
                {
                    _GameOver = true;
                    DisplayMines();
                    SetPause(true);
                    _WinMenu.SetActive(true);
                }
            }
        }

        if (!_GameOver && Input.GetMouseButtonDown(1))
        {

            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(MousePos.x);
            int y = Mathf.RoundToInt(MousePos.y);

            if (x >= 0 && x < _GridSize && y >= 0 && y < _GridSize)
            {
                PutFlagOnTile(x, y);
                UpdateGrid();
                if (CaseIsSafe())
                {
                    _GameOver = true;
                    DisplayMines();
                    SetPause(true);
                    _WinMenu.SetActive(true);
                }
            }  
        }
    }

    private void InitGrid()
    {
        _Grid = new int[_GridSize,_GridSize];
        _RevealedCases= new bool[_GridSize,_GridSize];
        _CasesWithFlags = new bool[_GridSize, _GridSize];

        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                _Grid[i, j] = 0;
                _RevealedCases[i, j] = false;
            }
        }
        Camera.main.transform.position = new Vector3((float)_GridSize / 2 - .5f, (float)_GridSize / 2 - .5f,  -1);
        Camera.main.orthographicSize = (float)_GridSize / 2;
    }

    private void PlaceMines()
    {
        int MinePlaced = 0;
        while (MinePlaced < _NbMines)
        {
            int x = Random.Range(0,_GridSize);
            int y = Random.Range(0,_GridSize);

            if (_Grid[x,y] != -1)
            {
                _Grid[x,y] = -1;
                MinePlaced++;
            }
        }
        SetProxyMines();
    }

    private void SetProxyMines()
    {
        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                if (_Grid[i,j] != -1)
                {
                    int NbMinesProxy = 0;
                    for (int k = i -1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1 ; l++)
                        {
                            if (k >= 0 && k < _GridSize && l >= 0 && l < _GridSize)
                            {
                                if (_Grid[k, l] == -1)
                                {
                                    NbMinesProxy++;
                                }
                            }
                        }
                    }
                    _Grid[i, j] = NbMinesProxy;
                }
            }
        }
    }

    private void CreateSpriteGrid()
    {
        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                GameObject CaseObject = new GameObject("Case_" + i + "_" + j);
                CaseObject.transform.position = new Vector3(i, j, 0);

                SpriteRenderer Render = CaseObject.AddComponent<SpriteRenderer>();
                Render.sprite = _HiddenCase;

                BoxCollider2D boxCollid = CaseObject.AddComponent<BoxCollider2D>();

                boxCollid.size = new Vector2(1, 1);
            }
        }
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                if (_CasesWithFlags[i, j])
                {
                    SpriteRenderer Render = GameObject.Find("Case_" + i + "_" + j).GetComponent<SpriteRenderer>();
                    Render.sprite = _FlagCase;
                }
                else if (_RevealedCases[i,j])
                {
                    SpriteRenderer Render = GameObject.Find("Case_" + i + "_" + j).GetComponent<SpriteRenderer>();
                    if (_Grid[i,j] == -1)
                    {
                        Render.sprite = _MineCase;
                    }
                    else
                    {
                        Render.sprite = _ProxyMinesCaseSprite[_Grid[i, j]];
                    }
                }
                else
                {
                    SpriteRenderer Render = GameObject.Find("Case_" + i + "_" + j).GetComponent<SpriteRenderer>();
                    Render.sprite = _HiddenCase;
                }
            }
        }
    }
    
    private void RevealCase(int x, int y)
    {
        if (!_RevealedCases[x, y] && !_CasesWithFlags[x,y])
        {
            _RevealedCases[x, y] = true;

            if (_Grid[x,y] == 0)
            {
                for (int i = x - 1 ; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < _GridSize && j >= 0 && j < _GridSize)
                        {
                            RevealCase(i, j);
                        }
                    }
                }
            }
        }
    }

    private void DisplayMines()
    {
        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                if (_Grid[i,j] == -1)
                {
                    _RevealedCases[i,j] = true;
                }
            }
        }
        UpdateGrid();
    }

    private bool CaseIsSafe()
    {
        for (int i = 0; i < _GridSize; i++)
        {
            for (int j = 0; j < _GridSize; j++)
            {
                if (!_RevealedCases[i,j] && _Grid[i,j] != -1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void PutFlagOnTile(int x, int y)
    {
        if (!_RevealedCases[x,y])
        { 
            _CasesWithFlags[x, y] = !_CasesWithFlags[x, y];
            if (_CasesWithFlags[x, y])
            {
                --_NbMinesLeft;
            }
            else
            {
                ++_NbMinesLeft;
            }
        }
    }

    public void SetPause(bool pause)
    {
        _isPaused = pause;
    }

    public int GetNbMinesLeft()
    {
        return _NbMinesLeft;
    }

    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
}