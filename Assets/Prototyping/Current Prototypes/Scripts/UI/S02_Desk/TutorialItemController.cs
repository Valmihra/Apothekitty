using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialItemController : MonoBehaviour
{
    [SerializeField] private DraggableTutorialItem draggableTutorialItem01;
    [SerializeField] private DraggableTutorialItem draggableTutorialItem02;
    [SerializeField] private DraggableTutorialItem draggableTutorialItem03;

    [SerializeField] private TutorialBin tutorialBin;
    
    // [SerializeField] private Canvas tutorialItemControllerParentCanvas;
    
    private static TutorialItemController _instance;
    public static TutorialItemController Instance
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

    public void InitialiseTutorialItemsDesk()
    {
        tutorialBin.gameObject.SetActive(false);
        Rect tempRect = tutorialBin.CreateRectBounds();
        
        draggableTutorialItem01.InitialiseTutorialItem(tempRect);
        draggableTutorialItem02.InitialiseTutorialItem(tempRect);
        draggableTutorialItem03.InitialiseTutorialItem(tempRect);
    }

    public void ResetTutorialItemsDesk()
    {
        if (GameManager.Instance.runningTutorial)
        {
            draggableTutorialItem01.gameObject.SetActive(true);
            draggableTutorialItem02.gameObject.SetActive(true);
            draggableTutorialItem03.gameObject.SetActive(true);
            tutorialBin.gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }
    
    public void DebugDestroyTutorialItems()
    {
        Destroy(draggableTutorialItem01.gameObject);
        Destroy(draggableTutorialItem02.gameObject);
        Destroy(draggableTutorialItem03.gameObject);
        Destroy(tutorialBin.gameObject);
        
        DialogueRunner.Instance.deskIsClean = true;
        
        this.gameObject.GetComponent<Image>().raycastTarget = false;
        //Destroy();
    }

    public void DestroyTutorialItem(DraggableTutorialItem draggableTutorialItem)
    {
        Debug.Log("Calling failsafe destruction method...");
        tutorialBin.TutorialBinTossItem(draggableTutorialItem);
    }
}
