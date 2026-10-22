using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotanicalCodexDisplay : MonoBehaviour
{
    public CanvasGroup botanicalCodexCanvasGroup;
    private Vector2 botanicalCodexStoredPosition;
    
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbName;
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbDescription;
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbExtras;

    [SerializeField] private GameObject sizeIconSmall;
    [SerializeField] private GameObject sizeIconMedium;
    [SerializeField] private GameObject sizeIconLarge;
    
    [SerializeField] private GameObject dietIconCarnivore;
    [SerializeField] private GameObject dietIconHerbivore;
    [SerializeField] private GameObject dietIconOmnivore;


    private List<GameObject> sizeIconList;
    private List<GameObject> dietIconList;
    
    private int botanicalCodexPotencyReferenceNumber;
    private int botanicalCodexDietaryReferenceNumber;
    
    /*private static BotanicalCodexDisplay _instance;
    public static BotanicalCodexDisplay Instance
    {
        get
        {
            return _instance;
        }
    }*/

	// public void Update

    public void InitialiseBotanicalCodex()
    {
        botanicalCodexCanvasGroup.gameObject.SetActive(false);
        //UIManager.Instance.DisableUI(botanicalCodexCanvasGroup);
        RectTransform rectTransform = botanicalCodexCanvasGroup.GetComponent<RectTransform>();
        botanicalCodexStoredPosition = rectTransform.anchoredPosition;


        sizeIconList = new List<GameObject>();
            sizeIconList.Add(sizeIconSmall);
            sizeIconList.Add(sizeIconMedium);
            sizeIconList.Add(sizeIconLarge);

        dietIconList = new List<GameObject>();
            dietIconList.Add(dietIconCarnivore);
            dietIconList.Add(dietIconHerbivore);
            dietIconList.Add(dietIconOmnivore);
    }

    // Updates the text displayed on the note popup
    public void ReceiveInformation(string displayName, string displayDescription, string displayExtras)
    {
        Debug.Log("Information received");
        botanicalCodexDisplayedHerbName.text = displayName;
        botanicalCodexDisplayedHerbDescription.text = displayDescription;
        // botanicalCodexDisplayedHerbExtras.text = displayExtras;
        botanicalCodexDisplayedHerbExtras.text = "test";
        

        // PlaceUI();
        botanicalCodexCanvasGroup.gameObject.SetActive(true);
    }

    void HideIconObjects()
    {
        foreach (GameObject s in sizeIconList)
        {
            s.SetActive(false);
        }
        
        foreach (GameObject d in dietIconList)
        {
            d.SetActive(false);
        }
    }
    
    public void PrototypeReceiveInformation(AllHerbsData.SingleHerb herb)//(string displayName, string displayDescription, List<bool> sizes, List<bool> diets)
    {
        Debug.Log("Information received");
        botanicalCodexDisplayedHerbName.text = herb._herbName;// displayName;
        botanicalCodexDisplayedHerbDescription.text = herb._herbDescription;// displayDescription;
        
        botanicalCodexDisplayedHerbExtras.text = "test";

        HideIconObjects();
        
        for (int i = 0; i < herb._sizes.Count; i++)
        {
            if (herb._sizes[i] == true)
            {
                sizeIconList[i].SetActive(true);
            }

            if (herb._diets[i] == true)
            {
                dietIconList[i].SetActive(true);
            }
        }

        botanicalCodexCanvasGroup.gameObject.SetActive(true);
    }

    // determines where best to place the UI before enabling it again
    /*public void PlaceUI()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //botanicalCodexCanvasGroup.anchoredPosition =
        
        botanicalCodexCanvasGroup.gameObject.SetActive(true);
        //UIManager.Instance.EnableUI(botanicalCodexCanvasGroup);
    }*/

    /*
                FOR NEW VERSION::
                -----------------
     
     
    void AssignSingleHerbExtrasToCodexDisplayedHerb()
    {
        foreach (AllHerbsData.SingleHerb herb in AllHerbsData.herbDrawerContents)
        {
            if (herb._herbName == botanicalCodexDisplayedHerbName.text)
            {
                botanicalCodexPotencyReferenceNumber = herb._herbPotency;
                botanicalCodexDietaryReferenceNumber = herb._herbDietaryReference;
                break;
            }
        }
        
        ClearIconLists();
        UpdatePotencyForDisplayedHerb(botanicalCodexPotencyReferenceNumber);
        UpdateDietaryReferenceForDisplayedHerb(botanicalCodexDietaryReferenceNumber);
    }

    void ClearIconLists(List<GameObject> iconsToHide)
    {
        foreach (GameObject icon in iconsToHide)
        {
            icon.SetActive(false);
        }
    }
    
    void UpdatePotencyForDisplayedHerb(int potencyReferenceNumber)
    {
        for (int i = 0; i < potencyReferenceNumber - 1; i++)
        {
            potencyIconList[i].SetActive(true);
        }
    }

    void UpdateDietaryReferenceForDisplayedHerb(int dietaryReferenceNumber)
    {
        // could do dictionary and set up configuration like that? or could do just iconToDisplay dep. on specific ints?
        // 1 = herbivore
        // 2 = carnivore
        // 3 = omnivore
        if (dietaryReferenceNumber == 1)
        {
            // only display plant
        }

        if (dietaryReferenceNumber == 2)
        {
            // only display meat
        }

        if (dietaryReferenceNumber == 3)
        {
            // display both plant and meat
        }
    }*/

    // botanicalCodexStoredPosition is where the note will spawn at on hover
    
    
}
