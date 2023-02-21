using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid()
    {
        for (int i = 1; i < 10; i+=2)
        {
            for (int j = 1; j < 10; j+=2)
            {
                Instantiate(tile, new Vector2(i, j), Quaternion.identity);
            }
        }
    }
}
