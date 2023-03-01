using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float _timeValue = 120;
    public Text timerText;

    void Update()
    {
        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
        else
        {
            _timeValue += 120;
        }
        DisplayTime(_timeValue);
    }

    void DisplayTime(float TimeToDisplay)
    {
        if (TimeToDisplay < 0)
        {
            TimeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(TimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);

        timerText.text = string.Format("{00:00}:{1:00}",minutes,seconds);
    }
}

//a
