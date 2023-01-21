using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public ProcedureCollection collection;
    public MenuControl menuControl;

    public ProcedureControl currentProcedure;
    public TextMeshProUGUI currentTitle;
    public TextMeshProUGUI currentProcedureTitle;

    public SupportInformationControl supportInformation;

    void Start()
    {
        // set up singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        supportInformation = GetComponent<SupportInformationControl>();

        // init buttons
        menuControl.InitializeNavigation();
        menuControl.InitializeSelectionButtons(collection.GetProcedures());
        currentTitle.text = "MARA Prototype";
        currentProcedureTitle.text = "";
    }

    public void StartProcedure(Procedure procedure)
    {
        if (!procedure.IsActive())
            return;

        currentProcedure.SetProcedure(procedure);

        // update text
        currentProcedureTitle.text = procedure.GetTitle();

        // maybe hide menu / swap it for navigation menu
        menuControl.selectionMenu.SetActive(false);
        menuControl.navigationMenu.transform.position = menuControl.selectionMenu.transform.position;
        menuControl.navigationMenu.transform.rotation = menuControl.selectionMenu.transform.rotation;
        menuControl.navigationMenu.SetActive(true);
    }

    public void EndProcedure()
    {
        // TODO make sure to reset everything
        // swap menus and their orientation / position
        menuControl.selectionMenu.SetActive(true);
        menuControl.selectionMenu.transform.position = menuControl.navigationMenu.transform.position;
        menuControl.selectionMenu.transform.rotation = menuControl.navigationMenu.transform.rotation;
        menuControl.navigationMenu.SetActive(false);
        ResetProcedureText();
        currentProcedure.UnsetProcedure();
    }

    public void ResetProcedureText()
    {
        currentProcedureTitle.text = "";

        supportInformation.HideInformation();
    }

    public void NextStep()
    {
        currentProcedure.Next();
    }

    public void PreviousStep()
    {
        currentProcedure.Previous();
    }
}
