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

    private Dictionary<int, BotanicalCodexIconUpdater> dietIconUpdaterDictionary;
	private Dictionary<int, BotanicalCodexIconUpdater> sizeIconUpdaterDictionary;

    private List<GameObject> sizeIconList;
    private List<GameObject> dietIconList;
    
    private int botanicalCodexPotencyReferenceNumber;
    private int botanicalCodexDietaryReferenceNumber;
	

    public void InitialiseBotanicalCodex()
    {
	    // Hides the codex object and creates all list and dictionary information.
        botanicalCodexCanvasGroup.gameObject.SetActive(false);

        sizeIconList = new List<GameObject>();
            sizeIconList.Add(sizeIconSmall);
            sizeIconList.Add(sizeIconMedium);
            sizeIconList.Add(sizeIconLarge);
		
		sizeIconUpdaterDictionary = new Dictionary<int, BotanicalCodexIconUpdater>();
            sizeIconUpdaterDictionary[0] = sizeIconSmall.GetComponent<BotanicalCodexIconUpdater>();
            sizeIconUpdaterDictionary[0].InitialiseBotanicalCodexIconUpdater();

            sizeIconUpdaterDictionary[1] = sizeIconMedium.GetComponent<BotanicalCodexIconUpdater>();
            sizeIconUpdaterDictionary[1].InitialiseBotanicalCodexIconUpdater();

            sizeIconUpdaterDictionary[2] = sizeIconLarge.GetComponent<BotanicalCodexIconUpdater>();
            sizeIconUpdaterDictionary[2].InitialiseBotanicalCodexIconUpdater();



        dietIconList = new List<GameObject>();
            dietIconList.Add(dietIconCarnivore);
            dietIconList.Add(dietIconHerbivore);
            dietIconList.Add(dietIconOmnivore);

        dietIconUpdaterDictionary = new Dictionary<int, BotanicalCodexIconUpdater>();
            dietIconUpdaterDictionary[0] = dietIconCarnivore.GetComponent<BotanicalCodexIconUpdater>();
            dietIconUpdaterDictionary[0].InitialiseBotanicalCodexIconUpdater();

            dietIconUpdaterDictionary[1] = dietIconHerbivore.GetComponent<BotanicalCodexIconUpdater>();
            dietIconUpdaterDictionary[1].InitialiseBotanicalCodexIconUpdater();

            dietIconUpdaterDictionary[2] = dietIconOmnivore.GetComponent<BotanicalCodexIconUpdater>();
            dietIconUpdaterDictionary[2].InitialiseBotanicalCodexIconUpdater();

		ResetCodexIconDisplay();
    }

	void ResetCodexIconDisplay()
	{
		for (int i = 0; i < sizeIconList.Count; i++)
        {
            sizeIconUpdaterDictionary[i].ResetBotanicalCodexIcon();
			dietIconUpdaterDictionary[i].ResetBotanicalCodexIcon();
        }
	}

	void UpdateCodexIconDisplay(AllHerbsData.SingleHerb herb)
	{
		for (int i = 0; i < herb._sizes.Count; i++)
        {
            if (herb._sizes[i] == true)
            {
				int index = sizeIconList.IndexOf(sizeIconList[i]);
				sizeIconUpdaterDictionary[index].ToggleBotanicalCodexIcon();
            }

            if (herb._diets[i] == true)
            {
                int index = sizeIconList.IndexOf(sizeIconList[i]);
                dietIconUpdaterDictionary[index].ToggleBotanicalCodexIcon();
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
