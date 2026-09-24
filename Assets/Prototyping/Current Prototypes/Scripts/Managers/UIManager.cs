using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private CanvasGroup canvasToHighlight;

    public CanvasGroup arrows;
    public CanvasGroup curtain;
    public CanvasGroup ailmentIcon;
    public CanvasGroup modifiers; 
    public CanvasGroup grimoire;
    public CanvasGroup herbGuide;
    public CanvasGroup effect;
    public CanvasGroup target;

    private static UIManager _instance;
    public static UIManager Instance
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
    
    // Scripts responsible for toggling between canvas groups
    // // *TAG* - Maybe add separate enable/disable but instead of canvasgroups it's object.SetActive = true // false?
    public void EnableUI(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableUI(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void HideTextComponent(TMP_Text textToHide)
    {
        textToHide.enabled = false;
    }

    public void ShowTextComponent(TMP_Text textToShow)
    {
        textToShow.enabled = true;
    }

    public void SpriteShift(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }
    
    public void EnableInteraction(CanvasGroup canvasGroup)
    {
        // keeps canvasgroup visible, and allows interaction
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableInteraction(CanvasGroup canvasGroup)
    {
        // keeps canvasgroup visible, and prevents interaction
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void HighlightCanvasElement(string targetObjectName)
    {
        if (targetObjectName == "curtain")
        {
            canvasToHighlight = curtain;
        }
        else if (targetObjectName == "arrows")
        {
            canvasToHighlight = arrows;
        }
        else if (targetObjectName == "ailmentIcon")
        {
            canvasToHighlight = ailmentIcon;
        }
        else if (targetObjectName == "modifiers")
        {
            canvasToHighlight = modifiers; 
        }
        else if (targetObjectName == "grimoire")
        {
            canvasToHighlight = grimoire; 
        }
        else if (targetObjectName == "herbGuide")
        {
            canvasToHighlight = herbGuide; 
        }
        else if (targetObjectName == "effect")
        {
            canvasToHighlight = effect; 
        }
        else if (targetObjectName == "target")
        {
            canvasToHighlight = target; 
        }

        canvasToHighlight.GetComponent<HighlightObject>().PerformPulse();
    }

}