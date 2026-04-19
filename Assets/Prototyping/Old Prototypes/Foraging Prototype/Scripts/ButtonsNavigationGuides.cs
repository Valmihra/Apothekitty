using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsNavigationGuides : MonoBehaviour
{
    public Button diagnosisSheetButton;
    public Button herbalistsGuideButton;

    public Button diagnosisSheetReturn;
    public Button herbalistsGuideReturn;

    public CanvasGroup diagnosisSheet;
    public CanvasGroup herbalistsGuide;
    public CanvasGroup herbalistsNote;
    public CanvasGroup guides;
    public CanvasGroup inventory;
    

    public List<CanvasGroup> canvasGroups;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseList();
        
        //diagnosisSheetButton.onClick.AddListener(delegate {UIManager.Global.EnableUI(diagnosisSheet); });
        diagnosisSheetButton.onClick.AddListener(delegate {ShowOverlay(diagnosisSheet); });
        herbalistsGuideButton.onClick.AddListener(delegate {ShowOverlay(herbalistsGuide); });
            // will switch around to UIManager later!
        
        diagnosisSheetReturn.onClick.AddListener(delegate {HideOverlay(diagnosisSheet); });
        herbalistsGuideReturn.onClick.AddListener(delegate {HideOverlay(herbalistsGuide); });

        HideGroups();
    }


    /*PauseGame()
    {
        //

        //!gamedata.isPaused;

        //
    }*/
    
    void HideGroups()
    {
        //
        diagnosisSheet.alpha = 0f;
        diagnosisSheet.interactable = false;
        diagnosisSheet.blocksRaycasts = false;
        herbalistsGuide.alpha = 0f;
        herbalistsGuide.interactable = false;
        herbalistsGuide.blocksRaycasts = false;
        herbalistsNote.alpha = 0f;
        herbalistsNote.interactable = false;
        herbalistsNote.blocksRaycasts = false;
    }
    
    void InitialiseList()
    {
        canvasGroups = new List<CanvasGroup>();
        canvasGroups.Add(guides);
        canvasGroups.Add(inventory);
        //canvasGroups.Add(herbalistsGuide);
        //canvasGroups.Add(diagnosisSheet);
    }

    void ShowOverlay(CanvasGroup canvasGroup)
    {
        foreach (CanvasGroup c in canvasGroups)
        {
            c.alpha = 0.5f;
            c.interactable = false;
            //!c.blocksRaycasts;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void HideOverlay(CanvasGroup canvasGroup)
    {
        //Debug.Log("click registered");
        foreach (CanvasGroup c in canvasGroups)
        {
            c.alpha = 1f;
            c.interactable = true;
            //!c.blocksRaycasts;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /*EnableUI(CanvasGroup canvasGroup)
    {
        //
    }

    DisableUI(CanvasGroup canvasGroup)
    {
        //
    }*/
}
