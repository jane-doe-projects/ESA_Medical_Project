using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ECGLead : MonoBehaviour
{
    [SerializeField] string leadName;
    [SerializeField] GameObject tooltipObj;
    [SerializeField] ToolTipConnector connector;
    [SerializeField] ToolTip tooltip;

    [SerializeField] Material activeMat;
    [SerializeField] Material inactiveMat;

    public ECGLeadType type;

    void Start()
    {
        InitTooltip();
    }

    void InitTooltip()
    {
        connector = tooltipObj.GetComponent<ToolTipConnector>();
        connector.Target = this.gameObject;

        tooltip = tooltipObj.GetComponent<ToolTip>();
        tooltip.ToolTipText = leadName;
        ShowTT();
    }

    void HideTT()
    {
        tooltipObj.SetActive(false);
    }

    void ShowTT()
    {
        tooltipObj.SetActive(true);
    }

    public void Highlight()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.material = activeMat;
        ShowTT();
    }

    public void BlendOut()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.material = inactiveMat;
        HideTT();
    }
}

public enum ECGLeadType
{
    // according to IEC (International Electrotechnical Commission)
    None, R, L, N, F, C1, C2, C3, C4, C5, C6
}
