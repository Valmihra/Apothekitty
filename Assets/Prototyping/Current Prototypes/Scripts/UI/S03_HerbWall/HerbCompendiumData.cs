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
    
    private int totalPages = 0;
    public List<HerbCompendiumLeafletData> pagesList { get; private set; }

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
    
    public void InitialiseHerbalistGuide()
    {
        SetPageData();
        
    }


    void SetPageData()
    {
        pagesList = new List<HerbCompendiumLeafletData>();
        
        HerbCompendiumLeafletData fortify = new HerbCompendiumLeafletData();
        fortify.SetTreatmentCategoryName("Fortify");
        fortify.SetTreatmentCategoryDescription("Includes <b><#8A1E1E>red</color></b> elements.");
            pagesList.Add(fortify);

        HerbCompendiumLeafletData heal = new HerbCompendiumLeafletData();
        heal.SetTreatmentCategoryName("Heal");
        heal.SetTreatmentCategoryDescription("Includes <b><#2E8A1E>green</color></b> elements.");
            pagesList.Add(heal);

        HerbCompendiumLeafletData ease = new HerbCompendiumLeafletData();
        ease.SetTreatmentCategoryName("Ease");
        ease.SetTreatmentCategoryDescription("Includes <b><#1E338A>blue</color></b> elements.");
            pagesList.Add(ease);
            
            
        HerbCompendiumLeafletData mind = new HerbCompendiumLeafletData();
        mind.SetTreatmentCategoryName("Mind");
        mind.SetTreatmentCategoryDescription("<i>intricate, small</i> or <i>delicate</i> in nature.");
        pagesList.Add(mind);

        HerbCompendiumLeafletData body = new HerbCompendiumLeafletData();
        body.SetTreatmentCategoryName("Body");
        body.SetTreatmentCategoryDescription("<b>hard, strong</b> or <b>large</b> in nature.");
        pagesList.Add(body);
        
        
        
        /*
        HerbCompendiumLeafletData spirit = new HerbCompendiumLeafletData();
        spirit.SetTreatmentCategoryName("OLD: USED ONLY ON DAY 2 \n\nSpirit/Enhance");
        // spirit.SetTreatmentCategoryDescription("How to identify plants that can be used for this property:\n\n plant has white growths\n\nOR\n\nplant's main body is purple");
        spirit.SetTreatmentCategoryDescription("TEMPORARY TEXT \n\nIf you're seeing this and you need to use an enhancer in day 2: \n\n1. may god help you\n2. look for white growths OR purple/blue -- never both\n3. click the answer sheet when you get sick of scouring>");
        // ,
            pagesList.Add(spirit);
            */

        foreach (HerbCompendiumLeafletData g in pagesList)
        {
            totalPages++;
        }
    }
}
