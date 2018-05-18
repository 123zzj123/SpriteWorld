using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class Elf1Manager : MonoBehaviour
{

    public void LoadFightScene()
    {
        SceneManager.LoadScene("OfflineFightScene");
    }
}
