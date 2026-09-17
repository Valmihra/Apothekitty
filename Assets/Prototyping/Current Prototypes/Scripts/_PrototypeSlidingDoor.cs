using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PrototypeSlidingDoor : MonoBehaviour
{
    [SerializeField] private Slider slidingDoor;
    [SerializeField] private GameObject interactionBlocker;
    // private int openValue = 0;
    private int closedValue = 1;
    
    void Awake()
    {
        // slidingDoor.onValueChanged.AddListener(delegate {OnSliderValueChanged(); });
        slidingDoor.value = closedValue;
    }
    
    /*void OnSliderValueChanged()
    {
        // Might not even have to do this if hover scripts don't interfere when dragging slider~~
        if (slidingDoor.value > 0.08)
        {
            interactionBlocker.SetActive(true);
        }
        else
        {
            interactionBlocker.SetActive(false);
        }

        // Debug.Log(interactionBlocker.activeInHierarchy);
    }*/
}
