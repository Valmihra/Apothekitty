using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DayTrigger : MonoBehaviour, IPointerClickHandler
{
    public Image curtainDisplay;
    public Image open;
    public Image closed;

    private Sprite openVariant;
    private Sprite closedVariant;

    private CanvasGroup curtainGroup;


    void Start()
    {
        curtainGroup = gameObject.GetComponent<CanvasGroup>();
        openVariant = open.sprite;
        closedVariant = closed.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.runningTutorial && GameManager.Instance.canStartDay)
        {
            ToggleCurtainState();
            GameManager.Instance.canStartDay = false;
        }
        else if (!GameManager.Instance.runningTutorial)
        {
            ToggleCurtainState();
            GameManager.Instance.canStartDay = false;
        }
    }

    // GameManager uses this to close the curtain and ensure the scene is set up correctly.
    public void ResetCurtain()
    {
        EnableDayTrigger();
        if (curtainDisplay.sprite != closedVariant)
        {
            SpriteShift(curtainDisplay, closedVariant);
        }
        return;
    }

    // Enables interaction on the curtain. Called separately so that tutorial dialogue can run first.
    void EnableDayTrigger()
    {
        UIManager.Instance.EnableInteraction(curtainGroup);
    }


    // Toggles curtain sprite and disables interaction depending on time of day.
    void ToggleCurtainState()
    {
        // If it's the beginning of the day
        if (GameManager.Instance.beginningDay == true)
        {
            if (curtainDisplay.sprite == closedVariant)
            {
                // Debug.Log("Registered as closed");
                SpriteShift(curtainDisplay, openVariant);
                UIManager.Instance.DisableInteraction(curtainGroup);

                TriggerDay();
            }
            else
            {
                Debug.Log("Issue with logic on setup.");
            }
        }
        else
        {
            // If it's the last client of the day
            if (ClientLetter.Instance.currentDayClientsList.Count == 1)     // might change this check to after submitting treatment
            {
                if (curtainDisplay.sprite == openVariant)
                {
                    // Debug.Log("Registered as open");
                    SpriteShift(curtainDisplay, closedVariant);
                    UIManager.Instance.DisableInteraction(curtainGroup);
                }
                else
                {
                    Debug.Log("Issue with logic when trying to close shop.");
                }
            }
            
        }
    }

    // Changes the image's sprite to a known sprite
    void SpriteShift(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }

    // Begins the day
    void TriggerDay()
    {
        GameManager.Instance.SummonClient();
    }
}
