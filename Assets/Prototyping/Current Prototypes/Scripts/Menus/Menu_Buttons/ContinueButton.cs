using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private Button continueGameButton;

    void Start()
    {
        continueGameButton = GetComponent<Button>();
        continueGameButton.onClick.AddListener(delegate { ContinueGame(); });
    }

    // Exits the menu and starts a new game
    void ContinueGame()
    {
        GameManager.Instance.LoadGame();
    }
}
