using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Manager
{
            #region Variables

    public static UIManager instance;

            #region Variables - Managers

    private ProductManager mng_ProductManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Components

    [Header("Components")]
    [SerializeField] private RectTransform comp_Board = null;
    [SerializeField] private RectTransform comp_Notification = null;
    [SerializeField] private TMP_InputField[] comp_InputFields = new TMP_InputField[0];

            // End of Variables - Components
            #endregion
            #region Variables - Settings

    [Header("Settings")]
    [SerializeField] private float set_CloseBoardDistance = 700;
    [SerializeField] private float set_CloseNotificationDistance = 700;
    [SerializeField] private AnimationCurve set_VerticalCurve = null;
    [SerializeField] private AnimationCurve set_RotationCurve = null;

            // End of Variables - Settings
            #endregion
            #region Variables - Flags

    private bool flag_Animating = false;

            // End of Variables - Flags
            #endregion
            #region Variables - Dynamic

    private Vector2 dyn_OpenBoardPosition = new Vector2(0, 0);
    private Vector2 dyn_CloseBoardPosition = new Vector2(0, 0);
    private Vector2 dyn_OpenNotificationPosition = new Vector2(0, 0);
    private Vector2 dyn_CloseNotificationPosition = new Vector2(0, 0);

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
        mng_ProductManager = ProductManager.instance;
        
        dyn_OpenBoardPosition = new Vector2(comp_Board.position.x, comp_Board.position.y);
        dyn_CloseBoardPosition = new Vector2(comp_Board.position.x + set_CloseBoardDistance, comp_Board.position.y);
        comp_Board.position = dyn_CloseBoardPosition;
        comp_Board.gameObject.SetActive(false);

        dyn_OpenNotificationPosition = new Vector2(comp_Notification.position.x, comp_Notification.position.y);
        dyn_CloseNotificationPosition = new Vector2(comp_Notification.position.x, comp_Notification.position.y - set_CloseNotificationDistance);
        comp_Notification.position = dyn_CloseNotificationPosition;
        comp_Notification.gameObject.SetActive(false);
    }

    protected override void ManagerUpdate() //[Trigger]
    {
        //
    }

    protected override void ManagerFixedUpdate() //[Trigger]
    {
        //
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void UpdateBoard(int _index) // [Called]
    {
        comp_InputFields[0].text = mng_ProductManager.GetProductText(_index, 0);
        comp_InputFields[1].text = mng_ProductManager.GetProductText(_index, 1);
    }

    public void ShowHideBoard(bool _show) // [Called]
    {
        if (flag_Animating)
            return;
        
        if (_show)
            StartCoroutine(AnimateBoard(true));
        else
            StartCoroutine(AnimateBoard(false));
    }

    public void ShowNotification() // [Called]
    {
        if (flag_Animating)
            return;
        
        StartCoroutine(AnimateNotification());
    }

    public string GetInputFieldText(int _index) => comp_InputFields[_index].text;

            // End of Functions - Public
            #endregion
            #region Functions - Coroutines

    private IEnumerator AnimateBoard(bool _show) // [Called - ShowHideBoard]
    {
        if (_show)
        {
            comp_Board.gameObject.SetActive(true);
            comp_Board.position = dyn_CloseBoardPosition;
            while (Vector3.Distance(comp_Board.position, dyn_OpenBoardPosition) > 0.1f && mng_SceneryManager.pub_SceneState == enum_SceneState.Transition)
            {
                comp_Board.position = Vector3.Lerp(comp_Board.position, dyn_OpenBoardPosition, Time.deltaTime * 10);
                yield return null;
            }
            comp_Board.position = dyn_OpenBoardPosition;
        }
        else
        {
            comp_Board.position = dyn_OpenBoardPosition;
            while (Vector3.Distance(comp_Board.position, dyn_CloseBoardPosition) > 0.1f && mng_SceneryManager.pub_SceneState == enum_SceneState.Transition)
            {
                comp_Board.position = Vector3.Lerp(comp_Board.position, dyn_CloseBoardPosition, Time.deltaTime * 10);
                yield return null;
            }
            comp_Board.position = dyn_CloseBoardPosition;
            comp_Board.gameObject.SetActive(false);
        }
    }

    private IEnumerator AnimateNotification() // [Called - ShowNotification]
    {
        flag_Animating = true;

        comp_Notification.gameObject.SetActive(true);
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            comp_Notification.position = new Vector2(comp_Notification.position.x, Mathf.Lerp(dyn_CloseNotificationPosition.y, dyn_OpenNotificationPosition.y, set_VerticalCurve.Evaluate(time)));
            comp_Notification.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -720, set_RotationCurve.Evaluate(time)));
            yield return null;
        }
        comp_Notification.position = dyn_CloseNotificationPosition;
        comp_Notification.gameObject.SetActive(false);

        flag_Animating = false;
    }

            // End of Functions - Coroutines
            #endregion

            // End of Functions
            #endregion
}
