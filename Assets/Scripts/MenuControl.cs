using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class MenuControl : MonoBehaviour
{
    [Header("Selection Menu")]
    public GameObject selectionMenu;
    public GameObject buttonCollection;
    [SerializeField] List<GameObject> buttons;

    [Header("Navigation Menu")]
    public GameObject navigationMenu;
    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject exitButton;

    void Awake()
    {
        buttons = new List<GameObject>();
        if (!buttonCollection)
            Debug.LogError("Button Collection not set.");

        // get all buttons in collection
        foreach (Transform child in buttonCollection.transform)
            buttons.Add(child.gameObject);
        
    }

    private void Start()
    {
        // toggle follow of selection menu on start
        selectionMenu.GetComponent<FollowMeToggle>().ToggleFollowMeBehavior();
        navigationMenu.GetComponent<FollowMeToggle>().ToggleFollowMeBehavior();
        navigationMenu.SetActive(false);
    }

    public void InitializeSelectionButtons(List<Procedure> procedures)
    {
        int lowerLimit = Mathf.Min(procedures.Count, buttons.Count); // TODO: this is not pretty, will improve later to not access arrays out of bounds
        for (int i = 0; i < lowerLimit; i++)
        {
            ButtonConfigHelper btnHelper = buttons[i].GetComponent<ButtonConfigHelper>();
            btnHelper.MainLabelText = procedures[i].GetTitle();
            btnHelper.OnClick.AddListener(procedures[i].StartProcedure);
        }
    }

    public void InitializeNavigation()
    {
        // just init exit button for now - to have a working prototype loop
        ButtonConfigHelper exitHelper = exitButton.GetComponent<ButtonConfigHelper>();
        exitHelper.OnClick.AddListener(StateManager.Instance.EndProcedure);

        // init prev / next with corresponding function calls
        ButtonConfigHelper prevHelper = previousButton.GetComponent<ButtonConfigHelper>();
        ButtonConfigHelper nextHelper = nextButton.GetComponent<ButtonConfigHelper>();
        prevHelper.OnClick.AddListener(StateManager.Instance.PreviousStep);
        nextHelper.OnClick.AddListener(StateManager.Instance.NextStep);

        // TODO grey out buttons if no other steps are available in either direction
    }

}
