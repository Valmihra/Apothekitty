using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsScreen : MonoBehaviour
{

    private CanvasGroup resultsScreen;
    public CanvasGroup resultsIconCanvas;
    public CanvasGroup cure;
    public CanvasGroup fail;
    
	private string positiveResult = "yes";
	private string negativeResult = "no";

    public TMP_Text displayedPatientName;
    public TMP_Text ailmentResult;
    public TMP_Text recipeResult;
    public TMP_Text herbsResult;
    
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

    public void InitialiseResultsScreen()
    {
        resultsScreen = GetComponent<CanvasGroup>();
    }

	public void HideResultsScreen()
	{
		UIManager.Instance.DisableUI(resultsScreen);
        PrepNextClientResultScreen();
	}

	public void PrepNextClientResultScreen()
	{
		UIManager.Instance.DisableUI(resultsIconCanvas);
        UIManager.Instance.DisableUI(cure);
        UIManager.Instance.DisableUI(fail);
        
        UIManager.Instance.HideTextComponent(ailmentResult);
        UIManager.Instance.HideTextComponent(recipeResult);
        UIManager.Instance.HideTextComponent(herbsResult);
	}

    public void GenerateResultsScreen(string name, bool ailment, bool recipe, bool herbs)
    {
        // displayedPatientName.text = PatientData.Instance.activePatientData._patientName;
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
        Debug.Log(nameToSearch);

        iconToFind = GameObject.Find(nameToSearch);
        Debug.Log(iconToFind);
        icon = iconToFind.GetComponent<Image>();
        UIManager.Instance.SpriteShift(resultsIcon, icon.sprite);
        StartShowResults();
        
    }

    void StartShowResults()
    {
        UIManager.Instance.EnableUI(resultsScreen);
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
