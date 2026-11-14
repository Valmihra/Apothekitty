using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    public CanvasGroup resultsIconCanvas;
    public CanvasGroup cure;
    public CanvasGroup fail;
    
	private string positiveResult = "yes";
	private string negativeResult = "no";

    public TMP_Text displayedPatientName;
    public TMP_Text ailmentResult;
    public TMP_Text recipeResult;
    public TMP_Text herbsResult;
    public TMP_Text patientCheckupLetter;
    
    public Image resultsIcon;

    private bool clientCured;

    private static ResultsScreen _instance;
    public static ResultsScreen Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

	public void HideResultsScreen()
	{
		UIManager.Instance.DisableUI(SceneManager.Instance.mainCanvasGroupResultsScreen);
        PrepNextClientResultScreen();
        SoundManager.Instance.GetAudioToReturnTo();
	}

	public void PrepNextClientResultScreen()
	{
		UIManager.Instance.DisableUI(resultsIconCanvas);
        UIManager.Instance.DisableUI(cure);
        UIManager.Instance.DisableUI(fail);
        
        UIManager.Instance.HideTextComponent(ailmentResult);
        UIManager.Instance.HideTextComponent(recipeResult);
        UIManager.Instance.HideTextComponent(herbsResult);
        UIManager.Instance.HideTextComponent(patientCheckupLetter);

	}

    public void GenerateResultsScreen(string name, bool ailment, bool recipe, bool herbs)
    {
		displayedPatientName.text = name;
        
        ailmentResult.text = ailment ? positiveResult : negativeResult;
        recipeResult.text = recipe ? positiveResult : negativeResult;
        herbsResult.text = herbs ? positiveResult : negativeResult;
        
        if (!herbs)
        {
            clientCured = false;
        }
        else
        {
            clientCured = true;
        }

        UpdateResultsScreen(displayedPatientName.text);
    }

    void UpdateResultsScreen(string name)
    {
        patientCheckupLetter.text = PatientData.Instance.ReturnPatientCheckupLetter(name, clientCured);
        
        GameObject iconToFind;
        Image icon;
        string nameToSearch;
        
        if (clientCured)
        {
            nameToSearch = ("Result - " + name + " - Cured");
        }
        else
        {
            nameToSearch = ("Result - " + name + " - Failed");
        }
        
        iconToFind = GameObject.Find(nameToSearch);
        icon = iconToFind.GetComponent<Image>();
        UIManager.Instance.SpriteShift(resultsIcon, icon.sprite);
        
        // had the checkup letter returning here, but have got it elsewhere instead now!
            
        StartShowResults();
        
    }

    void StartShowResults()
    {
        SoundManager.Instance.GetResultsScreen();
        
        UIManager.Instance.EnableUI(SceneManager.Instance.mainCanvasGroupResultsScreen);
        Invoke(nameof(ShowAilmentResult), 1f);
    }

    void ShowAilmentResult()
    {
        UIManager.Instance.ShowTextComponent(ailmentResult);
        Invoke(nameof(ShowRecipeResult), 1f);
    }

    void ShowRecipeResult()
    {
        UIManager.Instance.ShowTextComponent(recipeResult);
        Invoke(nameof(ShowFinalResult), 1f);
    }

    void ShowFinalResult()
    {
        UIManager.Instance.ShowTextComponent(herbsResult);
        UIManager.Instance.ShowTextComponent(patientCheckupLetter);

        UIManager.Instance.EnableUI(resultsIconCanvas);
        if (clientCured)
        {
            UIManager.Instance.EnableUI(cure);
        }
        else
        {
            UIManager.Instance.EnableUI(fail);
        }
    }
}
