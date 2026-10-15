using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbDrawersController : MonoBehaviour
{
    // this script could be an email
    // *TAG* - girl what do i do about this.
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
            if (child.TryGetComponent<DrawerSensor>(out DrawerSensor temp))
            {
                allHerbDrawers.Add(temp);
            }
            
            /*DrawerSensor temp = child.GetComponent<DrawerSensor>();       // switch to trygetcomponent?
            if (temp != null)
            {
                allHerbDrawers.Add(temp);
            }*/
        }
    }

    public void ResetHerbDrawerIcons()
    {
        SetupList();
        // int drawerNumber = DayManager.Instance.currentDayNumber == 0 ? 3 : DayManager.Instance.currentDayNumber == 1 ? 7 : DayManager.Instance.currentDayNumber == 2 ? 
        /*foreach (DrawerSensor d in allHerbDrawers)
        {
            d.SetupHerbWall();
        }*/
        for (int i = 0; i < allHerbDrawers.Count; i++)
        {
            DrawerSensor d = allHerbDrawers[i];
            d.SetupHerbWall();

            if (DayManager.Instance.currentDayNumber == 0 || DayManager.Instance.currentDayNumber == 1)
            {
                int drawerNumber = DayManager.Instance.currentDayNumber == 0 ? 3 : 7;

                if (i > drawerNumber)
                {
                    allHerbDrawers[i].DisableDrawer();
                }
            }
        }
    }
}
