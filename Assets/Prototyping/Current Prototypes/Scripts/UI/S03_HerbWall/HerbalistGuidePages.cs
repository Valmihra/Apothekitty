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
    
    // private CanvasGroup herbalistGuideBookCanvasGroup;
    [SerializeField] private Button herbalistGuideShowBookToggle;
    [SerializeField] private GameObject herbalistGuideBookObject;
    
    private bool herbalistGuideBookIsOpen;
    
    private Vector2 herbalistGuideBookInitialPosition;

    public List<HerbalistGuideSinglePage> pagesList { get; private set; }
    // public HerbalistGuideSinglePage[] pagesArray;

    int totalPages = 0;

    private HerbalistGuideNavigation herbalistGuideNavigationReference;
    private bool testingMockup;

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
        testingMockup = true;
    }
    
    public void InitialiseHerbalistGuide()
    {
        // herbalistGuideBookObject = GameObject.Find("Panel - Expanded Herbalist's Guide");
        // herbalistGuideBookObject = GetComponentInChildren<MouseHover>().gameObject;
        SetPageData();
        
        if (testingMockup)
        {
            Debug.Log("Testing new herb wall mockup. Deleting old objects...");
            // herbalistGuideBookObject = null;
            herbalistGuideNavigationReference = null;
            // herbalistGuideBookInitialPosition = null;
            Destroy(herbalistGuideBookObject);
            Destroy(herbalistGuideShowBookToggle.gameObject);
        }
        else
        {
            herbalistGuideNavigationReference = GetComponent<HerbalistGuideNavigation>();
            herbalistGuideNavigationReference.InitialiseHerbalistGuideNavigation();
                
            herbalistGuideBookInitialPosition = herbalistGuideBookObject.transform.position;
            
            if (GameObject.Find("Button - Herbalist's Guide").TryGetComponent<Button>(out Button herbalistGuideShowBookToggle))
            {
                herbalistGuideShowBookToggle.onClick.AddListener(delegate { ToggleGuide(); });
                // Debug.Log("Checking for the button like this works");
            }
        }
    }

    public void ResetHerbalistGuide()
    {
        if (!testingMockup)
        {
            herbalistGuideBookObject.transform.position = herbalistGuideBookInitialPosition;
            herbalistGuideBookObject.SetActive(false);      // UIManager.Instance.DisableUI(herbalistGuideBookCanvasGroup);
            herbalistGuideBookIsOpen = false;

            // Resets the navigation script
            herbalistGuideNavigationReference.ResetHerbalistGuideNavigation();
        }
    }

    void ToggleGuide()
    {
        if (!testingMockup)
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
        
    }

    void SetPageData()
    {
        pagesList = new List<HerbalistGuideSinglePage>();
        
        HerbalistGuideSinglePage fortify = new HerbalistGuideSinglePage();
        fortify.SetTreatmentCategoryName("Fortify");
        fortify.SetTreatmentCategoryDescription("Includes <b><#8A1E1E>red</color></b> elements.");
            pagesList.Add(fortify);

        HerbalistGuideSinglePage heal = new HerbalistGuideSinglePage();
        heal.SetTreatmentCategoryName("Heal");
        heal.SetTreatmentCategoryDescription("Includes <b><#2E8A1E>green</color></b> elements.");
            pagesList.Add(heal);

        HerbalistGuideSinglePage ease = new HerbalistGuideSinglePage();
        ease.SetTreatmentCategoryName("Ease");
        ease.SetTreatmentCategoryDescription("Includes <b><#1E338A>blue</color></b> elements.");
            pagesList.Add(ease);
            
            
        HerbalistGuideSinglePage mind = new HerbalistGuideSinglePage();
        mind.SetTreatmentCategoryName("Mind");
        mind.SetTreatmentCategoryDescription("<i>intricate, small</i> or <i>delicate</i> in nature.");
        pagesList.Add(mind);

        HerbalistGuideSinglePage body = new HerbalistGuideSinglePage();
        body.SetTreatmentCategoryName("Body");
        body.SetTreatmentCategoryDescription("<b>hard, strong</b> or <b>large</b> in nature.");
        pagesList.Add(body);
        
        
        
        /*
        HerbalistGuideSinglePage spirit = new HerbalistGuideSinglePage();
        spirit.SetTreatmentCategoryName("OLD: USED ONLY ON DAY 2 \n\nSpirit/Enhance");
        // spirit.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n plant has white growths\n\nOR\n\nplant's main body is purple");
        spirit.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nIf you're seeing this and you need to use an enhancer in day 2: \n\n1. may god help you\n2. look for white growths OR purple/blue -- never both\n3. click the answer sheet when you get sick of scouring>");
        // ,
            pagesList.Add(spirit);
            */

        foreach (HerbalistGuideSinglePage g in pagesList)
        {
            totalPages++;
        }

        // Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
}
