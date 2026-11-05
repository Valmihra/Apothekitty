using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerSensor : MonoBehaviour
{
    public bool isActive;
    // public bool drawerClosedActive;
    // public bool drawerOpenActive;

    public CanvasGroup openDrawer;
    public CanvasGroup closedDrawer;

    public Button buttonOpen;
    public Button buttonClosed;

    public string drawerContents;

    void Awake()
    {
        openDrawer = transform.Find("Drawer Button - Open").GetComponent<CanvasGroup>();
        closedDrawer = transform.Find("Drawer Button - Closed").GetComponent<CanvasGroup>();

        buttonClosed = closedDrawer.GetComponent<Button>();
        buttonOpen = openDrawer.GetComponent<Button>();
    

        buttonClosed.onClick.AddListener(delegate { OpenDrawer(); });
        buttonOpen.onClick.AddListener(delegate { CloseDrawer(); });
        
    }

    public void SetupHerbWall()
    {
        UIManager.Instance.DisableUI(this.openDrawer);
        UIManager.Instance.EnableUI(this.closedDrawer);
        buttonClosed.enabled = true;
    }

    void OpenDrawer()
    {
        UIManager.Instance.DisableUI(this.closedDrawer);
        UIManager.Instance.EnableUI(this.openDrawer);
    }

    void CloseDrawer()
    {
        UIManager.Instance.DisableUI(this.openDrawer);
        UIManager.Instance.EnableUI(this.closedDrawer);
    }

    public void ToggleActivity()
    {
        if (!isActive)
        {
            UIManager.Instance.DisableUI(openDrawer);
            UIManager.Instance.EnableUI(closedDrawer);
        }
        else
        {
            UIManager.Instance.DisableUI(closedDrawer);
            UIManager.Instance.EnableUI(openDrawer);
        }
        isActive = !isActive;
    }

    
    public void FillDrawer(string herbName)
    {
        // holds the contents of the drawer once set by AllHerbsData
        drawerContents = herbName;
        SendToHerbs(drawerContents);
    }
    
    
    void SendToHerbs(string herbName)
    {
        // sends the name of the herb to the cutting, so it can be read by the inventory
        var herb = transform.Find("Drawer Button - Open/Icon - Herb");

        if (herb.TryGetComponent<DraggableHerbs>(out DraggableHerbs draggableHerb))
        {
            draggableHerb.UpdateHerbType(herbName);
        }
        else
        {
            Debug.Log("Issue when trying to send information to the DraggableHerbs component.");
        }
    }

    public void DisableDrawer()
    {
        buttonClosed.enabled = false;
    }
}
