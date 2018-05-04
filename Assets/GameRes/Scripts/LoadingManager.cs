using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {

    #region PRIVATE_MEMBER_VARIABLES

    bool mChangeLevel = true;
    RawImage mUISpinner;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mUISpinner = GetComponentInChildren<RawImage>();
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        mChangeLevel = true;
    }

    void Update()
    {
        if (mUISpinner)
            mUISpinner.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);

        if (mChangeLevel)
        {
            LoadNextSceneAsync();
            mChangeLevel = false;
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS

    void LoadNextSceneAsync()
    {
        string sceneName = "BackGround2";

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    #endregion // PRIVATE_METHODS
}
