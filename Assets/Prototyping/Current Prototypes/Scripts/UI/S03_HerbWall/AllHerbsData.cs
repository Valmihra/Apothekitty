using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHerbsData : MonoBehaviour
{
    public class SingleHerb
    {
        public string _herbName;
        public string _herbDescription;
        
        // Treatment Categories - Effects
        public bool _canFortify {get; private set;}
        public bool _canHeal {get; private set;}
        public bool _canEase {get; private set;}
        
        // Treatment Categories - Targets
        public bool _targetsMind {get; private set;}
        public bool _targetsBody {get; private set;}
        public bool _targetsSpirit {get; private set;}

        // Sizes
		public bool _forSmallPatients {get; private set;}
		public bool _forMediumPatients {get; private set;}
		public bool _forLargePatients {get; private set;}
            public List<bool> _sizes {get; private set;}

        // Diets
		public bool _forCarnivores {get; private set;}
		public bool _forOmnivores {get; private set;}
        public bool _forHerbivores {get; private set;}
            public List<bool> _diets {get; private set;}
        
        // Modifiers (old)
        // public bool _isEnhancable {get; private set;}
        // public bool _isInvertable {get; private set;}

        public void SetSingleHerbName(string newName)
        {
            _herbName = newName;
        }

        public void SetSingleHerbDescription(string description)
        {
            _herbDescription = description;
        }
		
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

        /*public void SetSingleHerbModifiers(bool enhance, bool invert)
        {
            _isEnhancable = enhance;
            _isInvertable = invert;
        }*/
    }

    // Colour Hexadecimal Codes
    // #FF0000 - Red
    // #076600 - Green
    // #002DFF - Blue
    
    private int herbCount = 12;
    public List<SingleHerb> herbDrawerContents;
    [SerializeField] private BotanicalCodexDisplay botanicalCodexOnHoverReference;  // was public

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
        botanicalCodexOnHoverReference = GetComponent<BotanicalCodexDisplay>();
        
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
        morningMint.SetSingleHerbDescription("Morning Mint is a <i>gentle</i> <#076600>green</color> herb which smells like a mix of peppermint and morning sunshine.");
        morningMint.SetSingleHerbEffects(n, y, n);
        morningMint.SetSingleHerbTargets(y, n, n);
        morningMint.SetSingleHerbSizes(n, n, y);        // large
        morningMint.SetSingleHerbDiets(y, y, y);        // all 3
        // morningMint.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(morningMint);
        
        SingleHerb heavensHollyhock = new SingleHerb();
        heavensHollyhock.SetSingleHerbName("heavensHollyhock");
        heavensHollyhock.SetSingleHerbDescription("Heaven's Hollyhock is a <i>delicate</i> cluster of white flowers that tightly overlap each other.");
        heavensHollyhock.SetSingleHerbEffects(n, n, n);
        heavensHollyhock.SetSingleHerbTargets(y, n, y);
        heavensHollyhock.SetSingleHerbSizes(y, n, n);   // small
        heavensHollyhock.SetSingleHerbDiets(y, y, y);   // all 3
        // heavensHollyhock.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(heavensHollyhock);
        
        SingleHerb crystalVine = new SingleHerb();
        crystalVine.SetSingleHerbName("crystalVine");
        crystalVine.SetSingleHerbDescription("Crystal Vine is a <i>fragile</i> <#076600>green</color> herb with <#002DFF>blue</color> crystal growths, their stems coil onto anything nearby.");
        crystalVine.SetSingleHerbEffects(n, y, y);
        crystalVine.SetSingleHerbTargets(y, n, n);
        crystalVine.SetSingleHerbSizes(n, y, n);        // med
        crystalVine.SetSingleHerbDiets(n, y, n);        // omnivore
        // crystalVine.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(crystalVine);
        
        SingleHerb lavendear = new SingleHerb();
        lavendear.SetSingleHerbName("lavendear");
        lavendear.SetSingleHerbDescription("Lavendear is a <#002DFF>blue</color> herb with a <b>stubby, bloated</b> bud. Special-made materials are needed to produce cuttings of this herb.");
        lavendear.SetSingleHerbEffects(n, n, y);
        lavendear.SetSingleHerbTargets(n, y, n);
        lavendear.SetSingleHerbSizes(y, y, y);          // all 3
        lavendear.SetSingleHerbDiets(n, y, n);          // omnivore
        // lavendear.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(lavendear);
        
        
        
        SingleHerb wingroot = new SingleHerb();
        wingroot.SetSingleHerbName("wingroot");
        wingroot.SetSingleHerbDescription("Wingroot is a <i>soft</i> yellow herb commonly used to soften the inside of bedding.");
        wingroot.SetSingleHerbEffects(n, n, n);
        wingroot.SetSingleHerbTargets(y, n, n);
        wingroot.SetSingleHerbSizes(n, y, n);           // med
        wingroot.SetSingleHerbDiets(n, y, n);           // omnivore
        // wingroot.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(wingroot);
        
        SingleHerb spiceLeaf = new SingleHerb();
        spiceLeaf.SetSingleHerbName("spiceLeaf");
        spiceLeaf.SetSingleHerbDescription("SpiceLeaf is a <b>robust</b> collection of <#FF0000>red</color> fruit with speckled yellow spots. These fruits are considered quite spicy and tough to chew for the average consumer.");
        spiceLeaf.SetSingleHerbEffects(y, n, n);
        spiceLeaf.SetSingleHerbTargets(n, y, n);
        spiceLeaf.SetSingleHerbSizes(y, n, n);          // small
        spiceLeaf.SetSingleHerbDiets(n, y, n);          // omnivore
        // spiceLeaf.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(spiceLeaf);
        
        SingleHerb pupilPetal = new SingleHerb();
        pupilPetal.SetSingleHerbName("pupilPetal");
        pupilPetal.SetSingleHerbDescription("Pupil petal is a <#FF0000>red</color> flower with long spindly stems.");
        pupilPetal.SetSingleHerbEffects(y, n, n);
        pupilPetal.SetSingleHerbTargets(n, n, n);
        pupilPetal.SetSingleHerbSizes(n, y, y);         // med, lar
        pupilPetal.SetSingleHerbDiets(n, y, y);         // omnivore, herbivore
        // pupilPetal.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(pupilPetal);
        
        SingleHerb bellBloom = new SingleHerb();
        bellBloom.SetSingleHerbName("bellBloom");
        bellBloom.SetSingleHerbDescription("Bell Bloom is a yellow herb with a <b>hard and calloused</b> bell-shaped bud.");
        bellBloom.SetSingleHerbEffects(n, n, n);
        bellBloom.SetSingleHerbTargets(n, y, n);
        bellBloom.SetSingleHerbSizes(y, n, n);          // small
        bellBloom.SetSingleHerbDiets(y, y, y);          // all 3
        // bellBloom.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(bellBloom);


        SingleHerb featherFern = new SingleHerb();
        featherFern.SetSingleHerbName("featherFern");
        featherFern.SetSingleHerbDescription("Feather Fern is a <#002DFF>blue</color> herb. It grows in areas where only birds and other flying folk can easily reach it.");
        featherFern.SetSingleHerbEffects(n, n, y);
        featherFern.SetSingleHerbTargets(n, n, n);
        featherFern.SetSingleHerbSizes(y, y, y);        // all 3
        featherFern.SetSingleHerbDiets(n, n, y);        // herbivore
        // featherFern.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(featherFern);
        
        SingleHerb bumbleBlooms = new SingleHerb();
        bumbleBlooms.SetSingleHerbName("bumbleBlooms");
        bumbleBlooms.SetSingleHerbDescription("Bumble Blooms is a collection of small yellow fruit with <#076600>green</color> stems and bulbous blooms."); // not body, only heal
        bumbleBlooms.SetSingleHerbEffects(n, y, n);
        bumbleBlooms.SetSingleHerbTargets(n, n, n);
        bumbleBlooms.SetSingleHerbSizes(y, n, n);       // small
        bumbleBlooms.SetSingleHerbDiets(y, y, y);       // all 3
        // bumbleBlooms.SetSingleHerbModifiers(n, n);
        herbDrawerContents.Add(bumbleBlooms);
        
        SingleHerb sweetRotCap = new SingleHerb();
        sweetRotCap.SetSingleHerbName("sweetRotCap");
        sweetRotCap.SetSingleHerbDescription("Sweet-Rot Cap is a short but wide fungi with <#002DFF>blue</color> specks on its <b>sturdy</b> caps.");
        sweetRotCap.SetSingleHerbEffects(n, n, y);
        sweetRotCap.SetSingleHerbTargets(n, y, n);
        sweetRotCap.SetSingleHerbSizes(n, y, n);        // medium
        sweetRotCap.SetSingleHerbDiets(n, y, y);        // ommnivore, herbivore
        // sweetRotCap.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(sweetRotCap);
        
        SingleHerb watchersWeed = new SingleHerb();
        watchersWeed.SetSingleHerbName("watchersWeed");
        watchersWeed.SetSingleHerbDescription("Watcher's Weed is most recognisable for its eye-like appearance. This herb is noted to be primarily white and <#FF0000>red</color>.");
        watchersWeed.SetSingleHerbEffects(y, n, n);
        watchersWeed.SetSingleHerbTargets(n, n, n);
        watchersWeed.SetSingleHerbSizes(y, n, y);       // small, large
        watchersWeed.SetSingleHerbDiets(y, n, n);       // carnivore
        // watchersWeed.SetSingleHerbModifiers(y, n);
        herbDrawerContents.Add(watchersWeed);
        
        /*SingleHerb meltingRoot = new SingleHerb();
            meltingRoot.SetSingleHerbName("meltingRoot");
            meltingRoot.SetSingleHerbDescription("Meltingroot is a pale herb that resembles tree roots and is <b>difficult to break</b>.);
            meltingRoot.SetSingleHerbEffects(n, n, n);
            meltingRoot.SetSingleHerbTargets(n, y, n);
				meltingRoot.SetSingleHerbSizes(y, n, n);
				meltingRoot.SetSingleHerbDiets(y, y, y);
            // meltingRoot.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(meltingRoot);

        SingleHerb warmWhisper = new SingleHerb();
            warmWhisper.SetSingleHerbName("warmWhisper");
            warmWhisper.SetSingleHerbDescription("Warm Whisper is a luscious <#FF0000>red</color> herb, standing tall with its long stems. Often used as a garnish.");
            warmWhisper.SetSingleHerbEffects(y, n, n);
            warmWhisper.SetSingleHerbTargets(n, n, n);
				warmWhisper.SetSingleHerbSizes(n, n, y);
				warmWhisper.SetSingleHerbDiets(n, n, y);
            // warmWhisper.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(warmWhisper);

        SingleHerb crystalMoss = new SingleHerb();
            crystalMoss.SetSingleHerbName("crystalMoss");
            crystalMoss.SetSingleHerbDescription("Crystal Moss is a <#076600>green</color> herb with white crystal-like growths. Unlike most herbs, it grows best when attached to tree bark.")
            crystalMoss.SetSingleHerbEffects(n, y, n);
            crystalMoss.SetSingleHerbTargets(n, n, n);
				crystalMoss.SetSingleHerbSizes(n, n, y);
				crystalMoss.SetSingleHerbDiets(y, n, y);
            // crystalMoss.SetSingleHerbModifiers(y, n);
                herbDrawerContents.Add(crystalMoss);

        SingleHerb hexacore = new SingleHerb();
            hexacore.SetSingleHerbName("hexacore");
            hexacore.SetSingleHerbDescription("Hexacore is a vibrant yellow <b>stocky</b> herb with distinctly hexagonal-shaped seed pods.")
            hexacore.SetSingleHerbEffects(n, n, n);
            hexacore.SetSingleHerbTargets(n, y, n);
				hexacore.SetSingleHerbSizes(n, n, y);
				hexacore.SetSingleHerbDiets(y, n, y);
            // hexacore.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(hexacore);

        SingleHerb queensReed = new SingleHerb();
            queensReed.SetSingleHerbName("queensReed");
            queensReed.SetSingleHerbDescription("Queen's Reed is a <#002DFF>blue</color> herb once only accessible to the queen due to the price. ")
            queensReed.SetSingleHerbEffects(n, n, y);
            queensReed.SetSingleHerbTargets(n, n, n);
				queensReed.SetSingleHerbSizes(y, n, y);
				queensReed.SetSingleHerbDiets(n, y, n);
            // queensReed.SetSingleHerbModifiers(y, n);
                herbDrawerContents.Add(queensReed);

		SingleHerb moonlightFern = new SingleHerb();
            moonlightFern.SetSingleHerbName("moonlightFern");
            moonlightFern.SetSingleHerbDescription("Moonlight Fern is an <i>elegant</i> <#076600>green</color> herb with long stems that shine bright under the moon's glow.");
            moonlightFern.SetSingleHerbEffects(n, y, n);
            moonlightFern.SetSingleHerbTargets(y, n, n);
				moonlightFern.SetSingleHerbSizes(y, y, y);
				moonlightFern.SetSingleHerbDiets(n, n, y);
            // moonlightFern.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(moonlightFern);

		SingleHerb bloodBloom = new SingleHerb();
            bloodBloom.SetSingleHerbName("bloodBloom");
            bloodBloom.SetSingleHerbDescription("Blood Bloom is a <#FF0000>red</color> herb recognised for its uniquely flesh-like epidermic layer.");
            bloodBloom.SetSingleHerbEffects(y, n, n);
            bloodBloom.SetSingleHerbTargets(n, n, n);
				bloodBloom.SetSingleHerbSizes(y, n, n);
				bloodBloom.SetSingleHerbDiets(y, n, y);
            // bloodBloom.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(bloodBloom);

		SingleHerb gildedClover = new SingleHerb();
            gildedClover.SetSingleHerbName("gildedClover");
            gildedClover.SetSingleHerbDescription("Gilded Clover is a <i>fragile</i> herb which grows in a collection of many other unified clovers. ");
            gildedClover.SetSingleHerbEffects(n, n, n);
            gildedClover.SetSingleHerbTargets(y, n, n);
				gildedClover.SetSingleHerbSizes(y, n, n);
				gildedClover.SetSingleHerbDiets(n, n, y);
            // gildedClover.SetSingleHerbModifiers(n, n);
                herbDrawerContents.Add(gildedClover);
                */
    }

    void AssignHerbsToDrawerScripts()
    {
        string prefix = "Drawer ";
        // string searchName;

        List<DrawerSensor> drawerSensorScripts = new List<DrawerSensor>();
        // DrawerSensor temporaryReference;
        // GameObject temporaryObject;
        for (int i = 1; i < herbCount + 1; i++)
        {
            string searchName = prefix + i.ToString();
            GameObject temporaryObject = GameObject.Find(searchName);

            if (temporaryObject.TryGetComponent<DrawerSensor>(out DrawerSensor temporaryReference))
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
                botanicalCodexOnHoverReference.CodexReceiveInformation(s);
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
