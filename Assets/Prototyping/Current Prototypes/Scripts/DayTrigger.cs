using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DayTrigger : MonoBehaviour, IPointerClickHandler
{
    //opening the curtain serves as the "on" switch for the whole day


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
        Debug.Log("Click registered.");
        ToggleCurtainState();
    }

    // Closes the curtain and resets interaction on its canvasgroup component, acting as a 'reset' button
    public void ResetCurtain()
    {
        if (curtainDisplay.sprite != closedVariant)
        {
            Debug.Log("Resetting the curtain display.");
            SpriteShift(curtainDisplay, closedVariant);
            UIManager.Instance.EnableInteraction(curtainGroup);
        }
        return;
    }


    // Checks for the game state and opens or closes the curtain accordingly
    void ToggleCurtainState()
    {
        // If it's the start of the day, open the curtain and disable its interaction to keep it static in the scene.
        if (GameManager.Instance.beginningDay == true)
        {
            if (curtainDisplay.sprite == closedVariant)
            {
                Debug.Log("Registered as closed");
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
            // If it's not the start of the day, it should only become interactable again at the end.
            // This checks to make sure the sprite is the correct variant, and closes the curtain for the rest of the day.
            if (curtainDisplay.sprite == openVariant)
            {
                Debug.Log("Registered as open");
                SpriteShift(curtainDisplay, closedVariant);
                UIManager.Instance.DisableInteraction(curtainGroup);
            }
            else
            {
                Debug.Log("Issue with logic when trying to close shop.");
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
        GameManager.Instance.BeginDay();
    }
}
