using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrimoirePagesData : MonoBehaviour
{
    //public SinglePage HonEye;
    public class SinglePage
    {
        //public int/icon? which to use? int??
        public string ailmentName_;
        public string ailmentDescription_;

        public void UpdateName(string newName)
        {
            ailmentName_ = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            ailmentDescription_ = newDescription;
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

    void SetPageData()
    {
        SinglePage HonEye = new SinglePage();
        HonEye.UpdateName("Hon-Eye Infection");
        HonEye.UpdateDescription("An eye infection caused by eating bacteria-infested honey. Takes the appearance of amber-like crystals lining the lower eyelid. Treatment should focus on healing the affected eye, as it will permanently close if left untreated, and the host will lose their vision forever.");
            pagesList.Add(HonEye);
        totalPages++;

        SinglePage PageTwo = new SinglePage();
        PageTwo.UpdateName("Ailment 2: Electric Boogaloo");
        PageTwo.UpdateDescription("This is a Very Good description about a Very Real ailment that is Very Concerning, and may well be the answer to the client's issues.");
            pagesList.Add(PageTwo);
        totalPages++;

        SinglePage PageThree = new SinglePage();
        PageThree.UpdateName("Ailment 3 (real!)");
        PageThree.UpdateDescription("This is yet another compelling description about an ailment! Oh no, they are all so informative and bizarre, whichever could be the correct choice?");
            pagesList.Add(PageThree);
        totalPages++;

        SinglePage PageFour = new SinglePage();
        PageFour.UpdateName("Ailment IV: Couldn't Stand My Wife");
        PageFour.UpdateDescription("I dunno I'm thinking about horrible histories right now guys.");
            pagesList.Add(PageFour);
        totalPages++;

        Debug.Log("There are currently " + totalPages + " pages set up correctly.");

    }
    
    //List<string> ailmentDescriptions;


    //string ailment
    //
    //public void UpdatePage
}
