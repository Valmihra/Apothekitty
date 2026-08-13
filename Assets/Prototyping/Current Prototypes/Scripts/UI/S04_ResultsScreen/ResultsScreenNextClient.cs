using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsScreenNextClient : MonoBehaviour
{
    private Button nextClient;
    private TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        nextClient = GetComponent<Button>();
        buttonText = nextClient.GetComponentInChildren<TMP_Text>();

        /*if (buttonText != null)
        {
            Debug.Log("text found!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("Text is " + buttonText.text);
        }*/
        nextClient.onClick.AddListener(delegate {NextClient(); });    
    }

    void NextClient()
    {
        if (GameManager.Instance.runningTutorial)
        {
            GameManager.Instance.runningTutorial = false;
        }

        if (buttonText.text == "End Day")
        {
            Debug.Log("day ends here");
            // popup to say you've finished the demo, and send back to the main menu
        }
        else
        {
            GameManager.Instance.GoNextClient();
        }
        
    }
}
