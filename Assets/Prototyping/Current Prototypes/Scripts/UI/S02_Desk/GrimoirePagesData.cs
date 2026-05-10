using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrimoirePagesData : MonoBehaviour
{
    public class SinglePage
    {
        //public int/icon? which to use? int??
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

    private List<SinglePage> pagesList;
    public SinglePage[] pagesArray;

    int totalPages = 0;

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
        //if (_instance != this)
        //{
        //    Destroy(GetComponent<GameObject>());
        //}
        
        InitialiseList();
        SetPageData();
        SetArray();
        //Debug.Log("pagesList is currently " + pagesList.Count + " entries long!.");
    }

    void InitialiseList()
    {
        pagesList = new List<SinglePage>();
    }

    void SetArray()
    {
        pagesArray = pagesList.ToArray();
        //Debug.Log("pagesArray is " + pagesArray.Length + " units long!");
    }

    void SetPageData()  // alphabetically set up for now
    {
        SinglePage chronicInsomnia = new SinglePage();
        chronicInsomnia.UpdateName("Chronic Insomnia");
        chronicInsomnia.UpdateDescription("Patients present with significant disturbances with their body's internal clock and circadian rhythm. These sleep complications are often shown to result in lower quality and quantity of sleep. \n\nDisturbances and symptoms have occurred longer than three months.");   //\n\nThis ");
            pagesList.Add(chronicInsomnia);

        SinglePage contaminationOCD = new SinglePage();
        contaminationOCD.UpdateName("Contamination-Specific OCD");
        contaminationOCD.UpdateDescription("Patients present with extreme neuroticism of germs, diseases, and contaminants. This fear results in ritualistic and repetitive self-implemented systems to help sooth and regulate stress responses.");
            pagesList.Add(contaminationOCD);

        SinglePage dietDrift = new SinglePage();
        dietDrift.UpdateName("Diet Drift");     // should prob actually be fortify mind!!
        dietDrift.UpdateDescription("An ailment in which the patient develops an insatiable hunger for food unnatural to their species. For example, a herbivore craving meat.\n\nThis condition is typically triggered by a traumatic experience involving the consumption of that forbidden diet. In some cases, it may progress into cannibalism if the trauma involved consuming a member of the same species. Treatment should focus on expelling corrupted thoughts and clearing the body of any impurities. ");
            pagesList.Add(dietDrift);

        SinglePage honEye = new SinglePage();
        honEye.UpdateName("Hon-Eye Infection");
        honEye.UpdateDescription("An eye infection caused by eating bacteria-infested honey. By the time its amber-like crystals have begun to line the lower eyelid, the case is extreme and may need a strong dosage. Treatment should primarily focus on healing the affected eye(s). If left untreated, the eye(s) will permanently shut, and the patient will lose their vision.");
            pagesList.Add(honEye);

        SinglePage illnessAnxiety = new SinglePage();
        illnessAnxiety.UpdateName("Illness Anxiety");
        illnessAnxiety.UpdateDescription("Patients present with an irrational fear and are convinced of having a serious health condition, despite being healthy.");
            pagesList.Add(illnessAnxiety);

        SinglePage orthorexia = new SinglePage();
        orthorexia.UpdateName("Orthorexia");
        orthorexia.UpdateDescription("Patients present fear of unhealthy food consumption, often believing food should only be eaten if it is clean, healthy, and pure, according to their understandings and standards. This often leads to malnutrition and disordered eating.");
            pagesList.Add(orthorexia);

        SinglePage sapEye = new SinglePage();
        sapEye.UpdateName("Sap-Eye Infection");
        sapEye.UpdateDescription("An eye infection caused by eating bacteria-infested tree sap. Takes the appearance of orange goo lining the lower eyelid. Treatment should primarily focus on healing the affected eye(s). If left untreated, the eye(s) will permanently shut, and the patient will lose their vision.");
            pagesList.Add(sapEye);

        SinglePage theBlues = new SinglePage();
        theBlues.UpdateName("The Blues");
        theBlues.UpdateDescription("A mental and physical ailment triggered by a sudden and intense source of sadness. The patient's skin begins to turn blue and melt away, and they become paralysed due to the heavy weight on their mind. Treatment should focus on calming the mind and healing the skin. If left untreated, the patient will eventually melt into a puddle of tears. ");
            pagesList.Add(theBlues);

        SinglePage theFanging = new SinglePage();
        theFanging.UpdateName("The Fanging");
        theFanging.UpdateDescription("An ailment transmitted through the bite of an infected Nocturnal Mosquito, causing the patient to transform into a bat-like creature.\n\nPatients may develop bat-like sensory processing, wings, sharpened teeth, and nocturnal instincts. If left untreated, the patient will fully transform into the bat-like creature. Treatment should focus on reversing physical and sensory transformation and reinforcing the patient's sense of identity.");
            pagesList.Add(theFanging);
        
        SinglePage theFawning = new SinglePage();
        theFawning.UpdateName("The Fawning");
        theFawning.UpdateDescription("An ailment in which predators begin to transform into prey after prolonged stress or self-esteem issues. Patients develop heightened fear responses, a nervous demeanour, and become increasingly paranoid of their surroundings.\n\nPhysical symptoms include a reduced appetite for meat, dulled teeth and claws, and the development of features of the prey species (such as antlers). Treatment should focus on reversing physical transformation and gently restoring the patient's confidence and sense of identity.");
            pagesList.Add(theFawning);

        foreach (SinglePage s in pagesList)
        {
            totalPages++;
        }

        Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
    
    //List<string> ailmentDescriptions;


    //string ailment
    //
    //public void UpdatePage
}
