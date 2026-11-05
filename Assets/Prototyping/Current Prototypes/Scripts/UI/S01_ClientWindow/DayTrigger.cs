using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DayTrigger : MonoBehaviour, IPointerClickHandler
{
    
    public Image openCurtainImage;
    public Image closedCurtainImage;
    public Image displayedCurtainImage;
    
    private Sprite openCurtainSpriteVariant;
    private Sprite closedCurtainSpriteVariant;

    private CanvasGroup curtainCanvasGroup;


    public void InitialiseDayTrigger()
    {
        curtainCanvasGroup = gameObject.GetComponent<CanvasGroup>();
        openCurtainSpriteVariant = openCurtainImage.sprite;
        closedCurtainSpriteVariant = closedCurtainImage.sprite;
    }
    
    public void ResetCurtain()
    {
		// Enables Interaction on the curtain and sets the sprite to closed variant
		UIManager.Instance.EnableInteraction(curtainCanvasGroup);
        if (displayedCurtainImage.sprite != closedCurtainSpriteVariant)
        {
            UIManager.Instance.SpriteShift(displayedCurtainImage, closedCurtainSpriteVariant);
        }
        return;
    }
    
    // Toggles curtain sprite and disables interaction depending on time of day.
    void ToggleCurtainState()
    {
        // If it's the beginning of the day
        if (GameManager.Instance.shopIsClosed == true)
        {
            if (displayedCurtainImage.sprite == closedCurtainSpriteVariant)
            {
				UIManager.Instance.SpriteShift(displayedCurtainImage, openCurtainSpriteVariant);
                UIManager.Instance.DisableInteraction(curtainCanvasGroup);
                GameManager.Instance.shopIsClosed = false;
                GameManager.Instance.SummonClient();
            }
            else
            {
                Debug.Log("Issue with logic on setup.");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.runningTutorial && GameManager.Instance.canOpenShop)
        {
            ToggleCurtainState();
            GameManager.Instance.canOpenShop = false;
        }
        else if (!GameManager.Instance.runningTutorial)
        {
            ToggleCurtainState();
            GameManager.Instance.canOpenShop = false;
        }
    }
}
