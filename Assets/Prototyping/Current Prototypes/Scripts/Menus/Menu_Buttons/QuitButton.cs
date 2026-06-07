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
        Debug.Log("got to pressing button");
        MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
        Debug.Log("passed closing menu");
        MenuManager.Instance.ExitGamePopup();
        Debug.Log("called the exit popup");
    }
}
