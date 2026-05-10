using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(delegate { PauseGame(); });
    }

    
    void PauseGame()
    {
        MenuManager.Instance.OpenMenu(MenuManager.Instance.pauseMenu);
    }
}
