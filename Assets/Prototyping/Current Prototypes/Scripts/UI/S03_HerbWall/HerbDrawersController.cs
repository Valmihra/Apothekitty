using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbDrawersController : MonoBehaviour
{
    private List<DrawerSensor> allHerbDrawers;

    private static HerbDrawersController _instance;
    public static HerbDrawersController Instance
    {
        get
        {
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
    }

    void SetupList()
    {
        allHerbDrawers = new List<DrawerSensor>();
        // Debug.Log("Herb Drawers are resetting, list currently contains " + allHerbDrawers.Count + " drawers.");
        foreach (Transform child in transform)
        {
            DrawerSensor temp = child.GetComponent<DrawerSensor>();       // switch to trygetcomponent?
            if (temp != null)
            {
                allHerbDrawers.Add(temp);
            }
        }
        // Debug.Log("Reset complete. There are " + allHerbDrawers.Count + " drawers to close.");
    }

    public void ResetHerbDrawerIcons()
    {
        SetupList();
        foreach (DrawerSensor d in allHerbDrawers)
        {
            d.SetupHerbWall();
        }
    }
}
