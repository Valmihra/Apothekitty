using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button restartButton;

    void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(delegate { OnRestartButtonPushed(); });
    }
    
    void OnRestartButtonPushed()
    {
        MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
        GameManager.Instance.RestartLevel();
    }
}
