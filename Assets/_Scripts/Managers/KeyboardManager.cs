using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
            #region Variables

    [DllImport("__Internal")] private static extern void ShowMobileKeyboard();

    [DllImport("__Internal")] private static extern void HideMobileKeyboard();

            #region Variables - Dynamic

    private TMP_InputField dyn_CurrentInputField;

            // End of Variables - Dynamic
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Public

    public void OnInputFieldSelected(TMP_InputField _inputField) // [Called]
    {
        dyn_CurrentInputField = _inputField;
#if !UNITY_EDITOR && UNITY_WEBGL
            ShowMobileKeyboard();
#endif
    }

    public void OnInputFieldDeselected() // [Called]
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            HideMobileKeyboard();
#endif
    }

    public void OnMobileInput(string value) // [Called]
    {
        dyn_CurrentInputField.text = value;
    }

            // End of Functions - Public
            #endregion

            // End of Functions
            #endregion
}