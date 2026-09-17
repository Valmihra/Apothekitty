using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialItemController : MonoBehaviour
{
    [SerializeField] 
	private DraggableTutorialItem draggableTutorialItem01;
    [SerializeField] 
	private DraggableTutorialItem draggableTutorialItem02;
    [SerializeField] 
	private DraggableTutorialItem draggableTutorialItem03;

    [SerializeField] 
	private TutorialBin tutorialBin;
    
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
        draggableTutorialItem01.gameObject.SetActive(false);
        draggableTutorialItem02.gameObject.SetActive(false);
        draggableTutorialItem03.gameObject.SetActive(false);
        tutorialBin.gameObject.SetActive(false);
        // foreach ()
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
}
