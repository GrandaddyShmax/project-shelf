using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
            #region Variables

    public static PlayerManager instance;

            #region Variables - Managers

    private UIManager mng_UIManager;
    private ProductManager mng_ProductManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Components

    [Header("Components")]
    [SerializeField] private Transform comp_ObjTransform;

            // End of Variables - Components
            #endregion
            #region Variables - Flags

    private bool flag_OnDestroy = false;

            // End of Variables - Flags
            #endregion
            #region Variables - Dynamic

    private GameObject dyn_SelectedObject;
    private Vector3 dyn_ObjStartPosition;
    private Vector3 dyn_ObjStartRotation;

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
        mng_UIManager = UIManager.instance;
        mng_ProductManager = ProductManager.instance;
    }

    protected override void ManagerUpdate() //[Trigger]
    {
        if (mng_SceneryManager.pub_SceneState == enum_SceneState.Shelf)
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                OnClickOrTap();
    }

    protected override void ManagerFixedUpdate() //[Trigger]
    {
        //
    }

    private void OnDestroy() //[Trigger]
    {
        flag_OnDestroy = true;
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void ExitProductScreen() //[Called]
    {
        if (mng_SceneryManager.pub_SceneState != enum_SceneState.Product)
            return;
        
        mng_SceneryManager.SceneChange();

        mng_UIManager.ShowHideBoard(false);
        StartCoroutine(AnimatePosition(dyn_SelectedObject, dyn_ObjStartPosition));
        StartCoroutine(AnimateRotation(dyn_SelectedObject, dyn_ObjStartRotation));
    }

    public int GetSelectedObjectIndex() => (dyn_SelectedObject.transform.parent.parent.name[6] - '0') - 1;

    public void UpdateProduct() //[Called]
    {
        bool success = mng_ProductManager.UpdateProduct(GetSelectedObjectIndex());

        if (success)
            mng_UIManager.ShowNotification();
    }

            // End of Functions - Public
            #endregion
            #region Functions - Private

    private void OnClickOrTap() //[Update]
    {
        dyn_SelectedObject = GetObjectOnTouch();
        if (dyn_SelectedObject == null)
            return;

        dyn_ObjStartPosition = dyn_SelectedObject.transform.position;
        dyn_ObjStartRotation = dyn_SelectedObject.transform.eulerAngles;
        ProductScreen();
    }

    private GameObject GetObjectOnTouch() //[Update - OnClickOrTap]
    {
        Vector3 pos = GetTouchOrClickPosition();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;

        return null;
    }

    private Vector3 GetTouchOrClickPosition() //[Update - OnClickOrTap - GetObjectOnTouch]
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;

        if (Input.GetMouseButtonDown(0))
            return Input.mousePosition;

        return Vector3.zero;
    }

    private void ProductScreen() //[Update - OnClickOrTap]
    {
        if (mng_SceneryManager.pub_SceneState != enum_SceneState.Shelf)
            return;

        mng_SceneryManager.SceneChange();

        mng_UIManager.UpdateBoard(GetSelectedObjectIndex());
        mng_UIManager.ShowHideBoard(true);
        StartCoroutine(RotateProduct());
        StartCoroutine(AnimatePosition(dyn_SelectedObject, comp_ObjTransform.position));
    }

            // End of Functions - Private
            #endregion
            #region Functions - Coroutines

    private IEnumerator RotateProduct() //[Update - ProductScreen]
    {
        while (!flag_OnDestroy && mng_SceneryManager.pub_SceneState != enum_SceneState.Shelf)
        {
            dyn_SelectedObject.transform.Rotate(Vector3.up, Time.deltaTime * 10);
            yield return null;
        }
    }

    private IEnumerator AnimatePosition(GameObject obj, Vector3 target) //[Update - ProductScreen/ExitProductScreen]
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.01f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, target, Time.deltaTime * 10);
            yield return null;
        }
        obj.transform.position = target;
        mng_SceneryManager.SceneChange();
    }

    private IEnumerator AnimateRotation(GameObject obj, Vector3 target) //[Update - ExitProductScreen]
    {
        while (Vector3.Distance(obj.transform.eulerAngles, target) > 0.01f && mng_SceneryManager.pub_SceneState == enum_SceneState.Transition)
        {
            obj.transform.eulerAngles = Vector3.Lerp(obj.transform.eulerAngles, target, Time.deltaTime * 12);
            yield return null;
        }
        obj.transform.eulerAngles = target;
    }

            // End of Functions - Coroutines
            #endregion

            // End of Functions
            #endregion
}
