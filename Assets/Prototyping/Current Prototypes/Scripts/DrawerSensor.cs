using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerSensor : MonoBehaviour
{
    public bool isActive;
    public bool drawerClosedActive;
    public bool drawerOpenActive;

    public CanvasGroup openDrawer;
    public CanvasGroup closedDrawer;

    public Button buttonOpen;
    public Button buttonClosed;

    //private GameObject drawer;

    void Awake()
    {
        openDrawer = transform.Find("Drawer Button - Open").GetComponent<CanvasGroup>();
        closedDrawer = transform.Find("Drawer Button - Closed").GetComponent<CanvasGroup>();

        buttonClosed = closedDrawer.GetComponent<Button>();
        buttonOpen = openDrawer.GetComponent<Button>();
        
        /*buttonClosed.onClick.AddListener(delegate { ToggleActivity(); });
        buttonOpen.onClick.AddListener(delegate { ToggleActivity(); });/*/

        buttonClosed.onClick.AddListener(delegate { OpenDrawer(); });
        buttonOpen.onClick.AddListener(delegate { CloseDrawer(); });
        //buttonOpen.onClick.Invoke();
        
        Invoke (nameof(SetupHerbWall), 0.5f);
        
    }
    // Start is called before the first frame update
    void Start()
    {
                //Invoke (nameof(SetupHerbWall), 0.5f);
                //isActive = false;
                //drawer =  this.gameObject;
                //Debug.Log("Starting.");
        /*openDrawer = transform.Find("Drawer Button - Open").GetComponent<CanvasGroup>();
        closedDrawer = transform.Find("Drawer Button - Closed").GetComponent<CanvasGroup>();

        buttonClosed = closedDrawer.GetComponent<Button>();
        buttonOpen = openDrawer.GetComponent<Button>();
        
        /*buttonClosed.onClick.AddListener(delegate { ToggleActivity(); });
        buttonOpen.onClick.AddListener(delegate { ToggleActivity(); });/

        buttonClosed.onClick.AddListener(delegate { OpenDrawer(); });
        buttonOpen.onClick.AddListener(delegate { CloseDrawer(); });*/

        //this.CloseDrawer();
        //buttonClosed()
            /*openDrawer.interactable = false;
            openDrawer.blocksRaycasts = false;
            openDrawer.alpha = 0;
            isActive = true;
            ToggleActivity();*/
            
            //drawerClosedActive = true;
            //drawerOpenActive = false;

                    //ToggleActivity();

                //Invoke (nameof(SetupHerbWall), 2.0f);
                //SetupHerbWall();  
                    //buttonClosed.onClick.Invoke();

                /*GameObject drawer = this.transform.Find(gameObject.name).gameObject;
                GameObject drawerOpen = drawer.transform.Find("Drawer Button - Open").gameObject;
                GameObject drawerClosed = drawer.transform.Find("Drawer Button - Closed").gameObject;*/

                //Panel drawer = this.transform.Find(gameObject.name).GetComponent<Panel>();
                //GameObject drawerOpen = drawer.transform.Find("Drawer Button - Open").gameObject;
                //GameObject drawerClosed = drawer.transform.Find("Drawer Button - Closed").gameObject;

                //openDrawer = drawerOpen.GetComponent<CanvasGroup>();
                //closedDrawer = drawerClosed.GetComponent<CanvasGroup>();
                
                //ToggleActivity();
                //isActive = false;
                //SetupHerbWall();
        
    }

    void SetupHerbWall()
    {
        UIManager.Instance.DisableUI(this.openDrawer);
        UIManager.Instance.EnableUI(this.closedDrawer);
        //isActive = false;

        //drawerClosedActive = true;
        //drawerOpenActive = false;

        //Debug.Log("Set up.");
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
        //Debug.Log("Toggling...");
        /*if (drawerClosedActive && !drawerOpenActive)//(!isActive)
        {
            UIManager.Instance.DisableUI(openDrawer);
            UIManager.Instance.EnableUI(closedDrawer);
        }
        else if (drawerOpenActive && !drawerClosedActive)
        {
            UIManager.Instance.DisableUI(closedDrawer);
            UIManager.Instance.EnableUI(openDrawer);
        }
        else
        {
            //SetupHerbWall();
            Debug.Log("Problem here.");
        }
        
        drawerClosedActive = !drawerClosedActive;
        drawerOpenActive = !drawerOpenActive;*/

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
}
