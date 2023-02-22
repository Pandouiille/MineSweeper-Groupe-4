using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeper : MonoBehaviour
{
    public int GridSize = 10;
    public int NbMines = 10;

    public Sprite HiddenCase;
    public Sprite MineCase;

    public Sprite[] ProxyMinesCaseSprite;

    private int[,] Grid;
    private bool[,] RevealedCases;
    private bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[GridSize,GridSize];
        RevealedCases= new bool[GridSize,GridSize];

        InitGrid();
        PlaceMines();
        CreateSpriteGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver && Input.GetMouseButtonDown(0))
        {
            GameOver = false;
            InitGrid();
            PlaceMines();
            UpdateGrid();
        }
        if (!GameOver && Input.GetMouseButtonDown(0)) 
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(MousePos.x);
            int y = Mathf.RoundToInt(MousePos.y);

            if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
            {
                RevealCase(x, y);
                UpdateGrid();

                if (Grid[x,y] == -1)
                {
                    GameOver = true;
                    DisplayMines();
                }
                else if (CaseIsSafe())
                {
                    GameOver = true;
                    DisplayMines();
                    Debug.Log("GG");
                }
            }
        }
    }

    private void InitGrid()
    {
        Grid = new int[GridSize,GridSize];
        RevealedCases= new bool[GridSize,GridSize];

        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Grid[i, j] = 0;
                RevealedCases[i, j] = false;
            }
        }
    }

    private void PlaceMines()
    {
        int MinePlaced = 0;
        while (MinePlaced < NbMines)
        {
            int x = Random.Range(0,GridSize);
            int y = Random.Range(0,GridSize);

            if (Grid[x,y] != -1)
            {
                Grid[x,y] = -1;
                MinePlaced++;
            }
        }
        SetProxyMines();
    }

    private void SetProxyMines()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (Grid[i,j] != -1)
                {
                    int NbMinesProxy = 0;
                    for (int k = i -1; k <= i; k++)
                    {
                        for (int l = j - 1; l <= j + 1 ; l++)
                        {
                            if (k >= 0 && k < GridSize && l >= 0 && k < GridSize)
                            {
                                NbMinesProxy++;
                            }
                        }
                    }
                    Grid[i, j] = NbMinesProxy;
                }
            }
        }
    }

    private void CreateSpriteGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                GameObject CaseObject = new GameObject("Case_" + i + "_" + j);
                //CaseObject.transform.parent = transform;
                CaseObject.transform.position = new Vector3(i, j, 0);

                SpriteRenderer Render = CaseObject.AddComponent<SpriteRenderer>();
                Render.sprite = HiddenCase;

                BoxCollider2D boxCollid = CaseObject.AddComponent<BoxCollider2D>();
                boxCollid.size = new Vector2(1, 1);

                Case CaseSprite = CaseObject.AddComponent<Case>();
                CaseSprite.x = i;
                CaseSprite.y = j;
                CaseSprite.HiddenCase = HiddenCase;
                CaseSprite.MineCase = MineCase;
                CaseSprite.ProxyMinesCaseSprite = ProxyMinesCaseSprite;
                CaseSprite.MineSweeper = this;
            }
        }
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (RevealedCases[i,j])
                {
                    SpriteRenderer Render = transform.Find("Case(" + i + "," + j + ")").GetComponent<SpriteRenderer>();
                    if (Grid[i,j] == -1)
                    {
                        Render.sprite = MineCase;
                    }
                    else
                    {
                        Render.sprite = ProxyMinesCaseSprite[Grid[i,j]];
                    }
                }
            }
        }
    }
    
    private void RevealCase(int x, int y)
    {
        if (!RevealedCases[x, y])
        {
            RevealedCases[x, y] = true;

            if (Grid[x,y] == 0)
            {
                for (int i = x - 1 ; i < x + 1; i++)
                {
                    for (int j = y - 1; j < y + 1; j++)
                    {
                        if (i >= 0 && i < GridSize && j >= 0 && j < GridSize)
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
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (Grid[i,j] == -1)
                {
                    RevealedCases[i,j] = true;
                }
            }
        }
        UpdateGrid();
    }

    private bool CaseIsSafe()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (!RevealedCases[i,j] && Grid[i,j] != -1)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
