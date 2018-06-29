using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Set : MonoBehaviour {

    [SerializeField]
    private GameObject ConfirmButton;
    [SerializeField]
    private GameObject ReturnButton;

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
