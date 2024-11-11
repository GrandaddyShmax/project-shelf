using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
            #region Variables

            #region Variables - Managers

    private ControllerManager mng_controllerManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Settings

    [Header("Controller Settings")]
    [SerializeField] private enum_ControllerAssignment set_Manager = enum_ControllerAssignment.Default;
    [SerializeField] protected bool set_Update = false;
    [SerializeField] protected bool set_FixedUpdate = false;

            // End of Variables - Settings
            #endregion
            #region Variables - Constants

    private const int con_MaxAttempts = 10;

            // End of Variables - Constants
            #endregion
            #region Variables - Dynamic

    private int dyn_CurrentAttempt = 0;

            // End of Variables - Dynamic
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Start() //[Trigger]
    {
        GetControllerManager();
    }

    private void OnDestroy() //[Trigger]
    {
        ControllerManager.instance.RemoveController(this);
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void ManagerUpdate() //[Called]
    {
        if (set_Update)
            ControllerUpdate();
    }

    public void ManagerFixedUpdate() //[Called]
    {
        if (set_FixedUpdate)
            ControllerFixedUpdate();
    }

            // End of Functions - Public
            #endregion
            #region Functions - Private

    private async void GetControllerManager() //[Start]
    {
        if (set_Manager != enum_ControllerAssignment.Default)
            return;
        
        while (mng_controllerManager == null && dyn_CurrentAttempt < con_MaxAttempts)
        {
            mng_controllerManager = ControllerManager.instance;
            if (mng_controllerManager == null)
            {
                dyn_CurrentAttempt++;
                await Task.Delay(1000);
            }
            else
            {
                mng_controllerManager.AddController(this);
                break;
            }
        }

        if (mng_controllerManager == null)
        {
            Debug.LogError("Controller: Could not get Controller Manager instance after " + con_MaxAttempts + " attempts.");
        }
    }

            // End of Functions - Private
            #endregion
            #region Functions - Protected

    protected abstract void ControllerUpdate();
    protected abstract void ControllerFixedUpdate();

            // End of Functions - Protected
            #endregion

            // End of Functions
            #endregion
}
