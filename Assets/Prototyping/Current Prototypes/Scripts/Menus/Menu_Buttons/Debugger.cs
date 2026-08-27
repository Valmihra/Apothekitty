using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    // in case we need to test other things too,,
    public Button skipTutorialButton;
    public Button jumpDayTwoButton;

    // Start is called before the first frame update
    void Start()
    {
        //skipTutorialButton = GetComponent<Button>();
        skipTutorialButton.onClick.AddListener(delegate {SkipTutorial(); });
        
        jumpDayTwoButton.onClick.AddListener(delegate {JumpDayTwo(); });
    }

    void SkipTutorial()
    {
        Debug.Log("Skipping tutorial.");
        GameManager.Instance.runningTutorial = false;
        Debug.Log("runningTutorial is now " + GameManager.Instance.runningTutorial);
    }

    void JumpDayTwo()
    {
        GameManager.Instance.DebugJumpToDayNumber(2);

    }
}
