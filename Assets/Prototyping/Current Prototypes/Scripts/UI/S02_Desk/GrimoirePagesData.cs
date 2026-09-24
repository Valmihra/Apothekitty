using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrimoirePagesData : MonoBehaviour
{
    public class SinglePage
    {
        // public int/icon? which to use? int??
        public string _ailmentName;
        public string _ailmentDescription;

        public void UpdateName(string newName)
        {
            _ailmentName = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            _ailmentDescription = newDescription;
        }
    }

    private List<SinglePage> grimoirePagesList;
    public SinglePage[] grimoirePagesArray;

    int totalPages = 0;

    private GrimoireNavigation grimoireNavigation;
    private Stamp stamp;
    private Rect grimoireRect;

    private static GrimoirePagesData _instance;
    public static GrimoirePagesData Instance
    {
        get
        {
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
        grimoireNavigation = GetComponent<GrimoireNavigation>();
    }
    
        // For Stamp
        public Rect GetAndReturnGrimoireRect()
        {
            // Currently using the whole page, but could set to ailment icon specifically if that's preferred!!
            grimoireRect.center = transform.position;
            return grimoireRect;
        }

        public bool SelectedPageIsNotTheCover()
        {
            /*if (grimoireNavigation.currentGrimoirePageNumber > 0)
            {
                return true;
            }
            else
            {
                return false;
            }*/
            
            bool boolToReturn = grimoireNavigation.currentGrimoirePageNumber > 0 ? true : false;
            return boolToReturn;
        }
        
        public void TempSelectionCheck()
        {
            grimoireNavigation.TempSelectionCheck();
        }

        private void GetGrimoireRect()
        {
            // Initialises the rect for Stamp
            Vector3[] corners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(corners);

            Vector2 min = corners[0];
            Vector2 max = corners[2];
            Vector2 size = max - min;

            grimoireRect = new Rect(min, size);
        }

    public void InitialiseGrimoirePagesData()
    {
        InitialiseGrimoirePagesList();
        SetPageData();
        SetGrimoirePagesArray();
        
        grimoireNavigation.InitialiseGrimoireNavigation();
        
        stamp = FindObjectOfType<Stamp>();
        stamp.InitialiseStamp();
        GetGrimoireRect();
        
    }
    
    // *TAG* - Should probably just use list instead of array lmao, why do i need both??
    void InitialiseGrimoirePagesList()
    {
        grimoirePagesList = new List<SinglePage>();
    }

    void SetGrimoirePagesArray()
    {
        grimoirePagesArray = grimoirePagesList.ToArray();
    }

    void SetPageData()  // alphabetically set up for now
    {
                SinglePage cover = new SinglePage();
                cover.UpdateName("cover");
                cover.UpdateDescription("cover");
                    grimoirePagesList.Add(cover);

        SinglePage chronicInsomnia = new SinglePage();
        chronicInsomnia.UpdateName("Chronic Insomnia");
        chronicInsomnia.UpdateDescription("Clients present with significant disturbances with their body's internal clock and circadian rhythm. These sleep complications are often shown to result in lower quality and quantity of sleep. \n\nDisturbances and symptoms have occurred longer than three months.");   //\n\nThis ");
            grimoirePagesList.Add(chronicInsomnia);

        SinglePage contaminationOCD = new SinglePage();
        contaminationOCD.UpdateName("Contamination-Specific OCD");
        contaminationOCD.UpdateDescription("Clients present with extreme neuroticism of germs, diseases, and contaminants. This fear results in ritualistic and repetitive self-implemented systems to help sooth and regulate stress responses.");
            grimoirePagesList.Add(contaminationOCD);

        SinglePage dietDrift = new SinglePage();
        dietDrift.UpdateName("Diet Drift");     // should prob actually be fortify mind!!
        dietDrift.UpdateDescription("An ailment in which the client develops an insatiable hunger for food unnatural to their species. For example, a herbivore craving meat.\n\nThis condition is typically triggered by a traumatic experience involving the consumption of that forbidden diet. In some cases, it may progress into cannibalism if the trauma involved consuming a member of the same species. Treatment should focus on stopping corrupted thoughts and clearing the body of any impurities. ");
            grimoirePagesList.Add(dietDrift);

        SinglePage honEye = new SinglePage();
        honEye.UpdateName("Hon-Eye Infection");
        honEye.UpdateDescription("An eye infection caused by eating bacteria-infested honey. By the time its amber-like crystals have begun to line the lower eyelid, the case is extreme and may need a strong dosage. Treatment should primarily focus on healing the affected eye(s). If left untreated, the eye(s) will permanently shut, and the client will lose their vision.");
            grimoirePagesList.Add(honEye);

        SinglePage illnessAnxiety = new SinglePage();
        illnessAnxiety.UpdateName("Illness Anxiety");
        illnessAnxiety.UpdateDescription("Clients present with an irrational fear and are convinced of having a serious health condition, despite being healthy.");
            grimoirePagesList.Add(illnessAnxiety);

        SinglePage orthorexia = new SinglePage();
        orthorexia.UpdateName("Orthorexia");
        orthorexia.UpdateDescription("Clients present fear of unhealthy food consumption, often believing food should only be eaten if it is clean, healthy, and pure, according to their understandings and standards. This often leads to malnutrition and disordered eating.");
            grimoirePagesList.Add(orthorexia);

        SinglePage sapEye = new SinglePage();
        sapEye.UpdateName("Sap-Eye Infection");
        sapEye.UpdateDescription("An eye infection caused by eating bacteria-infested tree sap. Takes the appearance of orange goo lining the lower eyelid. Treatment should primarily focus on healing the affected eye(s). If left untreated, the eye(s) will permanently shut, and the client will lose their vision.");
            grimoirePagesList.Add(sapEye);

        SinglePage theBlues = new SinglePage();
        theBlues.UpdateName("The Blues");
        theBlues.UpdateDescription("A mental and physical ailment triggered by a sudden and intense source of sadness. The client's skin begins to turn blue and melt away, and they become paralysed due to the heavy weight on their mind. Treatment should focus on calming the mind and healing the skin. If left untreated, the client will eventually melt into a puddle of tears. ");
            grimoirePagesList.Add(theBlues);

        SinglePage theFanging = new SinglePage();
        theFanging.UpdateName("The Fanging");
        theFanging.UpdateDescription("An ailment transmitted through the bite of an infected Nocturnal Mosquito, causing the client to transform into a bat-like creature.\n\nClients may develop bat-like sensory processing, wings, sharpened teeth, and nocturnal instincts. If left untreated, the client will fully transform into the bat-like creature. Treatment should focus on healing physical transformation and reinforcing the client's sense of identity.");
            grimoirePagesList.Add(theFanging);
        
        SinglePage theFawning = new SinglePage();
        theFawning.UpdateName("The Fawning");
        theFawning.UpdateDescription("An ailment in which predators begin to transform into prey after prolonged stress or self-esteem issues. Clients develop heightened fear responses, a nervous demeanour, and become increasingly paranoid of their surroundings.\n\nPhysical symptoms include a reduced appetite for meat, dulled teeth and claws, and the development of features of the prey species (such as antlers). Treatment should focus on healing physical transformation and gently restoring the client's confidence and sense of identity.");
            grimoirePagesList.Add(theFawning);

        foreach (SinglePage s in grimoirePagesList)
        {
            totalPages++;
        }

        // Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }

    public void ResetGrimoire()
    {
        grimoireNavigation.ResetGrimoireNavigation();
        stamp.ResetStamp();
    }
}
