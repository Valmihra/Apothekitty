using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressLevelButton : MonoBehaviour
{
	// Button to be displayed on the popup between clients after submission
    private Button progressLevelButton;
	private TMP_Text progressLevelButtonText;
	// Text displayed on the popup button
    string noPatientsLeftToTreatText = "Finish day";
    string patientsLeftToTreatText = "Next patient";
    
    public bool patientsRemaining;
    
    void Awake()
    {
        patientsRemaining = true;
		progressLevelButton = GetComponent<Button>();
        progressLevelButton.onClick.AddListener(delegate { OnProgressLevelButtonPressed(); });
        progressLevelButtonText = progressLevelButton.GetComponentInChildren<TMP_Text>();
    }
    
	// Called from MenuManager!
    public void UpdateProgressLevelButtonText()
    {
        if (patientsRemaining)
        {
            progressLevelButtonText.text = patientsLeftToTreatText;
        }
        else
        {
            progressLevelButtonText.text = noPatientsLeftToTreatText;
        }
    }

	void OnProgressLevelButtonPressed()
    {
        if (GameManager.Instance.runningTutorial)
        {
			// Could maybe reset delegation to separate method after this so it's only checked the one time?
            GameManager.Instance.runningTutorial = false;
        }
        
        
        if (patientsRemaining)
        {
			MenuManager.Instance.ExitMenu(MenuManager.Instance.progressMenuCanvasGroup);
            GameManager.Instance.GoNextClient();
        }
        else
        {
			// close the curtain? little anim? some sauce??
			MenuManager.Instance.ExitMenu(MenuManager.Instance.progressMenuCanvasGroup);
			GameManager.Instance.GoResultsScreen();
        }
    }
}
