using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Procedure", menuName = "MARA Items/Procedures/Procedure", order = 1)]
public class Procedure : ScriptableObject
{
    // base class for procedures
    [SerializeField] string title;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] GameObject additionsPrefab;

    [SerializeField] bool isActive;
    [SerializeField] List<ProcedureStep> steps;

    public string GetTitle() { return title; }
    public string GetDescription() { return description; }
    public bool IsActive() { return isActive; }

    public void StartProcedure()
    {
        StateManager.Instance.StartProcedure(this);
    }

    public ProcedureStep GetInitialStep()
    {
        if (steps == null || steps.Count < 1)
        {
            ProcedureStep placeholderStep = new ProcedureStep("none set", "none set");
            return placeholderStep;
        }
        return steps[0];
    }

    public List<ProcedureStep> GetSteps() { return steps; }

    public int GetTotalStepCount() { return steps.Count; }

    public ProcedureStep GetStep(int index)
    {
        return steps[index];
    }

    public bool HasAdditions()
    {
        if (additionsPrefab != null)
            return true;

        return false;
    }

    public GameObject GetAdditions()
    {
        return additionsPrefab;
    }

}

[System.Serializable]
public class ProcedureStep
{
    [SerializeField] string stepTitle;
    [TextArea]
    [SerializeField] string description; // support / guidance text
    [SerializeField] UnityEvent functionCall;

    public ProcedureStep(string title, string description)
    {
        stepTitle = title;
        this.description = description;
    }
    public string GetTitle() { return stepTitle; }
    public string GetDescription() { return description; }

    public void RunStep()
    {
        functionCall.Invoke();
    }


}
