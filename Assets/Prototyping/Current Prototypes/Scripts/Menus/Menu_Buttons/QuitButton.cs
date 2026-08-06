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
        //Debug.Log("got to pressing button");
        if (GameManager.Instance.onMainMenu)
        {
            //MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenu);         nvm,,
            //Debug.Log("passed closing menu");
            MenuManager.Instance.ExitGamePopup();
            //Debug.Log("called the exit popup");
            
        }
        else
        {
            MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
            GameManager.Instance.GoMainMenu();
            // if this doesn't work, can try just exit and open main menu in menu manager,, ((!!))
        }
        
        
    }
}
