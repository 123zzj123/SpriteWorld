using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingManager : MonoBehaviour {

    #region PRIVATE_MEMBER_VARIABLES

    private bool mChangeLevel = true;
    private GameObject Spinner;
    private AsyncOperation operation;
    private float rotateSpeed;
    private float angle;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        rotateSpeed = 0.2f;
        angle = 0;
        Spinner = GameObject.Find("Spinner");
        Debug.Log(Spinner);
        StartCoroutine(AsyncLoading());
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void Update()
    {
        Spinner.transform.Rotate(Vector3.back * (rotateSpeed * 10));
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync("BackGround2");
        //阻止当加载完成自动切换  
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(1/ rotateSpeed);

        // 切换场景
        operation.allowSceneActivation = true;
    }

    #endregion // PRIVATE_METHODS
}
