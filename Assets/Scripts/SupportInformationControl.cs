using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SupportInformationControl : MonoBehaviour
{
    //public TextMeshProUGUI supportText;
    public TextMeshPro supportTitle;
    public TextMeshPro supportText;
    public GameObject supportInformationPanel;

    void Start()
    {
        supportText.text = "";
        supportInformationPanel.SetActive(false);
    }

    public void SetInformation(string text, string title)
    {
        supportTitle.text = title;
        supportText.text = text;
        supportInformationPanel.SetActive(true);
    }

    public void HideInformation()
    {
        //supportText.text = "";
        supportInformationPanel.SetActive(false);
    }

}
