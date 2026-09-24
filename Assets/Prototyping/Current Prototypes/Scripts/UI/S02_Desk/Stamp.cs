using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stamp : MonoBehaviour, IPointerClickHandler
{
    // private bool canUseStamp;
    private bool stampIsActive;
    private bool currentlyHoldingStamp;
    
    private Rect stampRect;
    private Rect stampDockRect;
    private Rect grimoireRect;
    private Image stampImage;
    private Sprite stampUp;
    private Sprite stampDown;
    private Vector2 initialStampPosition;

    public void InitialiseStamp()
    {
        initialStampPosition = transform.position;
        stampImage = GetComponent<Image>();
        stampUp = GameObject.Find("Stamp - Up").GetComponent<Image>().sprite;
        stampDown = GameObject.Find("Stamp - Down").GetComponent<Image>().sprite;
        
        // Sets the rect for the stamp
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        Vector2 min = corners[0];
        Vector2 max = corners[2];
        Vector2 size = max - min;

        stampRect = new Rect(min, size);
        stampDockRect = stampRect;
    }
    
    public void ResetStamp()
    {
        Debug.Log("Returning stamp to dock and resetting the hover script.");
        ReturnStampToDock();
        UIManager.Instance.SpriteShift(stampImage, stampDown);
        AddHoverScript();
    }

    void ReturnStampToDock()
    {
        Debug.Log("Returning stamp to dock.");
        stampIsActive = false;
        currentlyHoldingStamp = false;
        // Returns stamp to dock
        UpdateStampPosition(initialStampPosition);
    }

    void AddHoverScript()
    {
        if (gameObject.TryGetComponent<MouseHover>(out MouseHover mouseHover))
        {
            Debug.Log("Hover script already attached.");
        }
        else
        {
            this.gameObject.AddComponent(typeof(MouseHover));
            mouseHover = gameObject.GetComponent<MouseHover>();
            Debug.Log("Hover script attached.");
        }
    }

    void RemoveHoverScript()
    {
        if (this.gameObject.TryGetComponent<MouseHover>(out MouseHover mouseHover))
        {
            Debug.Log("Destroying the hover script.");
            Destroy(mouseHover);
        }
    }
    
    void UpdateStampPosition(Vector3 position)
    {
        transform.position = position;
        stampRect.center = position;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ensures interaction only works if ailment isn't already submitted
        if (!GameManager.Instance.ailmentSubmitted)
        {
            // Gets the location of the grimoire and stamp onscreen at the time of the click
            grimoireRect = GrimoirePagesData.Instance.GetAndReturnGrimoireRect();
            UpdateStampPosition(eventData.pressPosition);
        
            // Picks up the stamp if not currently holding it
            if (!currentlyHoldingStamp)
            {
                // Brings the parent object of the stamp to the front of the screen
                if (GameObject.Find("Panel - Stamp Dock").TryGetComponent<RectTransform>(out RectTransform rectTransform))
                {
                    rectTransform.SetAsLastSibling();
                }
                
                UIManager.Instance.SpriteShift(stampImage, stampUp);
                // GameObject.Find("Panel - Stamp Dock").GetComponent<RectTransform>().SetAsLastSibling();
                
                currentlyHoldingStamp = true;
                stampIsActive = true;
                PickUpStamp();
            }
            else
            {
                // Uses location at time of the click to determine what to do
                if (stampRect.Overlaps(grimoireRect))
                {
                    if (GrimoirePagesData.Instance.SelectedPageIsNotTheCover())
                    {
                        stampIsActive = false;
                        UIManager.Instance.SpriteShift(stampImage, stampDown);
                        // Debug.Log("Selecting ailment before returning stamp to dock.");
                        GrimoirePagesData.Instance.TempSelectionCheck();

                        if (this.gameObject.TryGetComponent<MouseHover>(out MouseHover mouseHover))
                        {
                            Debug.Log("Destroying the hover script.");
                            Destroy(mouseHover);
                        }
                        
                        ReturnStampToDock();
                        return;
                    }
                    else
                    {
                        Debug.Log("Invalid selection.");
                        ReturnStampToDock();
                        return;
                    }
                }
                
                if (stampRect.Overlaps(stampDockRect))
                {
                    ReturnStampToDock();
                    return;
                }
            }
        }
    }
    
    void PickUpStamp()
    {
        StopAllCoroutines();
        StartCoroutine(FollowCursor());
    }

    IEnumerator FollowCursor()
    {
        Debug.Log("Inside Enumerator");

        while (stampIsActive)
        {
            Vector3 mousePosition = Input.mousePosition;
            UpdateStampPosition(mousePosition);

            yield return null;
        }
    }
}
