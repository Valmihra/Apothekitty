using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HerbCompendiumController : MonoBehaviour
{
    private List<TMP_Text> compendiumLeafletTitlesList;
    private List<HerbCompendiumLeaflet> compendiumLeafletsList;
    
    private Rect compendiumPanelRect;
    private float compendiumPanelBoundingBoxOffset = 100f;
    
    private float closedSlidingDoorValue = 1.0f;	// was prev. int 1.
	private float openSlidingDoorValue = 0.0f;
    [SerializeField] private Slider slidingDoor;
    // [SerializeField] private GameObject interactionBlocker;
    // *TAG* - For use on slider: Maybe if roller is open, interaction blocker prevents touching the drawers? idk.
    private static HerbCompendiumController _instance;
    public static HerbCompendiumController Instance
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
    
    public void InitialiseCompendium()
    {
        compendiumPanelRect = CalculateCompendiumRect();
        
        // Closes sliding door
        slidingDoor.value = closedSlidingDoorValue;
        
        InitialiseCompendiumLeaflets();
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
        // *TAG* - COULD SIMPLIFY -- DON'T NECESSARILY NEED THE TITLES, SINCE CAN SET IN LEAFLET INDIVIDUALLY..!!!
        
        
        // List<string> titles = new List<string> { "Fortify", "Heal", "Ease", "Mind", "Body" };
            TMP_Text[] tmpArray = GetComponentsInChildren<TMP_Text>();
            compendiumLeafletTitlesList = new List<TMP_Text>(tmpArray);
            
        compendiumLeafletsList = new List<HerbCompendiumLeaflet>();
        
        for (int i = 0; i < HerbCompendiumData.Instance.pagesList.Count; i++)
        {
            // Assigns the title from the list to the TMPro element at the same position in the relevant list
            // compendiumLeafletTitlesList[i].text = titles[i];
            compendiumLeafletTitlesList[i].raycastTarget = false;
            
            compendiumLeafletsList.Add(compendiumLeafletTitlesList[i].GetComponentInParent<HerbCompendiumLeaflet>());
                compendiumLeafletsList[i].InitialisePrototypeLeaflet();
			    compendiumLeafletsList[i].UpdateLeafletText(i);
                compendiumLeafletsList[i].AssignRect(compendiumPanelRect);
        }
    }

    public void ResetCompendiumLeaflets()
    {
		foreach (HerbCompendiumLeaflet leaflet in compendiumLeafletsList)
        {
            leaflet.ResetLeaflet();
        }
    }


	public void TutorialOpenCompendium()
	{
		StopAllCoroutines();
		StartCoroutine(OpenCompendium());
	}

	IEnumerator OpenCompendium()
	{
		// float targetValue = 0.0f;
		float timeToLerp = 0.5f;
		float elapsedTime = 0.0f;
		
		float currentValue = slidingDoor.value == closedSlidingDoorValue ? closedSlidingDoorValue : openSlidingDoorValue;
		float targetValue = currentValue == closedSlidingDoorValue ? openSlidingDoorValue : closedSlidingDoorValue;

		while (elapsedTime < timeToLerp)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime/timeToLerp);

			slidingDoor.value = Mathf.Lerp(currentValue, targetValue, elapsedTime / timeToLerp);

			yield return null;
		}
	}

}
