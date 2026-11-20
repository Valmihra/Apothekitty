using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbCompendiumData : MonoBehaviour
{
    public class HerbCompendiumLeafletData
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
    
    public List<HerbCompendiumLeafletData> compendiumLeafletsList { get; private set; }
    
    // Individual Leaflets
    private int totalCompendiumLeaflets = 0;
    private List<TMP_Text> compendiumLeafletTitlesList;
    private List<HerbCompendiumLeaflet> compendiumLeafletScriptsList;
    
    // Compendium Panel
    private Rect compendiumPanelRect;
    private float compendiumPanelBoundingBoxOffset = 100f;
    
    // Sliding Door
    [SerializeField] private Slider slidingDoor;
    private float closedSlidingDoorValue = 1.0f;	// was prev. int 1.
    // private float openSlidingDoorValue = 0.0f;
    
    
    
    private static HerbCompendiumData _instance;
    public static HerbCompendiumData Instance
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
    
    public void InitialiseHerbCompendium()
    {
        // Calculates the Rect for the Compendium Panel
        compendiumPanelRect = CalculateCompendiumRect();
        
        // Creates the information to be displayed on each leaflet, and then sets them to each prefab in the scene
        CreateLeafletData();
        InitialiseCompendiumLeaflets();
        
    }

    public void ResetCompendium()
    {
        // Closes the sliding door and resets each leaflet
        slidingDoor.value = closedSlidingDoorValue;
        
        foreach (HerbCompendiumLeaflet leaflet in compendiumLeafletScriptsList)
        {
            leaflet.ResetLeaflet();
        }
    }


    private void CreateLeafletData()
    {
        compendiumLeafletsList = new List<HerbCompendiumLeafletData>();
        
        HerbCompendiumLeafletData fortify = new HerbCompendiumLeafletData();
        fortify.SetTreatmentCategoryName("Fortify");
        fortify.SetTreatmentCategoryDescription("Includes <b><#8A1E1E>red</color></b> elements.");
            compendiumLeafletsList.Add(fortify);

        HerbCompendiumLeafletData heal = new HerbCompendiumLeafletData();
        heal.SetTreatmentCategoryName("Heal");
        heal.SetTreatmentCategoryDescription("Includes <b><#2E8A1E>green</color></b> elements.");
            compendiumLeafletsList.Add(heal);

        HerbCompendiumLeafletData ease = new HerbCompendiumLeafletData();
        ease.SetTreatmentCategoryName("Ease");
        ease.SetTreatmentCategoryDescription("Includes <b><#1E338A>blue</color></b> elements.");
            compendiumLeafletsList.Add(ease);
            
            
        HerbCompendiumLeafletData mind = new HerbCompendiumLeafletData();
        mind.SetTreatmentCategoryName("Mind");
        mind.SetTreatmentCategoryDescription("<i>intricate, small</i> or <i>delicate</i> in nature.");
        compendiumLeafletsList.Add(mind);

        HerbCompendiumLeafletData body = new HerbCompendiumLeafletData();
        body.SetTreatmentCategoryName("Body");
        body.SetTreatmentCategoryDescription("<b>hard, strong</b> or <b>large</b> in nature.");
        compendiumLeafletsList.Add(body);
        
        /*
        HerbCompendiumLeafletData spirit = new HerbCompendiumLeafletData();
        spirit.SetTreatmentCategoryName("OLD: USED ONLY ON DAY 2 \n\nSpirit/Enhance");
        // spirit.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n plant has white growths\n\nOR\n\nplant's main body is purple");
        spirit.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nIf you're seeing this and you need to use an enhancer in day 2: \n\n1. may god help you\n2. look for white growths OR purple/blue -- never both\n3. click the answer sheet when you get sick of scouring>");
        // ,
            compendiumLeafletsList.Add(spirit);
            */

        foreach (HerbCompendiumLeafletData g in compendiumLeafletsList)
        {
            totalCompendiumLeaflets++;
        }
    }
    
    private Rect CalculateCompendiumRect()
    {
        // Calculates the perimeter of the gameObject the controller is attached to
        Vector3[] corners = new Vector3[4];
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(corners);

        Vector2 min = corners[0];
        Vector2 max = new Vector2(corners[2].x + compendiumPanelBoundingBoxOffset, corners[2].y);
        Vector2 size = max - min;

        return new Rect(min, size);
    }
    
    private void InitialiseCompendiumLeaflets()
    {
        TMP_Text[] tmpArray = GetComponentsInChildren<TMP_Text>();
        compendiumLeafletTitlesList = new List<TMP_Text>(tmpArray);
        compendiumLeafletScriptsList = new List<HerbCompendiumLeaflet>();
        
        for (int i = 0; i < compendiumLeafletsList.Count; i++)
        {
            // Assigns the title from the list to the TMPro element at the same position in the relevant list
            compendiumLeafletTitlesList[i].raycastTarget = false;
            
            compendiumLeafletScriptsList.Add(compendiumLeafletTitlesList[i].GetComponentInParent<HerbCompendiumLeaflet>());
            compendiumLeafletScriptsList[i].InitialisePrototypeLeaflet();
            compendiumLeafletScriptsList[i].UpdateLeafletText(i);
            compendiumLeafletScriptsList[i].AssignRect(compendiumPanelRect);
        }
    }
}
