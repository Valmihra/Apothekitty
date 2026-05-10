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
                //temporary lmao
    private string correctAilment = "yes";
    private string incorrectAilment = "no";
    private string correctRecipe = "yes";
    private string incorrectRecipe = "no";
    private string correctHerbs = "yes";
    private string incorrectHerbs = "no";

    public TMP_Text clientName;
    public TMP_Text ailmentResult;
    public TMP_Text recipeResult;
    public TMP_Text herbsResult;
    
    public Image resultsIcon;
    //public Image resultsLogo;



    private bool clientCured;
    private Color textHidden;
    private Color textDisplayed;

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

    void Start()
    {
        //clientCured = false;
        resultsScreen = GetComponent<CanvasGroup>();
        textDisplayed = Color.black;
        textHidden = new Color (1,1,1,0);
        InitialiseResultsDisplay();
    }

    void InitialiseResultsDisplay()
    {
        UIManager.Instance.DisableUI(resultsScreen);
        UIManager.Instance.DisableUI(resultsIconCanvas);
        UIManager.Instance.DisableUI(cure);
        UIManager.Instance.DisableUI(fail);
        ChangeColour(ailmentResult, textHidden);
        ChangeColour(recipeResult, textHidden);
        ChangeColour(herbsResult, textHidden);
    }

    void ChangeColour(TMP_Text text, Color colour)
    {
        text.color = colour;
    }


    public void GenerateResultsScreen(bool ailment, bool recipe, bool herbs)
    {
        clientName.text = ClientLetter.Instance.clientLetter._clientName;

        
        ailmentResult.text = ailment ? correctAilment : incorrectAilment;
        recipeResult.text = recipe ? correctRecipe : incorrectRecipe;
        herbsResult.text = herbs ? correctHerbs : incorrectHerbs;
        
        if (!herbs)
        {
            clientCured = false;
        }
        else
        {
            clientCured = true;
        }

        UpdateResultsScreen();
    }

    void UpdateResultsScreen()
    {
        GameObject iconToFind;
        Image icon;
        string nameToSearch;
        if (clientCured)
        {
            nameToSearch = ("Result - " + ClientLetter.Instance.clientLetter._clientName + " - Cured");
            //resultsLogo.sprite = 
        }
        else
        {
            nameToSearch = ("Result - " + ClientLetter.Instance.clientLetter._clientName + " - Failed");
        }
        Debug.Log(nameToSearch);

        iconToFind = GameObject.Find(nameToSearch);
        Debug.Log(iconToFind);
        icon = iconToFind.GetComponent<Image>();
        resultsIcon.sprite = icon.sprite;
        StartShowResults();
        
    }

    void StartShowResults()
    {
        UIManager.Instance.EnableUI(resultsScreen);
        Invoke(nameof(ShowAilmentResult), 1f);
    }

    void ShowAilmentResult()
    {
        ChangeColour(ailmentResult, textDisplayed);
        Invoke(nameof(ShowRecipeResult), 1f);
    }

    void ShowRecipeResult()
    {
        ChangeColour(recipeResult, textDisplayed);
        Invoke(nameof(ShowFinalResult), 1f);
    }

    void ShowFinalResult()
    {
        ChangeColour(herbsResult, textDisplayed);
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
