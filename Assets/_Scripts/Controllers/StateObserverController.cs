using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateObserverController : Controller
{
            #region Variables

            #region Variables - Managers

    private SceneryManager mng_SceneryManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Components

    private TMP_Text comp_State = null;

            // End of Variables - Components
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        comp_State = GetComponent<TMP_Text>();
    }

    protected override void ControllerUpdate() //[Trigger]
    {
        if (mng_SceneryManager == null)
            mng_SceneryManager = SceneryManager.instance;
        
        CurrentState();
    }

    protected override void ControllerFixedUpdate() //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Private

    private void CurrentState() => comp_State.text = mng_SceneryManager.pub_SceneState.ToString();

            // End of Functions - Private
            #endregion

            // End of Functions
            #endregion
}
