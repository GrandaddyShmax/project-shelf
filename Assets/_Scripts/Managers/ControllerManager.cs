using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Manager
{
            #region Variables

    public static ControllerManager instance;

            #region Variables - Dynamic

    private List<Controller> dyn_ControllerList = new List<Controller>();

            // End of Variables - Dynamic
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    protected override void ManagerStart() //[Trigger]
    {
        //
    }

    protected override void ManagerUpdate() //[Trigger]
    {
        foreach (Controller controller in dyn_ControllerList)
            controller.ManagerUpdate();
    }

    protected override void ManagerFixedUpdate() //[Trigger]
    {
        foreach (Controller controller in dyn_ControllerList)
            controller.ManagerFixedUpdate();
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void AddController(Controller controller) => dyn_ControllerList.Add(controller);

    public void RemoveController(Controller controller) => dyn_ControllerList.Remove(controller);

            // End of Functions - Public
            #endregion

            // End of Functions
            #endregion
}
