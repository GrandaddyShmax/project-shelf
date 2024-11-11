using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{
            #region Variables

    public static SceneryManager instance;

            #region Variables - Managers

    [Header("Default Managers")]
    [SerializeField] private List<GameObject> mng_DefaultManagers = new List<GameObject>();

    private GameManager mng_gameManager;
    private List<Manager> mng_ManagerList = new List<Manager>();

            // End of Variables - Managers
            #endregion
            #region Variables - Public

    [HideInInspector] public bool pub_SceneLoaded = false;
    [HideInInspector] public enum_SceneState pub_SceneState = enum_SceneState.Shelf;

            // End of Variables - Public
            #endregion
            #region Variables - Const

    private const int con_MaxAttempts = 10;

            // End of Variables - Const
            #endregion
            #region Variables - Flags

    private bool flag_CalledManagers = false;

            // End of Variables - Flags
            #endregion
            #region Variables - Dynamic

    private int dyn_CurrentAttempt = 0;
    private enum_SceneState dyn_LastState = enum_SceneState.Shelf;

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

    private void Start() //[Trigger]
    {
        GetGameManager();
        GetManagers();
    }

    private void Update() //[Trigger]
    {
        if (flag_CalledManagers)
        {
            if (AllLoaded())
            {
                flag_CalledManagers = false;
                pub_SceneLoaded = true;
            }
        }

        if (pub_SceneLoaded)
            foreach (var manager in mng_ManagerList)
                manager.SceneUpdate();
    }

    private void FixedUpdate() //[Trigger]
    {
        if (pub_SceneLoaded)
            foreach (var manager in mng_ManagerList)
                manager.SceneFixedUpdate();
    }

            // End of Functions - Triggered
            #endregion
            #region Functions - Public

    public void SceneChange() //[Called]
    {
        if (pub_SceneState != enum_SceneState.Transition)
        {
            dyn_LastState = pub_SceneState;
            pub_SceneState = enum_SceneState.Transition;
        }
        else
            pub_SceneState = dyn_LastState == enum_SceneState.Shelf ? enum_SceneState.Product : enum_SceneState.Shelf;
    }

            // End of Functions - Public
            #endregion
            #region Functions - Private

    private void GetManagers() //[Start]
    {
        foreach (Transform child in this.transform)
        {
            Manager manager = child.GetComponent<Manager>();
            if (manager != null)
            {
                mng_ManagerList.Add(manager);
                manager.mng_SceneryManager = instance;
            }
        }

        GenerateDefaultManagers();

        foreach (var manager in mng_ManagerList)
        {
            manager.SceneStart();
        }

        flag_CalledManagers = true;
    }

    private void GenerateDefaultManagers() //[Start - GetManager]
    {
        foreach (GameObject prefab in mng_DefaultManagers)
        {
            bool flag = false;

            foreach (Transform child in this.transform)
            {
                if (child.gameObject.name == prefab.name)
                {
                    Debug.LogWarning("SceneryManager: Default manager prefab " + prefab.name + " already exists in the scene.");
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                GameObject managerPrefab = Instantiate(prefab, this.transform);
                Manager manager = managerPrefab.GetComponent<Manager>();
                if (manager == null)
                {
                    Debug.LogError("SceneryManager: Default manager prefab " + prefab.name + " does not have a Manager component.");
                    Destroy(managerPrefab);
                }
                else
                {
                    manager.name = manager.name.Replace("(Clone)", "");
                    mng_ManagerList.Add(manager);
                    manager.mng_SceneryManager = instance;
                }
            }
        }
    }

    private bool AllLoaded() //[Update]
    {
        foreach (var manager in mng_ManagerList)
        {
            if (!manager.pub_SceneLoaded)
            {
                return false;
            }
        }

        return true;
    }

            // End of Functions - Private
            #endregion
            #region Functions - Async

    private async void GetGameManager() //[Start]
    {
        while (mng_gameManager == null && dyn_CurrentAttempt < con_MaxAttempts)
        {
            mng_gameManager = GameManager.instance;
            if (mng_gameManager == null)
            {
                dyn_CurrentAttempt++;
                await Task.Delay(1000);
            }
            else
            {
                mng_gameManager.mng_SceneryManager = this;
                break;
            }
        }

        if (mng_gameManager == null)
        {
            Debug.LogError("SceneryManager: Could not get Game Manager instance after " + con_MaxAttempts + " attempts.");
        }
    }

            // End of Functions - Async
            #endregion

            // End of Functions
            #endregion
}
