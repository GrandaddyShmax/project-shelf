using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Controller
{
            #region Variables

            #region Variables - Components

    private TMP_Text comp_Text = null;

            // End of Variables - Components
            #endregion
            #region Variables - Managers

    private SceneryManager mng_SceneryManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Settings

    [Header("UI Controller")]
    [SerializeField] private bool set_ActiveOnShelf = false;
    [SerializeField] private bool set_ActiveOnTransition = false;
    [SerializeField] private bool set_ActiveOnProduct = false;

            // End of Variables - Settings
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        comp_Text = GetComponent<TMP_Text>();
    }

    protected override void ControllerUpdate() //[Trigger]
    {
        if (mng_SceneryManager == null)
            mng_SceneryManager = SceneryManager.instance;

        EnableDisable();
    }

    protected override void ControllerFixedUpdate()  //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Private

    private void EnableDisable() //[Update]
    {
        switch (mng_SceneryManager.pub_SceneState)
        {
            case enum_SceneState.Shelf:
                comp_Text.enabled = set_ActiveOnShelf;
                break;
            case enum_SceneState.Product:
                comp_Text.enabled = set_ActiveOnProduct;
                break;
            case enum_SceneState.Transition:
                comp_Text.enabled = set_ActiveOnTransition;
                break;
            default:
                break;
        }
    }

            // End of Functions - Private
            #endregion

            // End of Functions
            #endregion
}
