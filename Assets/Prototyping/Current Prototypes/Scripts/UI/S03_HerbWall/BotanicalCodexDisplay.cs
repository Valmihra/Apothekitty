using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotanicalCodexDisplay : MonoBehaviour
{
    public CanvasGroup botanicalCodexCanvasGroup;
    
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbName;
    [SerializeField] private TMP_Text botanicalCodexDisplayedHerbDescription;

    [SerializeField] private GameObject sizeIconSmall;
    [SerializeField] private GameObject sizeIconMedium;
    [SerializeField] private GameObject sizeIconLarge;
    
    [SerializeField] private GameObject dietIconCarnivore;  // 0
    [SerializeField] private GameObject dietIconHerbivore;  // 1
    [SerializeField] private GameObject dietIconOmnivore;   // 2

	[SerializeField] private Sprite sizeIconDeactivated;
	[SerializeField] private Sprite sizeIconActivated;

    private Dictionary<int, BotanicalCodexIconUpdater> iconUpdaterDictionary;

    // [SerializeField] private Sprite dietIconActivated;
    // [SerializeField] private Sprite dietIconDeactivated;



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


        sizeIconList = new List<GameObject>();
            sizeIconList.Add(sizeIconSmall);
            sizeIconList.Add(sizeIconMedium);
            sizeIconList.Add(sizeIconLarge);

        dietIconList = new List<GameObject>();
            dietIconList.Add(dietIconCarnivore);
            dietIconList.Add(dietIconHerbivore);
            dietIconList.Add(dietIconOmnivore);

        iconUpdaterDictionary = new Dictionary<int, BotanicalCodexIconUpdater>();
            iconUpdaterDictionary[0] = dietIconCarnivore.GetComponent<BotanicalCodexIconUpdater>();
            iconUpdaterDictionary[0].InitialiseBotanicalCodexIconUpdater();

            iconUpdaterDictionary[1] = dietIconHerbivore.GetComponent<BotanicalCodexIconUpdater>();
            iconUpdaterDictionary[1].InitialiseBotanicalCodexIconUpdater();

            iconUpdaterDictionary[2] = dietIconOmnivore.GetComponent<BotanicalCodexIconUpdater>();
            iconUpdaterDictionary[2].InitialiseBotanicalCodexIconUpdater();

		ResetCodexIconDisplay();
    }

	void ResetCodexIconDisplay()
	{
		foreach (GameObject s in sizeIconList)
		{
            if (s.TryGetComponent<Image>(out Image tempImage))
			{
                UIManager.Instance.SpriteShift(tempImage, sizeIconDeactivated);
            }
		}

        for (int i = 0; i < dietIconList.Count; i++)    // foreach (GameObject d in dietIconList)
        {
            // int index = 
            // if (d.TryGetComponent<BotanicalCodexIconUpdater>())
            // {
            // UIManager.Instance.SpriteShift(tempImage, dietIconDeactivated);
            iconUpdaterDictionary[i].ResetBotanicalCodexIcon();
            // }
        }
	}

	void UpdateCodexIconDisplay(AllHerbsData.SingleHerb herb)
	{
        // *TAG* - Implement diet code for sizes, too!

		for (int i = 0; i < herb._sizes.Count; i++)
        {
            if (herb._sizes[i] == true)
            {
                // Finds the correct size icon and toggles it
				UIManager.Instance.SpriteShift(sizeIconList[i].GetComponent<Image>(), sizeIconActivated);
            }

            if (herb._diets[i] == true)
            {
                // Finds the correct diet icon and toggles it in its updater
                int index = sizeIconList.IndexOf(sizeIconList[i]);
                iconUpdaterDictionary[index].ToggleBotanicalCodexIcon();
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

		ResetCodexIconDisplay();
		UpdateCodexIconDisplay(herb);

        botanicalCodexCanvasGroup.gameObject.SetActive(true);
    }

}
