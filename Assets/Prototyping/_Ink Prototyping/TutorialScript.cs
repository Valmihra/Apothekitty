using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using Ink.UnityIntegration;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private InkFile inkFile;
    //[SerializeField]
    //private TextAsset inkAsset;     // public TextAsset inkAsset;
    
    private Story _inkStory;
    private bool needsStory;

    // UI
    //[SerializeField]
    //private GameObject canvas;          // public Canvas canvas;
    [SerializeField]
    private float elementPadding;   // = 20;
    
    [SerializeField]
    private TMP_Text text;              // public TMP_Text text;
    [SerializeField]
    private Button button;
    
    private float offset;

    [SerializeField]
    private GameObject textHolder;
    [SerializeField]
    private GameObject buttonHolder;

    void Awake()
    {
        // creates a new Story object to hold the information from the ink file
        //inkAsset = inkFile.
        //_inkStory = new Story(inkAsset.text);
        
        _inkStory = new Story(inkFile.storyJson);
        
        //inkAsset = 
        //Compile(inkFile);
        //_inkStory = new Story(inkFile.sourceJsonString);
        
        needsStory = true;

        //compiledInkAsset = inkAsset
    }

    // Update is called once per frame
    void Update()
    {
        if (needsStory == true)
        {
            RemoveChildren();
            offset = 0;

            while (_inkStory.canContinue)
            {
                //Debug.Log(_inkStory.Continue());
                //TMP_Text storyText = Instantiate(text) as 
                TMP_Text storyText = Instantiate (text) as TMP_Text;
                storyText.text = _inkStory.Continue();   // text = _inkStory.Continue();
                storyText.transform.SetParent(textHolder.transform, false);             // storyText.transform.SetParent(canvas.transform, false);
                storyText.transform.Translate(new Vector2(0, offset));
                offset -= (storyText.fontSize + elementPadding);

                //LayoutRebuilder.ForceRebuildLayoutImmediate(buttonHolder.GetComponent<RectTransform>());

                // LATER VER? uhhh
                // for loop that counts all in current section and adds them all into one single text node instead? 
                // maybe?
            }

            if (_inkStory.currentChoices.Count > 0)
            {
                // maybe set as a function? actually no? UHHHHH
                // ?????????? uhhhHHHHHHHH???????????
                for (int i = 0; i < _inkStory.currentChoices.Count; i++)
                {
                    Button choiceButton = Instantiate(button) as Button;
                    choiceButton.transform.SetParent(buttonHolder.transform, false);    // choiceButton.transform.SetParent(canvas.transform, false);
                    choiceButton.transform.Translate(new Vector2(0, offset));

                    TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
                    choiceText.text = _inkStory.currentChoices[i].text;

                    HorizontalLayoutGroup layoutGroup = choiceButton.GetComponent<HorizontalLayoutGroup>();

                    int choiceNumber = i;
                    choiceButton.onClick.AddListener(delegate {PressedButton(choiceNumber); });

                    offset -= (choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding);

                    Choice choice = _inkStory.currentChoices[i];
                    //Debug.Log("Choice " + (i + 1) + ": " + choice.text);

                    //LayoutRebuilder.ForceRebuildLayoutImmediate(buttonHolder.GetComponent<RectTransform>());
                }
            }

            needsStory = false;
        }

    }

    void RemoveChildren()
    {
        /*int childrenNumber = canvas.transform.childCount;
        for (int i = childrenNumber - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }*/

        int childrenNumber = textHolder.transform.childCount;
        for (int i = childrenNumber - 1; i >= 0; --i)
        {
            GameObject.Destroy(textHolder.transform.GetChild(i).gameObject);
        }

        childrenNumber = buttonHolder.transform.childCount;
        for (int i = childrenNumber - 1; i >= 0; --i)
        {
            GameObject.Destroy(buttonHolder.transform.GetChild(i).gameObject);
        }
    }

    public void PressedButton(int number)
    {
        Debug.Log("Choice made.");
        _inkStory.ChooseChoiceIndex(number);
        needsStory = true;
    }

    /*void Compile()
    {
        var compiler = new Ink.Compiler(inkFileContents);
    }*/
}
