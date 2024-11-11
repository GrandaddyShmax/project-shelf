using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
            #region Variables

            #region Variables - Managers

    [HideInInspector] public SceneryManager mng_SceneryManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Public

    [HideInInspector] public bool pub_SceneLoaded = false;

            // End of Variables - Public
            #endregion
            #region Variables - Flags

    private bool flag_SceneStart = false;

            // End of Variables - Flags
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void OnEnable() //[Trigger]
    {
        mng_SceneryManager = SceneryManager.instance;
    }

    private void Start() //[Trigger]
    {
        if (!mng_SceneryManager)
            return;
        if (mng_SceneryManager.pub_SceneLoaded && !flag_SceneStart)
            SceneStart();
    }

            // End of Functions - Triggered
            #endregion
            #region Function - Public

    public void SceneStart() //[Called]
    {
        if (flag_SceneStart)
            return;
        flag_SceneStart = true;
        ManagerStart();
        pub_SceneLoaded = true;
    }

    public void SceneUpdate() //[Called]
    {
        if (pub_SceneLoaded)
            ManagerUpdate();
    }

    public void SceneFixedUpdate() //[Called]
    {
        if (pub_SceneLoaded)
            ManagerFixedUpdate();
    }

            // End of Functions - Public
            #endregion
            #region Functions - Protected

    protected abstract void ManagerStart();
    protected abstract void ManagerUpdate();
    protected abstract void ManagerFixedUpdate();

            // End of Functions - Protected
            #endregion

            // End of Functions
            #endregion
}
