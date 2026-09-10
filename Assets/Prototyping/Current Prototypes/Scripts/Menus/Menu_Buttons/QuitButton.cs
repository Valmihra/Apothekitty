using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quitButton;

    void Start()
    {
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(delegate { ExitGame(); });
    }

    
    void ExitGame()
    {
        if (GameManager.Instance.onMainMenu)
        {
            MenuManager.Instance.ExitGamePopup();
        }
        else
        {
            MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
            GameManager.Instance.GoMainMenu();
        }
    }
}
