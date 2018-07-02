using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class BG2Manager : MonoBehaviour {

    public void LoadNextScene()
    {
        StartCoroutine(NextScene("NewElf1"));
    }
    IEnumerator NextScene(string name)
    {
        GameObject.Find("Manager").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(name);
    }
}
