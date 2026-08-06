using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHerbsData : MonoBehaviour
{
    public class SingleHerb
    {
        public string herbName;
        // would add any extra herb information here
        public string herbDescription;
        public string herbExtras;

        public bool isFortify;
        public bool isHeal;
        public bool isEase;
        public bool isMind;
        public bool isBody;
        public bool isSpirit;

        public bool isEnhancable;
        public bool isInvertable;
        


        public void UpdateName(string newName)
        {
            herbName = newName;
        }

        public void UpdateDetails(string description, string extras)
        {
            herbDescription = description;
            herbExtras = extras;
        }

        public void SetEffects(bool fortify, bool heal, bool ease)
        {
            isFortify = fortify;
            isHeal = heal;
            isEase = ease;
        }

        public void SetTargets(bool mind, bool body, bool spirit)
        {
            isMind = mind;
            isBody = body;
            isSpirit = spirit;
        }

        public void SetModifiers(bool enhance, bool invert)
        {
            isEnhancable = enhance;
            isInvertable = invert;
        }
    }

    public List<SingleHerb> herbDrawerContents;
    private int herbCount = 12;

    public HerbalistNotesOnHover herbalistNotesOnHover;

    private static AllHerbsData _instance;
    public static AllHerbsData Instance
    {
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        
        GenerateAllHerbs();
        AssignHerbsToDrawerScripts();
        herbalistNotesOnHover = GetComponent<HerbalistNotesOnHover>();
    }

    // Creates and sets up all herbs being used in the scene.
    void GenerateAllHerbs()
    {
        herbDrawerContents = new List<SingleHerb>();

        SingleHerb meltingRoot = new SingleHerb();
            meltingRoot.UpdateName("meltingRoot");
            meltingRoot.UpdateDetails("Growing close to the ground with its roots partially exposed, Meltingroot is a pale herb with no flowers or fruit.", "Treatment Rules:\n1. Does not work alongside Sweet Rot Cap\n2. Recipe needs a secondary effect target");
            meltingRoot.SetEffects(false, false, false);
            meltingRoot.SetTargets(false, true, false);
            meltingRoot.SetModifiers(false, false);
                herbDrawerContents.Add(meltingRoot);

        SingleHerb spiceLeaf = new SingleHerb();
            spiceLeaf.UpdateName("spiceLeaf");
            spiceLeaf.UpdateDetails("Speckled with yellow spots, Spiceleaf is a small collection of red fruit that rests close to the ground. No flowers are present in bearing the fruit of this herb.", "Treatment Rules:\n1. Only works when Pupil Petal, Bumble Blooms, or Hexacore is present in the recipe");
            spiceLeaf.SetEffects(false, false, true);
            spiceLeaf.SetTargets(true, false, false);
            spiceLeaf.SetModifiers(false, false);
                herbDrawerContents.Add(spiceLeaf);

        SingleHerb warmWhisper = new SingleHerb();
            warmWhisper.UpdateName("warmWhisper");
            warmWhisper.UpdateDetails("A luscious red herb, standing tall with its long stems.", "Treatment Rules:\n1. Recipe needs both a primary and secondary effect\n2. If using this herb to ease while a heal effect is also present in the recipe, the treatment will be nullified. However, the treatment is fine if used to heal while an ease effect is present");
            warmWhisper.SetEffects(false, true, true);
            warmWhisper.SetTargets(false, false, false);
            warmWhisper.SetModifiers(false, false);
                herbDrawerContents.Add(warmWhisper);

        SingleHerb heavensHollyhock = new SingleHerb();
            heavensHollyhock.UpdateName("heavensHollyhock");
            heavensHollyhock.UpdateDetails("Delicate clusters of blooming white flowers that overlap amongst each other.", "Treatment Rules:\n1. Having a secondary effect / using another herb as an enhancer will nullify the treatment");
            heavensHollyhock.SetEffects(false, false, false);
            heavensHollyhock.SetTargets(true, false, true);
            heavensHollyhock.SetModifiers(true, false);
                herbDrawerContents.Add(heavensHollyhock);

        SingleHerb crystalMoss = new SingleHerb();
            crystalMoss.UpdateName("crystalMoss");
            crystalMoss.UpdateDetails("Attached to bark, Crystal Moss is a green herb with white growths. These growths are neither fruit nor flower.", "Treatment Rules:\n1. Only works as a secondary effect or as an enhancer\n2. Won't enhance without a secondary effect");
            crystalMoss.SetEffects(false, false, true);
            crystalMoss.SetTargets(false, true, true);
            crystalMoss.SetModifiers(true, false);
                herbDrawerContents.Add(crystalMoss);

        SingleHerb sweetRotCap = new SingleHerb();
            sweetRotCap.UpdateName("sweetRotCap");
            sweetRotCap.UpdateDetails("A short but wide purple fungi with light blue specks on its caps.", "Treatment Rules:\n1. Only safe for large animals to consume");
            sweetRotCap.SetEffects(true, true, false);
            sweetRotCap.SetTargets(false, false, true);
            sweetRotCap.SetModifiers(true, false);
                herbDrawerContents.Add(sweetRotCap);

        SingleHerb hexacore = new SingleHerb();
            hexacore.UpdateName("hexacore");
            hexacore.UpdateDetails("Named after the distinct hexagonal shape of their seed pods, the Hexacore is a primarily vibrant yellow plant with long stems.", "Treatment Rules:\n1. Only works in a treatment if the creature is a bird\n2. Only works when Meltingroot is also present in the recipe");
            hexacore.SetEffects(true, false, false);
            hexacore.SetTargets(false, false, false);
            hexacore.SetModifiers(false, false);
                herbDrawerContents.Add(hexacore);

        SingleHerb crystalVine = new SingleHerb();
            crystalVine.UpdateName("crystalVine");
            crystalVine.UpdateDetails("A green herb with white growths. These growths are neither fruit nor flower.", "Treatment Rules:\n1. Having a secondary effect will nullify the treatment");
            crystalVine.SetEffects(false, false, true);
            crystalVine.SetTargets(false, true, false);
            crystalVine.SetModifiers(false, false);
                herbDrawerContents.Add(crystalVine);

        SingleHerb watchersWeed = new SingleHerb();
            watchersWeed.UpdateName("watchersWeed");
            watchersWeed.UpdateDetails("Most recognisable for its 'eye-like' white growths.", "Treatment Rules:\n1. Only enhances when a recipe targets the mind");
            watchersWeed.SetEffects(false, false, false);
            watchersWeed.SetTargets(false, false, true);
            watchersWeed.SetModifiers(true, false);
                herbDrawerContents.Add(watchersWeed);

        SingleHerb pupilPetal = new SingleHerb();
            pupilPetal.UpdateName("pupilPetal");
            pupilPetal.UpdateDetails("Its red flowers stand on long spindly stems.", "Treatment Rules:\n1. Can only target the mind\n2. Won't work without an enhancer");
            pupilPetal.SetEffects(false, true, true);
            pupilPetal.SetTargets(false, false, false);
            pupilPetal.SetModifiers(false, false);
                herbDrawerContents.Add(pupilPetal);

        SingleHerb queensReed = new SingleHerb();
            queensReed.UpdateName("queensReed");
            queensReed.UpdateDetails("A green and purple herb, bearing no flowers or fruit.", "Treatment Rules:\n1. Can only impact the treatment's primary effect or enhancer\n2. Only enhances when targeting the body");
            queensReed.SetEffects(false, false, false);
            queensReed.SetTargets(false, true, true);
            queensReed.SetModifiers(true, false);
                herbDrawerContents.Add(queensReed);

        SingleHerb bumbleBlooms = new SingleHerb();
            bumbleBlooms.UpdateName("bumbleBlooms");
            bumbleBlooms.UpdateDetails("Small yellow fruit with long green stems.", "Treatment Rules:\n1. Only works when effect is 'ease'\n2. needs two effects, won't work with an enhancer");
            bumbleBlooms.SetEffects(false, false, false);
            bumbleBlooms.SetTargets(true, false, false);
            bumbleBlooms.SetModifiers(false, false);
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
            drawerSensorScripts[i].FillDrawer(herbDrawerContents[i].herbName);
        }

        // FOR DEBUGGING
        int randomDraw = Random.Range(0, herbCount); //+1
        Debug.Log("The herb in slot " + randomDraw + " is: " + herbDrawerContents[randomDraw].herbName + ". The drawerSensor for the same slot has updated its contents as: " + drawerSensorScripts[randomDraw].drawerContents + ".");
    }

    public void UpdateAndShowNote(string herbNameToSearch)
    {
        foreach (SingleHerb s in herbDrawerContents)
        {
            if (s.herbName == herbNameToSearch)
            {
                herbalistNotesOnHover.ReceiveInformation(s.herbName, s.herbDescription, s.herbExtras);
                //Debug.Log("Information sent to the note display script.");
            }
        }
        //SingleHerb locate = herbDrawerContents(herbNameToSearch);
        /*if (locate != null)
        {
            herbalistNotesOnHover.ReceiveInformation(locate.herbName, locate.herbDescription, locate.herbExtras);
            Debug.Log("Information sent to the note display script.");
        }*/
    }

    public void HideNote()
    {
        //Debug.Log("Sent")
        UIManager.Instance.DisableUI(herbalistNotesOnHover.herbalistNoteCanvasGroup);
    }

    /*public void UpdateNoteLocation(GameObject activeHerb)
    {
        Vector2 
    }
    public void SetNoteLocation(Vector2 initialPosition, )
    {
        //RectTransform rectTransform = herbalistNoteCanvasGroup.GetComponent<RectTransform>();
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
