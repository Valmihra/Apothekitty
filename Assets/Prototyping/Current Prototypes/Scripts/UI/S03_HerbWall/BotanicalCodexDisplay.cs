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
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbExtras;	// *TAG* - DELETE ME!

    [SerializeField] private GameObject sizeIconSmall;
    [SerializeField] private GameObject sizeIconMedium;
    [SerializeField] private GameObject sizeIconLarge;
    
    [SerializeField] private GameObject dietIconCarnivore;
    [SerializeField] private GameObject dietIconHerbivore;
    [SerializeField] private GameObject dietIconOmnivore;

	[SerializeField] private Image sizeIconDeactivated;
	[SerializeField] private Image sizeIconActivated;

    private List<GameObject> sizeIconList;
    private List<GameObject> dietIconList;
    
    private int botanicalCodexPotencyReferenceNumber;
    private int botanicalCodexDietaryReferenceNumber;
	
	[Header("Shader References")]
	public Shader standardIconShader;
	public Shader desaturatedIconShader;

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

		ResetCodexIconDisplay();
    }

	void ResetCodexIconDisplay()
	{
		foreach (GameObject s in sizeIconList)
		{
			Image tempImage = s.GetComponent<Image>();
			// UIManager.Instance.SpriteShift(sizeIconDeactivated, tempImage.sprite);// s.GetComponent<Image>().sprite);
			UIManager.Instance.SpriteShift(tempImage, sizeIconDeactivated.sprite);
		}

		foreach (GameObject d in dietIconList)
		{
			// For now, just deactivating, but in future, set all to black and white via shader?
			// d.SetActive(false);
			UpdateShader(d); //, desaturatedIconShader);
		}
	}

	void UpdateCodexIconDisplay(AllHerbsData.SingleHerb herb)
	{
		for (int i = 0; i < herb._sizes.Count; i++)
        {
            if (herb._sizes[i] == true)
            {
                // sizeIconList[i].SetActive(true);
				UIManager.Instance.SpriteShift(sizeIconList[i].GetComponent<Image>(), sizeIconActivated.sprite);
            }

            if (herb._diets[i] == true)
            {
				// For now, just reactivating, but in future, set to colour via shader?
                // dietIconList[i].SetActive(true);
				UpdateShader(dietIconList[i]);// , standardIconShader);
            }
        }
	}

    void HideIconObjects()
    {
		// *TAG* - TRY INSTEAD: Setup secondary icon for sizes. Display all three at all times, but reset = set to black. when activated, set to active sprite icon. to 
        foreach (GameObject s in sizeIconList)
        {
            s.SetActive(false);
        }
        
        foreach (GameObject d in dietIconList)
        {
            d.SetActive(false);
        }
    }

	void ShowCorrectIconObjects(AllHerbsData.SingleHerb herb)
	{
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
    }

    public void CodexReceiveInformation(AllHerbsData.SingleHerb herb)
    {
        // Uses SingleHerb data to set the herb's name and description
        botanicalCodexDisplayedHerbName.text = herb._herbName;
        botanicalCodexDisplayedHerbDescription.text = herb._herbDescription;
        
        botanicalCodexDisplayedHerbExtras.text = "test";	// *TAG* - DELETE ME!

        // HideIconObjects();
		// ShowCorrectIconObjects(herb);
		ResetCodexIconDisplay();
		UpdateCodexIconDisplay(herb);

        botanicalCodexCanvasGroup.gameObject.SetActive(true);
    }

	void UpdateShader(GameObject objectToUpdate)//, Shader shader)
	{
		// Updates the shader of the passed GameObject
		if (objectToUpdate.TryGetComponent<Renderer>(out Renderer renderer))
		{
			Debug.Log("Would change shader here.");
			// Material materialToEdit = renderer.material;
			// renderer.material.shader = shader;
		}
		else
		{
			Debug.Log("Error when trying to get the renderer for this object: " + objectToUpdate);
		}
	}

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
