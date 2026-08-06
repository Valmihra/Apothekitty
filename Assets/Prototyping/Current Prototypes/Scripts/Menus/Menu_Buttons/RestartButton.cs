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


        if (GameManager.Instance.onMainMenu)
        {
            //
            MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenu);
        }
        else
        {
            MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
            //GameManager.Instance.GoMainMenu();
            // if this doesn't work, can try just exit and open main menu in menu manager,, ((!!))
        }
        
        GameManager.Instance.ResetScene();
        
    }
}
