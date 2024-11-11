using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
            #region Variables

    public static GameManager instance;

            #region Variables - Managers

    [HideInInspector] public SceneryManager mng_SceneryManager;

            // End of Variables - Managers
            #endregion
            #region Variables - Settings

    [Header("Settings")]
    [SerializeField] private int set_FPS = 60;

            // End of Variables - Settings
            #endregion

            // End of Variables
            #endregion
            #region Functions

            #region Functions - Triggered

    private void Awake() //[Trigger]
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //[Trigger]
    {
        Application.targetFrameRate = set_FPS;
    }

            // End of Functions - Triggered
            #endregion

            // End of Functions
            #endregion
}
