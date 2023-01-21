using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureBehavior : MonoBehaviour
{
    [SerializeField] ProcedureControl procedureControl;

    private void Start()
    {
        procedureControl = StateManager.Instance.currentProcedure;
    }

}
