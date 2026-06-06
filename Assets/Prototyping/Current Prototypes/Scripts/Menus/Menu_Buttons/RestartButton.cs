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
        restartButton.onClick.AddListener(delegate { RestartLevel(); });
    }

    
    void RestartLevel()
    {
        // hide menu? 
        GameManager.Instance.reloaded = true;
        GameManager.Instance.ResetScene();
        MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
    }
}
