using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour
{
    public Button toggleButton;
    public GameObject objectToToggle;

    bool toggleActive;

    // Start is called before the first frame update
    void Start()
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
