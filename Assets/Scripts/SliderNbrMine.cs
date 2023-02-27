using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNbrMine : MonoBehaviour
{
    public void SetMaxNbrMine(float gridSize)
    {
        GetComponent<Slider>().maxValue = gridSize * gridSize - 1;
    }
}
