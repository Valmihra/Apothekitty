using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbalistGuidePages : MonoBehaviour
{
    public Button guideToggle;
    public CanvasGroup herbalistGuide;
    private bool active;

    public class GuidePage
    {
        public string propertyType_;
        public string propertyDescription_;

        public void UpdateType(string newName)
        {
            propertyType_ = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            propertyDescription_ = newDescription;
        }
    }

    private GameObject herbalistGuideObject;
    private Vector2 spawnPosition;

    private List<GuidePage> pagesList;
    public GuidePage[] pagesArray;

    int totalPages = 0;

    private HerbalistGuideNavigation herbalistGuideNavigation;

    private static HerbalistGuidePages _instance;
    public static HerbalistGuidePages Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        herbalistGuideNavigation = GetComponent<HerbalistGuideNavigation>();
        herbalistGuideObject = GameObject.Find("Panel - Expanded Herbalist's Guide");
        GetSpawnPoint();//spawnPosition = transform.position;
    }
    
    void Start()
    {
        guideToggle.onClick.AddListener(delegate { ToggleGuide(); });


        InitialiseList();
        SetPageData();
        SetArray();

        // ResetHerbalistGuide();
    }

    void GetSpawnPoint()
    {
        spawnPosition = herbalistGuideObject.transform.position;
    }

    void ResetNavigationScript()
    {
        herbalistGuideNavigation.ResetHerbalistGuideNavigation();
    }

    public void ResetHerbalistGuide()
    {
        herbalistGuideObject.transform.position = spawnPosition;
        UIManager.Instance.DisableUI(herbalistGuide);
        active = false;

        ResetNavigationScript();
    }

    void ToggleGuide()
    {
        if (active)
        {
            UIManager.Instance.DisableUI(herbalistGuide);
        }
        else
        {
            UIManager.Instance.EnableUI(herbalistGuide);
        }
        active = !active;
    }

    


    void InitialiseList()
    {
        pagesList = new List<GuidePage>();
    }

    void SetArray()
    {
        pagesArray = pagesList.ToArray();
        // Debug.Log("pagesArray is " + pagesArray.Length + " units long!");
    }

    void SetPageData()
    {
        GuidePage intro = new GuidePage();
        intro.UpdateType("A Reminder");
        intro.UpdateDescription("Remember that the plant must meet the requirements of the guide. if it meets both requirements when it only asks for one of the two, it is not a valid choice");
            pagesList.Add(intro);

        GuidePage mind = new GuidePage();
        mind.UpdateType("Mind");
        mind.UpdateDescription("How to identify plants that can be used for this property:\n\n flowers that bloom so much that their blossoms overlap\n\nOR\n\nplants that bear fruit");
        // intricate, delicate, complex
            pagesList.Add(mind);

        GuidePage body = new GuidePage();
        body.UpdateType("Body");
        body.UpdateDescription("How to identify plants that can be used for this property:\n\n the plant has no flowers or fruit\n\nAND\n\nhas two colours maximum");
        // ,
            pagesList.Add(body);

        GuidePage spirit = new GuidePage();
        spirit.UpdateType("Spirit - (also Enhance currently)");
        spirit.UpdateDescription("How to identify plants that can be used for this property:\n\n plant has white growths\n\nOR\n\nplant's main body is purple");
        // ,
            pagesList.Add(spirit);

        GuidePage fortify = new GuidePage();
        fortify.UpdateType("Fortify");
        fortify.UpdateDescription("How to identify plants that can be used for this property:\n\nplant is short and its body is wide\n\nOR\n\nhalf or more of the plant is yellow");
        // ,
            pagesList.Add(fortify);

        GuidePage heal = new GuidePage();
        heal.UpdateType("Heal");
        heal.UpdateDescription("How to identify plants that can be used for this property:\n\nplant's main growth has flecks of colour on it \n\nOR\n\nred is a prominent colour");
        // ,
            pagesList.Add(heal);

        GuidePage ease = new GuidePage();
        ease.UpdateType("Ease");
        ease.UpdateDescription("How to identify plants that can be used for this property:\n\nplant's stems or body is long and spindly\n\nOR\n\nit produces something that is not a flower");
        // ,
            pagesList.Add(ease);

        foreach (GuidePage g in pagesList)
        {
            totalPages++;
        }

        // Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
}
