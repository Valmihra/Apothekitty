using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHerbsData : MonoBehaviour
{
    public class SingleHerb
    {
        public string _herbName;
        public string _herbDescription;
        public string _herbExtras;      // WILL REMOVE THIS ONE ONCE UPDATED TO NEW VERSION!!
        
        // WILL ADD THESE ONCE UPDATED TO NEW VERSION!!
        // public int _herbPotency;
        // public int _herbDietaryReference;

        public bool _canFortify;
        public bool _canHeal;
        public bool _canEase;
        public bool _targetsMind;
        public bool _targetsBody;
        public bool _targetsSpirit;

        public bool _isEnhancable;
        public bool _isInvertable;

        public void SetSingleHerbName(string newName)
        {
            _herbName = newName;
        }

        public void SetSingleHerbDescription(string description, string extras)
        {
            _herbDescription = description;
            _herbExtras = extras;
        }
        
        /*
                FOR NEW VERSION::
                -----------------
                
        public void SetSingleHerbExtras(int herbPotency, int herbDietaryReference)
        {
            if ((herbPotency > 3 || herbDietaryReference > 3) || (herbPotency == 0 || herbDietaryReference == 0))
            {
                Debug.Log("One or both of the values you are attempting to assign to " + _herbName +
                          "'s extras are invalid. Please ensure you assign a number between 1 and 3");
            }
            else
            {
                _herbPotency = herbPotency;     // == 1 ?);
                _herbDietaryReference = herbDietaryReference;
            }
        }
        */

        public void SetSingleHerbEffects(bool fortify, bool heal, bool ease)
        {
            _canFortify = fortify;
            _canHeal = heal;
            _canEase = ease;
        }

        public void SetSingleHerbTargets(bool mind, bool body, bool spirit)
        {
            _targetsMind = mind;
            _targetsBody = body;
            _targetsSpirit = spirit;
        }

        public void SetSingleHerbModifiers(bool enhance, bool invert)
        {
            _isEnhancable = enhance;
            _isInvertable = invert;
        }
    }

    
    private int herbCount = 12;
    public List<SingleHerb> herbDrawerContents;
    [SerializeField] private BotanicalCodexOnHover botanicalCodexOnHoverReference;  // was public

    private static AllHerbsData _instance;
    public static AllHerbsData Instance
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

    public void InitialiseAllHerbsData()
    {
        GenerateAllHerbs();
        AssignHerbsToDrawerScripts();
        botanicalCodexOnHoverReference = GetComponent<BotanicalCodexOnHover>();
        
        botanicalCodexOnHoverReference.InitialiseBotanicalCodex();
    }

    // Creates and sets up all herbs being used in the scene.
    void GenerateAllHerbs()
    {
        herbDrawerContents = new List<SingleHerb>();

        SingleHerb meltingRoot = new SingleHerb();
            meltingRoot.SetSingleHerbName("meltingRoot");
            meltingRoot.SetSingleHerbDescription("Growing close to the ground with its roots partially exposed, Meltingroot is a pale herb with no flowers or fruit.", "Treatment Rules:\n1. Does not work alongside Sweet Rot Cap\n2. Recipe needs a secondary effect target");
            meltingRoot.SetSingleHerbEffects(false, false, false);
            meltingRoot.SetSingleHerbTargets(false, true, false);
            meltingRoot.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(meltingRoot);

        SingleHerb spiceLeaf = new SingleHerb();
            spiceLeaf.SetSingleHerbName("spiceLeaf");
            spiceLeaf.SetSingleHerbDescription("Speckled with yellow spots, Spiceleaf is a small collection of red fruit that rests close to the ground. No flowers are present in bearing the fruit of this herb.", "Treatment Rules:\n1. Only works when Pupil Petal, Bumble Blooms, or Hexacore is present in the recipe");
            spiceLeaf.SetSingleHerbEffects(false, false, true);
            spiceLeaf.SetSingleHerbTargets(true, false, false);
            spiceLeaf.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(spiceLeaf);

        SingleHerb warmWhisper = new SingleHerb();
            warmWhisper.SetSingleHerbName("warmWhisper");
            warmWhisper.SetSingleHerbDescription("A luscious red herb, standing tall with its long stems.", "Treatment Rules:\n1. Recipe needs both a primary and secondary effect\n2. If using this herb to ease while a heal effect is also present in the recipe, the treatment will be nullified. However, the treatment is fine if used to heal while an ease effect is present");
            warmWhisper.SetSingleHerbEffects(false, true, true);
            warmWhisper.SetSingleHerbTargets(false, false, false);
            warmWhisper.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(warmWhisper);

        SingleHerb heavensHollyhock = new SingleHerb();
            heavensHollyhock.SetSingleHerbName("heavensHollyhock");
            heavensHollyhock.SetSingleHerbDescription("Delicate clusters of blooming white flowers that overlap amongst each other.", "Treatment Rules:\n1. Having a secondary effect / using another herb as an enhancer will nullify the treatment");
            heavensHollyhock.SetSingleHerbEffects(false, false, false);
            heavensHollyhock.SetSingleHerbTargets(true, false, true);
            heavensHollyhock.SetSingleHerbModifiers(true, false);
                herbDrawerContents.Add(heavensHollyhock);

        SingleHerb crystalMoss = new SingleHerb();
            crystalMoss.SetSingleHerbName("crystalMoss");
            crystalMoss.SetSingleHerbDescription("Attached to bark, Crystal Moss is a green herb with white growths. These growths are neither fruit nor flower.", "Treatment Rules:\n1. Only works as a secondary effect or as an enhancer\n2. Won't enhance without a secondary effect");
            crystalMoss.SetSingleHerbEffects(false, false, true);
            crystalMoss.SetSingleHerbTargets(false, true, true);
            crystalMoss.SetSingleHerbModifiers(true, false);
                herbDrawerContents.Add(crystalMoss);

        SingleHerb sweetRotCap = new SingleHerb();
            sweetRotCap.SetSingleHerbName("sweetRotCap");
            sweetRotCap.SetSingleHerbDescription("A short but wide purple fungi with light blue specks on its caps.", "Treatment Rules:\n1. Only safe for large animals to consume");
            sweetRotCap.SetSingleHerbEffects(true, true, false);
            sweetRotCap.SetSingleHerbTargets(false, false, true);
            sweetRotCap.SetSingleHerbModifiers(true, false);
                herbDrawerContents.Add(sweetRotCap);

        SingleHerb hexacore = new SingleHerb();
            hexacore.SetSingleHerbName("hexacore");
            hexacore.SetSingleHerbDescription("Named after the distinct hexagonal shape of their seed pods, the Hexacore is a primarily vibrant yellow plant with long stems.", "Treatment Rules:\n1. Only works in a treatment if the creature is a bird\n2. Only works when Meltingroot is also present in the recipe");
            hexacore.SetSingleHerbEffects(true, false, false);
            hexacore.SetSingleHerbTargets(false, false, false);
            hexacore.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(hexacore);

        SingleHerb crystalVine = new SingleHerb();
            crystalVine.SetSingleHerbName("crystalVine");
            crystalVine.SetSingleHerbDescription("A green herb with white growths. These growths are neither fruit nor flower.", "Treatment Rules:\n1. Having a secondary effect will nullify the treatment");
            crystalVine.SetSingleHerbEffects(false, false, true);
            crystalVine.SetSingleHerbTargets(false, true, false);
            crystalVine.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(crystalVine);

        SingleHerb watchersWeed = new SingleHerb();
            watchersWeed.SetSingleHerbName("watchersWeed");
            watchersWeed.SetSingleHerbDescription("Most recognisable for its 'eye-like' white growths.", "Treatment Rules:\n1. Only enhances when a recipe targets the mind");
            watchersWeed.SetSingleHerbEffects(false, false, false);
            watchersWeed.SetSingleHerbTargets(false, false, true);
            watchersWeed.SetSingleHerbModifiers(true, false);
                herbDrawerContents.Add(watchersWeed);

        SingleHerb pupilPetal = new SingleHerb();
            pupilPetal.SetSingleHerbName("pupilPetal");
            pupilPetal.SetSingleHerbDescription("Its red flowers stand on long spindly stems.", "Treatment Rules:\n1. Can only target the mind\n2. Won't work without an enhancer");
            pupilPetal.SetSingleHerbEffects(false, true, true);
            pupilPetal.SetSingleHerbTargets(false, false, false);
            pupilPetal.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(pupilPetal);

        SingleHerb queensReed = new SingleHerb();
            queensReed.SetSingleHerbName("queensReed");
            queensReed.SetSingleHerbDescription("A green and purple herb, bearing no flowers or fruit.", "Treatment Rules:\n1. Can only impact the treatment's primary effect or enhancer\n2. Only enhances when targeting the body");
            queensReed.SetSingleHerbEffects(false, false, false);
            queensReed.SetSingleHerbTargets(false, true, true);
            queensReed.SetSingleHerbModifiers(true, false);
                herbDrawerContents.Add(queensReed);

        SingleHerb bumbleBlooms = new SingleHerb();
            bumbleBlooms.SetSingleHerbName("bumbleBlooms");
            bumbleBlooms.SetSingleHerbDescription("Small yellow fruit with long green stems.", "Treatment Rules:\n1. Only works when effect is 'ease'\n2. needs two effects, won't work with an enhancer");
            bumbleBlooms.SetSingleHerbEffects(false, false, false);
            bumbleBlooms.SetSingleHerbTargets(true, false, false);
            bumbleBlooms.SetSingleHerbModifiers(false, false);
                herbDrawerContents.Add(bumbleBlooms);
    }

    void AssignHerbsToDrawerScripts()
    {
        string prefix = "Drawer ";
        string searchName;

        List<DrawerSensor> drawerSensorScripts = new List<DrawerSensor>();
        DrawerSensor temporaryReference;
        GameObject temporaryObject;
        for (int i = 1; i < herbCount + 1; i++)
        {
            searchName = prefix + i.ToString();
            temporaryObject = GameObject.Find(searchName);
            temporaryReference = temporaryObject.GetComponent<DrawerSensor>();

            if (temporaryReference != null)
            {
                drawerSensorScripts.Add(temporaryReference);
            }
        }

        // foreach (DrawerSensor d in drawerSensorScripts)
        // int herbNumber
        for (int i = 0; i < herbCount; i++)
        {
            drawerSensorScripts[i].FillDrawer(herbDrawerContents[i]._herbName);
        }

        // FOR DEBUGGING
        // int randomDraw = Random.Range(0, herbCount); //+1
        // Debug.Log("The herb in slot " + randomDraw + " is: " + herbDrawerContents[randomDraw]._herbName + ". The drawerSensor for the same slot has updated its contents as: " + drawerSensorScripts[randomDraw].drawerContents + ".");
    }

    public void UpdateAndShowNote(string herbNameToSearch)
    {
        foreach (SingleHerb s in herbDrawerContents)
        {
            if (s._herbName == herbNameToSearch)
            {
                botanicalCodexOnHoverReference.ReceiveInformation(s._herbName, s._herbDescription, s._herbExtras);
            }
        }
    }

    public void HideNote()
    {
        botanicalCodexOnHoverReference.botanicalCodexCanvasGroup.gameObject.SetActive(false);
        //UIManager.Instance.DisableUI(botanicalCodexOnHoverReference.botanicalCodexCanvasGroup);
    }

    /*public void UpdateNoteLocation(GameObject activeHerb)
    {
        Vector2 
    }
    public void SetNoteLocation(Vector2 initialPosition, )
    {
        //RectTransform rectTransform = botanicalCodexCanvasGroup.GetComponent<RectTransform>();
        //noteWidth = rectTransform.width;

        //calculate distance

        float distance = Screen.width - (eventData.position.x/Screen.width);
        if (distance <= noteWidth)
        {
            
        }
        ;
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            transform.position = eventData.position - delta;
    }*/
}
