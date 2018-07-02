using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    private static AsyncOperation operation;

    public static IEnumerator LoadingNextScene(string objectName, string sceneName)
    {
        GameObject.Find(objectName).GetComponent<AudioSource>().Play();
        operation = SceneManager.LoadSceneAsync(sceneName);
        //阻止当加载完成自动切换  
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(0.1f);

        operation.allowSceneActivation = true;
    }

    public static IEnumerator PlayMenuAudio(string objectName)
    {
        GameObject.Find(objectName).GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
    }
}
