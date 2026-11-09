using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbsPointAndClick : MonoBehaviour
{
        // HERBS INTERACTABLES

    public Button applesButton;
    public Button lichenButton;
    public Button salviaButton;
    public Button sproutsButton;
    public Button sapButton;

    public CanvasGroup herbalistsNote;
    public Button herbalistsNoteReturn;

    private List<Button> herbButtonsList;
    private List<Herb> herbList;
    //private List<string> 

    public TMP_Text herbName;
    public TMP_Text herbDescription;

    //public ButtonsNavigationGuides buttonControls;

    public class Herb
    {
        public string _herbName;
        public string _herbDescription;

        public void UpdateName(string newName)
        {
            _herbName = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            _herbDescription = newDescription;
        }
    }

    //

    // Start is called before the first frame update
    void Start()
    {
        //buttonControls = FindObjectOfType<ButtonsNavigationGuides>();

        CreateHerbs();
        InitialiseHerbs();
        
        //applesButton.onClick.AddListener(delegate {OpenHerbalistsNote()})
        applesButton.onClick.AddListener(delegate {GetListIndex(applesButton); });
        lichenButton.onClick.AddListener(delegate {GetListIndex(lichenButton); });
        salviaButton.onClick.AddListener(delegate {GetListIndex(salviaButton); });
        sproutsButton.onClick.AddListener(delegate {GetListIndex(sproutsButton); });
        sapButton.onClick.AddListener(delegate {GetListIndex(sapButton); });


        herbalistsNoteReturn.onClick.AddListener(delegate {HideHerbalistsNote(); });
    }

    void GetListIndex(Button selected)
    {
        int listIndex = herbButtonsList.IndexOf(selected);
        OpenHerbalistsNote(listIndex);
        /*int listIndex = 0;
        //
        foreach (Button b in herbButtonsList)
        {
            if (b != selected)
            {
                continue;
            }
            if (b == selected)
            {
                
            }
        }*/
    }


    void OpenHerbalistsNote(int listIndex)
    {
        string name = herbList[listIndex]._herbName;
        string description = herbList[listIndex]._herbDescription;
        
        herbName.text = name;
        herbDescription.text = description;
        //herbName.text = ;
        // WOULD SWAP TO UI MANAGER

        herbalistsNote.alpha = 1f;
        herbalistsNote.interactable = true;
        herbalistsNote.blocksRaycasts = true;
    }

    void HideHerbalistsNote()
    {
        herbalistsNote.alpha = 0f;
        herbalistsNote.interactable = false;
        herbalistsNote.blocksRaycasts = false;
    }

    void InitialiseHerbs()
    {
        //
        herbButtonsList = new List<Button>();
        herbButtonsList.Add(applesButton);
        herbButtonsList.Add(lichenButton);
        herbButtonsList.Add(salviaButton);
        herbButtonsList.Add(sproutsButton);
        herbButtonsList.Add(sapButton);
    }

    void CreateHerbs()
    {

        Herb apples = new Herb();
        apples.UpdateName("Apples");
        apples.UpdateDescription("Red fruit. Crisp and sweet.");

        Herb lichen = new Herb();
        lichen.UpdateName("Lichen");
        lichen.UpdateDescription("A fuzzy plant that grows on trees.");

        Herb salvia = new Herb();
        salvia.UpdateName("Salvia");
        salvia.UpdateDescription("A beautiful flowering shrub used in cleansing ceremonies. Toxic when ingested.");

        Herb sprouts = new Herb();
        sprouts.UpdateName("Sprouts");
        sprouts.UpdateDescription("The delicate leaves of a sprouting plant. Good for fortification or smth.");

        Herb sap = new Herb();
        sap.UpdateName("Sap");
        sap.UpdateDescription("Life essence collected from a wound on a tree. A strong binding agent.");


        herbList = new List<Herb>();
        herbList.Add(apples);
        herbList.Add(lichen);
        herbList.Add(salvia);
        herbList.Add(sprouts);
        herbList.Add(sap);

    }

}
