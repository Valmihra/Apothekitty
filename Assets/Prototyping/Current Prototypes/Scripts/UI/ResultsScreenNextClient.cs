using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenNextClient : MonoBehaviour
{
    private Button nextClient;

    // Start is called before the first frame update
    void Start()
    {
        nextClient = GetComponent<Button>();
        nextClient.onClick.AddListener(delegate {NextClient(); });    
    }

    void NextClient()
    {
        if (GameManager.Instance.runningTutorial)
        {
            GameManager.Instance.runningTutorial = false;
        }
        GameManager.Instance.GoNextClient();
    }
}
