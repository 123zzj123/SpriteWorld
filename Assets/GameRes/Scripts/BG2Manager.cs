using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class BG2Manager : MonoBehaviour {

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Elf1");
    }
}
