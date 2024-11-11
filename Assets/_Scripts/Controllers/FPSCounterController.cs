using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounterController : Controller
{
            #region Variables

            #region Variables - Components

    private TMP_Text comp_FPS = null;

            // End of Variables - Components
            #endregion
            #region Variables - Dynamic

    private float[] dyn_FrameDeltaTimeArray;
    private int dyn_LastFrameIndex;

            // End of Variables - Dynamic
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        comp_FPS = GetComponent<TMP_Text>();
        dyn_FrameDeltaTimeArray = new float[100];
    }

    protected override void ControllerUpdate() //[Trigger]
    {
        FPSCounter();
    }

    protected override void ControllerFixedUpdate() //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Private

    private void FPSCounter() //[Update]
    {
        dyn_FrameDeltaTimeArray[dyn_LastFrameIndex] = Time.unscaledDeltaTime;
        dyn_LastFrameIndex = (dyn_LastFrameIndex + 1) % dyn_FrameDeltaTimeArray.Length;

        comp_FPS.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private float CalculateFPS() //[Update - FPSCounter]
    {
        float sum = 0;
        for (int i = 0; i < dyn_FrameDeltaTimeArray.Length; i++)
        {
            sum += dyn_FrameDeltaTimeArray[i];
        }
        return 1f / (sum / dyn_FrameDeltaTimeArray.Length);
    }

            // End of Functions - Private
            #endregion

            // End of Functions
            #endregion
}
