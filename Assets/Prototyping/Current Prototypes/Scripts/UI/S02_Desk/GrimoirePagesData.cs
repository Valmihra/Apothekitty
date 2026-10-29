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
    
        public Rect GetAndReturnGrimoireRect()
        {
            // *TAG* - For grimoire's stamp.
            // Currently using the whole page, but could set to ailment icon specifically if that's preferred!!
            grimoireRect.center = transform.position;
            return grimoireRect;
        }

        public bool SelectedPageIsNotTheCover()
        {
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
        chronicInsomnia.UpdateDescription("Clients present with significant disturbances with their body's internal clock and circadian rhythm, typically resulting in a lower quantity and quality of sleep\nShould symptoms persist for longer than three months, the client's physical wellbeing may also become affected. In these cases, practitioners should consider not only ways to resist potential stressors, but also to revitalise the body.");
            grimoirePagesList.Add(chronicInsomnia);

        SinglePage contaminationOCD = new SinglePage();
        contaminationOCD.UpdateName("Contamination-Specific OCD");
        contaminationOCD.UpdateDescription("Clients present with extreme neuroticism of germs, diseases, and contaminants. Not to be confused with Illness Anxiety, the client develops ritualistic and repetitive self-implemented systems to help soothe and regulate stress responses, but they do not believe they have any specific condition. /nPractitioners should do what they can to enable clients to soothe themselves to reduce the frequency of these thoughts. Take note that resisting thoughts entirely can make their condition worse.");
            grimoirePagesList.Add(contaminationOCD);

        SinglePage dietDrift = new SinglePage();
        dietDrift.UpdateName("Diet Drift");     // should prob actually be fortify mind!!
        dietDrift.UpdateDescription("The client demonstrates an insatiable hunger for food incompatible with their physiology. Observed to be an evolutionary coping mechanism in times of extreme resource scarcity, these cravings only present themselves in clients who have already consumed the food. As acts typically stemming from malnourished desperation, most patients are troubled by these thoughts. Treatment should ensure the client is calm in the face of these thoughts, and that their body is able to mend any physical harm done by malnutrition.");
            grimoirePagesList.Add(dietDrift);

        SinglePage honEye = new SinglePage();
        honEye.UpdateName("Hon-Eye Infection");
        honEye.UpdateDescription("A bacterial infection typically contracted by consuming a contaminated food source. Clients present with thickened optic discharge that rapidly hardens upon contact with the air. Discharge increases in quantity and frequency as the infection progresses, making hardened crystals extremely difficult to remove without damaging the affixed area. Left untreated, the affected eye(s) will be permanently sealed shut. To avoid excessive pain or blindness, swiftly remove the crystals and seek herbs to mend the tissue underneath.");
            grimoirePagesList.Add(honEye);

        SinglePage illnessAnxiety = new SinglePage();
        illnessAnxiety.UpdateName("Illness Anxiety");
        illnessAnxiety.UpdateDescription("A condition wherein the client believes themself to have a specific health condition, despite being physically fine. They often share a similar neuroticism towards contaminants as those with C-S OCD, leading to frequent misdiagnoses. Their concern is typically centred around a specific illness rather than the concept of contamination. To combat this belief, practitioners should help them to resist their thoughts and soothe any potential physical tension experienced as a result.");
            grimoirePagesList.Add(illnessAnxiety);

        SinglePage orthorexia = new SinglePage();
        orthorexia.UpdateName("Orthorexia");
        orthorexia.UpdateDescription("Clients present fear of unhealthy food consumption, often believing food should only be eaten if it is clean, healthy, and pure, according to their understandings and standards. This often leads to malnutrition and disordered eating. While the condition drastically affects the body, the client's mental wellbeing is of primary concern. Treatment should focus on building a resistance to their fears, and then to restore the body to its previous state.");
            grimoirePagesList.Add(orthorexia);

        SinglePage sapEye = new SinglePage();
        sapEye.UpdateName("Sap-Eye Infection");
        sapEye.UpdateDescription("An eye infection caused by eating bacteria-infested tree sap. Takes the appearance of orange goo lining the lower eyelid. Distinguished from Hon-Eye cases by the consistency of the fluid; as optic discharge remains a dense liquid. Early removal is still of the utmost importance, as prolonged exposure to the sap-like substance will gradually corrode the cornea and cause blindness. Treatment should primarily focus on reversing damage done to the affected eye(s).");
            grimoirePagesList.Add(sapEye);

		SinglePage stageFright = new SinglePage();
		stageFright.UpdateName("Stage Fright");
		stageFright.UpdateDescription("An intense anxiety for being perceived, specifically when performing and speaking to others. Patients may stutter, involuntarily shake, and have a strong desire to isolate themselves when experiencing this ailment. While some clients may describe feelings of paralysis, they are still capable of movement. The onset of these thoughts is typically sudden, with no perceivable underlying cause. Practitioners should help the client to soothe their anxious thoughts.");
			grimoirePagesList.Add(stageFright);

        SinglePage theBlues = new SinglePage();
        theBlues.UpdateName("The Blues");
        theBlues.UpdateDescription("A mental and physical ailment triggered by a sudden and intense source of sadness. The client's skin begins to turn blue and melt away, and they become paralysed due to the heavy weight on their mind. Early diagnosis is crucial to prevent exacerbating the client's symptoms further; left untreated, the client will eventually melt into a puddle of tears. Treatment should focus on calming the mind and healing the skin.");
            grimoirePagesList.Add(theBlues);

        SinglePage theFanging = new SinglePage();
        theFanging.UpdateName("The Fanging");
        theFanging.UpdateDescription("An ailment transmitted through the bite of an infected Nocturnal Mosquito, causing the client to transform into a bat-like creature. In addition to more nocturnal instincts, clients may develop bat-like sensory processing, wings, sharpened teeth, and noticeably leathery flesh. If left untreated, the client will fully transform into this creature, forgetting their past life. Treatment should focus on healing physical transformation and strongly reinforcing the client's original identity.");
            grimoirePagesList.Add(theFanging);
        
        SinglePage theFawning = new SinglePage();
        theFawning.UpdateName("The Fawning");
        theFawning.UpdateDescription("An ailment in which predators begin to transform into prey animals after prolonged stress or self-esteem issues. Clients develop heightened fear responses, a nervous demeanour, and become increasingly paranoid of their surroundings. Physical symptoms include a reduced appetite for meat, dulled teeth and claws, and the development of features of the prey species (such as antlers). Treatment should focus on reversing harm done by the physical transformation and helping to comfort and settle the client's sense of self.");
            grimoirePagesList.Add(theFawning);

        foreach (SinglePage s in grimoirePagesList)
        {
            totalPages++;
        }
    }

    public void ResetGrimoire()
    {
        grimoireNavigation.ResetGrimoireNavigation();
		// *TAG* - Stamp probably shouldn't be here.
        stamp.ResetStamp();
    }
}
