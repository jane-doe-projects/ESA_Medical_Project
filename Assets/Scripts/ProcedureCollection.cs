using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureCollection : MonoBehaviour
{
    [SerializeField] List<Procedure> procedures;
    // Start is called before the first frame update
    void Start()
    {
        if (procedures.Count < 1)
            Debug.LogWarning("No procedures set.");
    }

    public List<Procedure> GetProcedures()
    {
        return procedures;
    }

}
