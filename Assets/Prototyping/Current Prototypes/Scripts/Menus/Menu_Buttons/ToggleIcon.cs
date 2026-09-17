using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour
{
    public Button toggleButton;
    public GameObject objectToToggle;

    bool toggleActive;

    // larger version would probably want a toggle manager to handle all of them, right? idk bruhhhh
    /*void Start()
    {
        toggleActive = false;
        toggleButton.onClick.AddListener( delegate { Toggle(); });
        objectToToggle.SetActive(false);
    }*/
    
    // public void Toggle()

    public void InitialiseToggleIcon()
    {
        toggleActive = false;
        toggleButton.onClick.AddListener( delegate { Toggle(); });
        objectToToggle.SetActive(false);
    }

    void Toggle()
    {
        if (toggleActive)
        {
            objectToToggle.SetActive(false);
            toggleActive = false;
        }
        else
        {
            objectToToggle.SetActive(true);
            toggleActive = true;
        }
    }
}
