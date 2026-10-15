using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    [Header("Debugger Buttons")]
    public Button skipTutorialButton;
    public Button jumpDayTwoButton;
    public Button toggleCursorButton;
    
    
    void Start()
    {
        skipTutorialButton.onClick.AddListener(delegate {SkipTutorial(); });
        jumpDayTwoButton.onClick.AddListener(delegate {JumpDayTwo(); });
        toggleCursorButton.onClick.AddListener(delegate { ToggleCursor(); });
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

    void ToggleCursor()
    {
        _PrototypeCursor.Instance.ToggleCursorType();
    }
}
