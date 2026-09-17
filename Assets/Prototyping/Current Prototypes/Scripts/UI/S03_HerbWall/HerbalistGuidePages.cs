using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbalistGuidePages : MonoBehaviour
{
    public class HerbalistGuideSinglePage
    {
        public string _treatmentCategoryName;
        public string _treatmentCategoryDescription;

        public void SetTreatmentCategoryName(string newName)
        {
            _treatmentCategoryName = newName;
        }

        public void SetTreatmentCategoryDescription(string newDescription)
        {
            _treatmentCategoryDescription = newDescription;
        }
    }
    
    // public Button guideToggle;
    // public CanvasGroup herbalistGuideCanvasGroup;
    private Button herbalistGuideShowBookToggle;
    // private CanvasGroup herbalistGuideBookCanvasGroup;
    private bool herbalistGuideBookIsOpen;

    private GameObject herbalistGuideBookObject;
    private Vector2 herbalistGuideBookInitialPosition;

    public List<HerbalistGuideSinglePage> pagesList { get; private set; }
    // public HerbalistGuideSinglePage[] pagesArray;

    int totalPages = 0;

    private HerbalistGuideNavigation herbalistGuideNavigationReference;

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
    
    public void InitialiseHerbalistGuide()
    {
        herbalistGuideNavigationReference = GetComponent<HerbalistGuideNavigation>();
        
        herbalistGuideBookObject = GameObject.Find("Panel - Expanded Herbalist's Guide");
        herbalistGuideBookInitialPosition = herbalistGuideBookObject.transform.position;
        
        if (GameObject.Find("Button - Herbalist's Guide").TryGetComponent<Button>(out Button herbalistGuideShowBookToggle))
        {
            herbalistGuideShowBookToggle.onClick.AddListener(delegate { ToggleGuide(); });
            // Debug.Log("Checking for the button like this works");
        }
        
        SetPageData();
    }

    public void ResetHerbalistGuide()
    {
        herbalistGuideBookObject.transform.position = herbalistGuideBookInitialPosition;
        herbalistGuideBookObject.SetActive(false);      // UIManager.Instance.DisableUI(herbalistGuideBookCanvasGroup);
        herbalistGuideBookIsOpen = false;

        // Resets the navigation script
        herbalistGuideNavigationReference.ResetHerbalistGuideNavigation();
    }

    void ToggleGuide()
    {
        if (herbalistGuideBookIsOpen)
        {
            herbalistGuideBookObject.SetActive(false);  // UIManager.Instance.DisableUI(herbalistGuideBookCanvasGroup);
        }
        else
        {
            herbalistGuideBookObject.SetActive(true);   // UIManager.Instance.EnableUI(herbalistGuideBookCanvasGroup);
        }
        herbalistGuideBookIsOpen = !herbalistGuideBookIsOpen;
    }

    void SetPageData()
    {
        pagesList = new List<HerbalistGuideSinglePage>();
        
        // The astute botanist will recognise that herbs capable of affecting the mind will always physically reflect the complexity of the brain. physically reflect their target
        //Plants capable of affecting the mind will always reflect the complexity of the brain. 
        
        
        HerbalistGuideSinglePage intro = new HerbalistGuideSinglePage();
        intro.SetTreatmentCategoryName("A Reminder");
        intro.SetTreatmentCategoryDescription("Remember that the plant must meet the requirements of the guide. if it meets both requirements when it only asks for one of the two, it is not a valid choice");
            pagesList.Add(intro);
            

        HerbalistGuideSinglePage fortify = new HerbalistGuideSinglePage();
        fortify.SetTreatmentCategoryName("Fortify");
        // fortify.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\nplant is short and its body is wide\n\nOR\n\nhalf or more of the plant is yellow");
        fortify.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nPlants used to fortify will always have <b><#8A1E1E>red</color></b> on them.");
        // ,
            pagesList.Add(fortify);

        HerbalistGuideSinglePage heal = new HerbalistGuideSinglePage();
        heal.SetTreatmentCategoryName("Heal");
        // heal.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\nplant's main growth has flecks of colour on it \n\nOR\n\nred is a prominent colour");
        heal.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nPlants used to heal will always have <b><#2E8A1E>green</color></b> on them.");
        // ,
            pagesList.Add(heal);

        HerbalistGuideSinglePage ease = new HerbalistGuideSinglePage();
        ease.SetTreatmentCategoryName("Ease");
        // ease.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\nplant's stems or body is long and spindly\n\nOR\n\nit produces something that is not a flower");
        ease.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nPlants used to ease will always have <b><#1E338A>blue</color></b> on them.");
        // ,
            pagesList.Add(ease);
            
            
        HerbalistGuideSinglePage mind = new HerbalistGuideSinglePage();
        mind.SetTreatmentCategoryName("Mind");
        // mind.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n flowers that bloom so much that their blossoms overlap\n\nOR\n\nplants that bear fruit");
        mind.SetTreatmentCategoryDescription(
            "TEMPORARY TEXT \n\nPlants capable of affecting the mind will always physically reflect the complexity of the brain. \n\n<INTRICATE: CREEPING TENDRILS, DELICATE BLOSSOMS>");
        // intricate, delicate, complex
        pagesList.Add(mind);

        HerbalistGuideSinglePage body = new HerbalistGuideSinglePage();
        body.SetTreatmentCategoryName("Body");
        // body.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n the plant has no flowers or fruit\n\nAND\n\nhas two colours maximum");
        body.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nPlants capable of affecting the body will always physically reflect the solidity of the flesh. \n\n<HARDY: SHORT AND WIDE/THICK, RIGID, STOCKY, ETC.>");
        // ,
        pagesList.Add(body);

        HerbalistGuideSinglePage spirit = new HerbalistGuideSinglePage();
        spirit.SetTreatmentCategoryName("OLD: USED ONLY ON DAY 2 \n\nSpirit/Enhance");
        // spirit.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n plant has white growths\n\nOR\n\nplant's main body is purple");
        spirit.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nIf you're seeing this and you need to use an enhancer in day 2: \n\n1. may god help you\n2. look for white growths OR purple/blue -- never both\n3. click the answer sheet when you get sick of scouring>");
        // ,
            pagesList.Add(spirit);

        foreach (HerbalistGuideSinglePage g in pagesList)
        {
            totalPages++;
        }

        // Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
}
