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

        public bool _canFortify {get; private set;}
        public bool _canHeal {get; private set;}
        public bool _canEase {get; private set;}

        public bool _targetsMind {get; private set;}
        public bool _targetsBody {get; private set;}
        public bool _targetsSpirit {get; private set;}

		public bool _forSmallPatients {get; private set;}
		public bool _forMediumPatients {get; private set;}
		public bool _forLargePatients {get; private set;}
        
            public List<bool> _sizes {get; private set;}

		public bool _forCarnivores {get; private set;}
		public bool _forOmnivores {get; private set;}
        public bool _forHerbivores {get; private set;}
		
        
            public List<bool> _diets {get; private set;}

        public bool _isEnhancable {get; private set;}
        public bool _isInvertable {get; private set;}

        public void SetSingleHerbName(string newName)
        {
            _herbName = newName;
        }

        public void SetSingleHerbDescription(string description) //(string description, string extras)
        {
            _herbDescription = description;
            // _herbExtras = extras;
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
		
		public void SetSingleHerbSizes(bool small, bool medium, bool large)
		{
			_forSmallPatients = small; 
			_forMediumPatients = medium;
			_forLargePatients = large;
            
            _sizes = new List<bool>();
            _sizes.Add(small);
            _sizes.Add(medium);
            _sizes.Add(large);
		}

		public void SetSingleHerbDiets(bool carnivore, bool herbivore, bool omnivore)
        {
         	_forCarnivores = carnivore;
			_forOmnivores = omnivore;
            _forHerbivores = herbivore;

            _diets = new List<bool>();
            _diets.Add(carnivore);
            _diets.Add(omnivore);
            _diets.Add(herbivore);
            
        }

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

    // [SerializeField] private Color red;
    // #FF0000
    // [SerializeField] private Color green;
    // #076600
    // [SerializeField] private Color blue;
    // #002DFF
    
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
		bool y = true;
		bool n = false;
        
        SingleHerb morningMint = new SingleHerb();
        morningMint.SetSingleHerbName("morningMint");
        morningMint.SetSingleHerbDescription("UPDATE ME!! <#076600>greem</color> <i>mind</i>");
        morningMint.SetSingleHerbEffects(n, y, n);
        morningMint.SetSingleHerbTargets(y, n, n);
        morningMint.SetSingleHerbSizes(n, n, y);        // large
        morningMint.SetSingleHerbDiets(y, y, y);        // all 3
        morningMint.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(morningMint);
        
        SingleHerb heavensHollyhock = new SingleHerb();
        heavensHollyhock.SetSingleHerbName("heavensHollyhock");
        heavensHollyhock.SetSingleHerbDescription("UPDATE ME!! <i>mind</i>");
        heavensHollyhock.SetSingleHerbEffects(n, n, n);
        heavensHollyhock.SetSingleHerbTargets(y, n, y);
        heavensHollyhock.SetSingleHerbSizes(y, n, n);   // small
        heavensHollyhock.SetSingleHerbDiets(y, y, y);   // all 3
        heavensHollyhock.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(heavensHollyhock);
        
        SingleHerb crystalVine = new SingleHerb();
        crystalVine.SetSingleHerbName("crystalVine");
        crystalVine.SetSingleHerbDescription("UPDATE ME!! <#076600>green</color> <#002DFF>blue</color> <i>mind</i>");
        crystalVine.SetSingleHerbEffects(n, y, y);
        crystalVine.SetSingleHerbTargets(y, n, n);
        crystalVine.SetSingleHerbSizes(n, y, n);        // med
        crystalVine.SetSingleHerbDiets(n, y, n);        // omnivore
        crystalVine.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(crystalVine);
        
        SingleHerb lavendear = new SingleHerb();
        lavendear.SetSingleHerbName("lavendear");
        lavendear.SetSingleHerbDescription("UPDATE ME!! <#002DFF>blue</color> <b>body</b>");
        lavendear.SetSingleHerbEffects(n, n, y);
        lavendear.SetSingleHerbTargets(n, y, n);
        lavendear.SetSingleHerbSizes(y, y, y);          // all 3
        lavendear.SetSingleHerbDiets(n, y, n);          // omnivore
        lavendear.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(lavendear);
        
        
        
        SingleHerb wingroot = new SingleHerb();
        wingroot.SetSingleHerbName("wingroot");
        wingroot.SetSingleHerbDescription("UPDATE ME!! <i>mind</i>");
        wingroot.SetSingleHerbEffects(n, n, n);
        wingroot.SetSingleHerbTargets(y, n, n);
        wingroot.SetSingleHerbSizes(n, y, n);           // med
        wingroot.SetSingleHerbDiets(n, y, n);           // omnivore
        wingroot.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(wingroot);
        
        SingleHerb spiceLeaf = new SingleHerb();
        spiceLeaf.SetSingleHerbName("spiceLeaf");
        spiceLeaf.SetSingleHerbDescription("UPDATE ME!! <#FF0000>red</color> <b>body</b>");
        spiceLeaf.SetSingleHerbEffects(y, n, n);
        spiceLeaf.SetSingleHerbTargets(n, y, n);
        spiceLeaf.SetSingleHerbSizes(y, n, n);          // small
        spiceLeaf.SetSingleHerbDiets(n, y, n);          // omnivore
        spiceLeaf.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(spiceLeaf);
        
        SingleHerb pupilPetal = new SingleHerb();
        pupilPetal.SetSingleHerbName("pupilPetal");
        pupilPetal.SetSingleHerbDescription("UPDATE ME!! <#FF0000>red</color>");
        pupilPetal.SetSingleHerbEffects(y, n, n);
        pupilPetal.SetSingleHerbTargets(n, n, n);
        pupilPetal.SetSingleHerbSizes(n, y, y);         // med, lar
        pupilPetal.SetSingleHerbDiets(n, y, y);         // omnivore, herbivore
        pupilPetal.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(pupilPetal);
        
        SingleHerb bellBloom = new SingleHerb();
        bellBloom.SetSingleHerbName("bellBloom");
        bellBloom.SetSingleHerbDescription("UPDATE ME!! <b>body</b>");
        bellBloom.SetSingleHerbEffects(n, n, n);
        bellBloom.SetSingleHerbTargets(n, y, n);
        bellBloom.SetSingleHerbSizes(y, n, n);          // small
        bellBloom.SetSingleHerbDiets(y, y, y);          // all 3
        bellBloom.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(bellBloom);


        SingleHerb featherFern = new SingleHerb();
        featherFern.SetSingleHerbName("featherFern");
        featherFern.SetSingleHerbDescription("UPDATE ME!! <#002DFF>blue</color>");
        featherFern.SetSingleHerbEffects(n, n, y);
        featherFern.SetSingleHerbTargets(n, n, n);
        featherFern.SetSingleHerbSizes(y, y, y);        // all 3
        featherFern.SetSingleHerbDiets(n, n, y);        // herbivore
        featherFern.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(featherFern);
        
        SingleHerb bumbleBlooms = new SingleHerb();
        bumbleBlooms.SetSingleHerbName("bumbleBlooms");
        bumbleBlooms.SetSingleHerbDescription("UPDATE ME!! <#076600>green</color>");
        bumbleBlooms.SetSingleHerbEffects(n, y, n);
        bumbleBlooms.SetSingleHerbTargets(n, n, n);
        bumbleBlooms.SetSingleHerbSizes(y, n, n);       // small
        bumbleBlooms.SetSingleHerbDiets(y, y, y);       // all 3
        bumbleBlooms.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(bumbleBlooms);
        
        SingleHerb sweetRotCap = new SingleHerb();
        sweetRotCap.SetSingleHerbName("sweetRotCap");
        sweetRotCap.SetSingleHerbDescription("UPDATE ME!! <#002DFF>blue</color> <b>body</b>");
        sweetRotCap.SetSingleHerbEffects(n, n, y);
        sweetRotCap.SetSingleHerbTargets(n, y, n);
        sweetRotCap.SetSingleHerbSizes(n, y, n);        // medium
        sweetRotCap.SetSingleHerbDiets(n, y, y);        // ommnivore, herbivore
        sweetRotCap.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(sweetRotCap);
        
        SingleHerb watchersWeed = new SingleHerb();
        watchersWeed.SetSingleHerbName("watchersWeed");
        watchersWeed.SetSingleHerbDescription("UPDATE ME!! <#FF0000>red</color>");
        watchersWeed.SetSingleHerbEffects(y, n, n);
        watchersWeed.SetSingleHerbTargets(n, n, n);
        watchersWeed.SetSingleHerbSizes(y, n, y);       // small, large
        watchersWeed.SetSingleHerbDiets(y, n, n);       // carnivore
        watchersWeed.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(watchersWeed);
        
        /*SingleHerb meltingRoot = new SingleHerb();
            meltingRoot.SetSingleHerbName("meltingRoot");
            meltingRoot.SetSingleHerbDescription("Growing close to the ground with its roots partially exposed, Meltingroot is a pale herb with no flowers or fruit.", "Treatment Rules:\n1. Does not work alongside Sweet Rot Cap\n2. Recipe needs a secondary effect target");
            meltingRoot.SetSingleHerbEffects(n, n, n);
            meltingRoot.SetSingleHerbTargets(n, y, n);
				meltingRoot.SetSingleHerbSizes(y, n, n);
				meltingRoot.SetSingleHerbDiets(y, y, y);
            meltingRoot.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(meltingRoot);

        SingleHerb warmWhisper = new SingleHerb();
            warmWhisper.SetSingleHerbName("warmWhisper");
            warmWhisper.SetSingleHerbDescription("A luscious red herb, standing tall with its long stems.", "Treatment Rules:\n1. Recipe needs both a primary and secondary effect\n2. If using this herb to ease while a heal effect is also present in the recipe, the treatment will be nullified. However, the treatment is fine if used to heal while an ease effect is present");
            warmWhisper.SetSingleHerbEffects(y, n, n);
            warmWhisper.SetSingleHerbTargets(n, n, n);
				warmWhisper.SetSingleHerbSizes(n, n, y);
				warmWhisper.SetSingleHerbDiets(n, n, y);
            warmWhisper.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(warmWhisper);

        SingleHerb crystalMoss = new SingleHerb();
            crystalMoss.SetSingleHerbName("crystalMoss");
            crystalMoss.SetSingleHerbDescription("Attached to bark, Crystal Moss is a green herb with white growths. These growths are neither fruit nor flower.", "Treatment Rules:\n1. Only works as a secondary effect or as an enhancer\n2. Won't enhance without a secondary effect");
            crystalMoss.SetSingleHerbEffects(n, y, n);
            crystalMoss.SetSingleHerbTargets(n, n, n);
				crystalMoss.SetSingleHerbSizes(n, n, y);
				crystalMoss.SetSingleHerbDiets(y, n, y);
            crystalMoss.SetSingleHerbModifiers(y, n);
                herbDrawerContents.Add(crystalMoss);

        SingleHerb hexacore = new SingleHerb();
            hexacore.SetSingleHerbName("hexacore");
            hexacore.SetSingleHerbDescription("Named after the distinct hexagonal shape of their seed pods, the Hexacore is a primarily vibrant yellow plant with long stems.", "Treatment Rules:\n1. Only works in a treatment if the creature is a bird\n2. Only works when Meltingroot is also present in the recipe");
            hexacore.SetSingleHerbEffects(n, n, n);
            hexacore.SetSingleHerbTargets(n, y, n);
				hexacore.SetSingleHerbSizes(n, n, y);
				hexacore.SetSingleHerbDiets(y, n, y);
            hexacore.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(hexacore);

        SingleHerb queensReed = new SingleHerb();
            queensReed.SetSingleHerbName("queensReed");
            queensReed.SetSingleHerbDescription("A green and purple herb, bearing no flowers or fruit.", "Treatment Rules:\n1. Can only impact the treatment's primary effect or enhancer\n2. Only enhances when targeting the body");
            queensReed.SetSingleHerbEffects(n, n, y);
            queensReed.SetSingleHerbTargets(n, n, n);
				queensReed.SetSingleHerbSizes(y, n, y);
				queensReed.SetSingleHerbDiets(n, y, n);
            queensReed.SetSingleHerbModifiers(y, n);
                herbDrawerContents.Add(queensReed);

		SingleHerb moonlightFern = new SingleHerb();
            moonlightFern.SetSingleHerbName("moonlightFern");
            moonlightFern.SetSingleHerbDescription("UPDATE ME!!");
            moonlightFern.SetSingleHerbEffects(n, y, n);
            moonlightFern.SetSingleHerbTargets(y, n, n);
				moonlightFern.SetSingleHerbSizes(y, y, y);
				moonlightFern.SetSingleHerbDiets(n, n, y);
            moonlightFern.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(moonlightFern);

		SingleHerb bloodBloom = new SingleHerb();
            bloodBloom.SetSingleHerbName("bloodBloom");
            bloodBloom.SetSingleHerbDescription("UPDATE ME!!");
            bloodBloom.SetSingleHerbEffects(y, n, n);
            bloodBloom.SetSingleHerbTargets(n, n, n);
				bloodBloom.SetSingleHerbSizes(y, n, n);
				bloodBloom.SetSingleHerbDiets(y, n, y);
            bloodBloom.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(bloodBloom);

		SingleHerb gildedClover = new SingleHerb();
            gildedClover.SetSingleHerbName("gildedClover");
            gildedClover.SetSingleHerbDescription("UPDATE ME!!");
            gildedClover.SetSingleHerbEffects(n, n, n);
            gildedClover.SetSingleHerbTargets(y, n, n);
				gildedClover.SetSingleHerbSizes(y, n, n);
				gildedClover.SetSingleHerbDiets(n, n, y);
            gildedClover.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(gildedClover);
                */
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

            if (temporaryObject.TryGetComponent<DrawerSensor>(out temporaryReference))
            {
                drawerSensorScripts.Add(temporaryReference);
            }
        }

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
                botanicalCodexOnHoverReference.PrototypeReceiveInformation(s);
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
