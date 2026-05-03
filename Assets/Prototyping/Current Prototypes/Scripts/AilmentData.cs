using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentData : MonoBehaviour
{
    public class Ailment
    {
        public string affectedClientName;

        public int primaryEffectDropdownNumber;
        public int primaryTargetDropdownNumber;
        public int secondaryEffectDropdownNumber;
        public int secondaryTargetDropdownNumber;

        public string modifierType;

        public void AttachToClient(string name)
        {
            affectedClientName = name;
        }

        public void SetAilmentInformation(int primE, int primT, int secE, int secT, int mod)
        {
            primaryEffectDropdownNumber = primE;
            primaryTargetDropdownNumber = primT;
            secondaryEffectDropdownNumber = secE;
            secondaryTargetDropdownNumber = secT;

            SetModifier(mod);
        }

        private void SetModifier(int number)
        {
            int modifier = number == 0 ? 0 : number == 1 ? 1 : number == 2 ? 2 : 3;

            if (modifier != 3)
            {
                UpdateModifier(modifier);
            }
            else
            {
                Debug.Log("Issue when trying to assign the correct modifier number.");
            }
            
        }

        private void UpdateModifier(int number)
        {
            string modName = number == 0 ? "none" : number == 1 ? "enhanced" : "inverted";
        }
    }

    public List<Ailment> allAilments;

    private static AilmentData global;
    public static AilmentData Global
    {
        get
        {
            return global;
        }
    }

    //void Awake()
    //{
    //    global = this;
    //}

    void Start()
    {
        SetupList();
        GenerateAilments();
    }

    void GenerateAilments()
    {
        Ailment honeye = new Ailment();
        //honeye.SetAilmentInformation();
        //honeye.AttachToClient("barry");
    }

    void SetupList()
    {
        allAilments = new List<Ailment>();

    }
}
