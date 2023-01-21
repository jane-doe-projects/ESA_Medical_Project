using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;

public class ECGBehavior : ProcedureBehavior
{
    [SerializeField] GameObject chestModelPrefab;
    [SerializeField] GameObject chestModel;
    [SerializeField] GameObject tooltips;
    [SerializeField] Vector3 distanceOffset;

    [SerializeField] static BoundsControl boundControl;
    [SerializeField] static ObjectManipulator objManipulator;

    [SerializeField] public GameObject leadsParent;
    [SerializeField] static ECGLead[] leads;

    Vector3 oldPos;
    Vector3 newPos;


    private void Start()
    {
        leads = leadsParent.GetComponentsInChildren<ECGLead>();
        boundControl = chestModel.GetComponent<BoundsControl>();
        objManipulator = chestModel.GetComponent<ObjectManipulator>();
    }

    private void Update()
    {
        newPos = chestModel.transform.position;
        if (oldPos != newPos)
        {
            tooltips.transform.position = newPos;
        }
        oldPos = newPos;
    }

    public void S00Spawn()
    {
        Debug.Log("Introduction text.");
        HighlightAll();
    }

    public void S01PlaceModel()
    {
        // blend out every gameobject of the model that is not relevant for this step

        // unlock model if it is locked (in case user went to the previous procedure step
        // activate object manipulator and bound control (which will probably be already activated in case the user is going forward in the procedure
        UnlockChestModel();

        Debug.Log("Let the user place the torso");
        BlendOutAll();
    }

    public void S02PlaceLeadRL()
    {
        Debug.Log("Show R L indicators.");
        // automatically lock model if not locked 
        // deactivate object manipulator and bound control
        LockChestModel();

        HighlightTwoLeads(ECGLeadType.R, ECGLeadType.L);
        //HighlightLead(ECGLeadType.R);
        //HighlightLead(ECGLeadType.L);
    }

    public void S03PlaceLeadNF()
    {
        Debug.Log("Show N F indicators.");
        HighlightTwoLeads(ECGLeadType.N, ECGLeadType.F);
        //HighlightLead(ECGLeadType.N);
        //HighlightLead(ECGLeadType.F);
    }

    public void S04PlaceLeadC1()
    {
        Debug.Log("Show C1 indicator.");
        HighlightLead(ECGLeadType.C1);
    }

    public void S05PlaceLeadC2()
    {
        Debug.Log("Show C2 indicator.");
        HighlightLead(ECGLeadType.C2);
    }

    public void S06PlaceLeadC3()
    {
        Debug.Log("Show C3 indicator.");
        HighlightLead(ECGLeadType.C3);
    }

    public void S07PlaceLeadC4()
    {
        Debug.Log("Show C4 indicator.");
        HighlightLead(ECGLeadType.C4);
    }

    public void S08PlaceLeadC5()
    {
        Debug.Log("Show C5 indicator.");
        HighlightLead(ECGLeadType.C5);
    }

    public void S09PlaceLeadC6()
    {
        Debug.Log("Show C6 indicator.");
        HighlightLead(ECGLeadType.C6);
    }

    public void S10ConnectCables()
    {
        Debug.Log("Show all labels for leads so cables can be properly connected.");
        HighlightAll();
    }

    public void S11TempusProGuidance()
    {
        Debug.Log("Show instructions for TEMPUS Pro.");
    }

    void HighlightLead(ECGLeadType type)
    {
        foreach (ECGLead lead in leads)
        {
            if (lead.type == type)
            {
                lead.Highlight();
            }
            else
            {
                lead.BlendOut();

            }
        }
    }

    void HighlightTwoLeads(ECGLeadType type1, ECGLeadType type2)
    {
        foreach (ECGLead lead in leads)
        {
            //blend out all except the two
            if (lead.type != type1 && lead.type != type2)
                lead.BlendOut();
            else
                lead.Highlight();
        }

    }

    void HighlightAll()
    {
        foreach (ECGLead lead in leads)
        {
            lead.Highlight();
        }
    }

    void BlendOutAll()
    {
        foreach (ECGLead lead in leads)
        {
            lead.BlendOut();
        }
    }


    // info: two seperate functions so a toggle/switch solution does not accidently falsify the manipulation state of the chest model
    void LockChestModel()
    {
        boundControl.enabled = false;
        objManipulator.enabled = false;
    }

    void UnlockChestModel()
    {
        boundControl.enabled = true;
        objManipulator.enabled = true;
    }
}
