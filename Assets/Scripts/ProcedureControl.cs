using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureControl : MonoBehaviour
{
    [SerializeField] Procedure procedure;
    [SerializeField] ProcedureStep activeStep;
    [SerializeField] int activeStepCount;
    [SerializeField] int totalSteps;
    [SerializeField] GameObject additions;
    [SerializeField] Vector3 distanceOffset;

    public void SetProcedure(Procedure procedure)
    {
        this.procedure = procedure;
        
        activeStepCount = 0;
        activeStep = procedure.GetInitialStep();
        totalSteps = procedure.GetTotalStepCount();
        
        // set first step
        StateManager.Instance.supportInformation.SetInformation(procedure.GetInitialStep().GetDescription(), procedure.GetInitialStep().GetTitle());

        // instantiate additional functionality if available
        if (procedure.HasAdditions())
        {
            InstantiateAdditions();
        }
    }

    public void UnsetProcedure()
    {
        procedure = null;
        activeStepCount = 0;
        activeStep = null;
        totalSteps = 0;
        Destroy(additions);
        additions = null; // not necessary?!
    }

    public void Next()
    {
        // move to next step - update step count
        // TODO continue here
        if (activeStepCount == totalSteps-1)
            Debug.Log("No next steps - do nothing for now.");
        else
        {
            activeStepCount += 1;
            activeStep = procedure.GetStep(activeStepCount);
            StateManager.Instance.supportInformation.SetInformation(activeStep.GetDescription(), activeStep.GetTitle());
            activeStep.RunStep();

        }

    }

    public void Previous()
    {

        // move to previous step - update step count but only if possible
        if (activeStepCount == 0)
            Debug.Log("No previous steps - do nothing for now.");
        else
        {
            activeStepCount -= 1;
            activeStep = procedure.GetStep(activeStepCount);
            StateManager.Instance.supportInformation.SetInformation(activeStep.GetDescription(), activeStep.GetTitle());
            activeStep.RunStep();
        }
    }

    void InstantiateAdditions()
    {
        Vector3 inFrontOfUser = Camera.main.transform.position + distanceOffset;
        // spawn chest model in front of user
        additions = Instantiate(procedure.GetAdditions(), inFrontOfUser, Quaternion.identity);
        additions.transform.SetParent(StateManager.Instance.currentProcedure.transform, true);
    }

}
