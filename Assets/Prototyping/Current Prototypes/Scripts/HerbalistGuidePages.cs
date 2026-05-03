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

    private List<GuidePage> pagesList;
    public GuidePage[] pagesArray;

    int totalPages = 0;

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
    }
    
    void Start()
    {
        guideToggle.onClick.AddListener(delegate { ToggleGuide(); });


        InitialiseList();
        SetPageData();
        SetArray();


        SetupGuide();
    }

    void SetupGuide()
    {
        UIManager.Instance.DisableUI(herbalistGuide);
        active = false;
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
        //Debug.Log("pagesArray is " + pagesArray.Length + " units long!");
    }

    void SetPageData()
    {
        GuidePage mind = new GuidePage();
        mind.UpdateType("Mind");
        mind.UpdateDescription("Plants that creep and twist. \n\n Coiling tendrils, Clustered blossoms, or Small, hanging fruits");
            pagesList.Add(mind);
        totalPages++;

        GuidePage body = new GuidePage();
        body.UpdateType("Body");
        body.UpdateDescription("Plants that are of the earth. \n\n Visible roots, Grows close to the ground, or is Attached to bark");
            pagesList.Add(body);
        totalPages++;

        GuidePage spirit = new GuidePage();
        spirit.UpdateType("Spirit");
        spirit.UpdateDescription("Plants that make use of life cycles and the transfer of energy in some way. \n\n Grows from another plant, is carnivorous, or is a type of fungi");
            pagesList.Add(spirit);
        totalPages++;

        GuidePage fortify = new GuidePage();
        fortify.UpdateType("Fortify");
        fortify.UpdateDescription("These plants always have a rounded shape; cuttings either have three leaves, or are short and wide.");
            pagesList.Add(fortify);
        totalPages++;

        GuidePage heal = new GuidePage();
        heal.UpdateType("Heal");
        heal.UpdateDescription("These plants always have unique colouration; variegated leaves/striations/flecks of colour, or the leaves/body is fenestrated - meaning it has visible holes in it somewhere.");
            pagesList.Add(heal);
        totalPages++;

        GuidePage ease = new GuidePage();
        ease.UpdateType("Ease");
        ease.UpdateDescription("These plants are always delicate; cuttings either have long, spindly stems, or the plant itself is fragile and easily harmed.");
            pagesList.Add(ease);
        totalPages++;

        Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
}
