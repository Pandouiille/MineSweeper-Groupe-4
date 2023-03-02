using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject SettingsWindow;
    // Start is called before the first frame update
    public void whenButtonClick()
    {
        if (SettingsWindow.activeInHierarchy == true)
            SettingsWindow.SetActive(false);
        else
            SettingsWindow.SetActive(true);
    }
}
