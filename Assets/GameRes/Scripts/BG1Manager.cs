using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class BG1Manager : MonoBehaviour {
    private int index;
    private GameObject panel;
    private GameObject canvas;
    private AudioSource audio;
    // Make sure that only one coroutine is running
    private bool isrunning;

    private void Start()
    {
        index = 1;
        isrunning = false;
        // Get canvas: background
        canvas = GameObject.Find("background");
        // Get panel: layout1
        panel = canvas.transform.Find("layout" + Convert.ToString(index)).gameObject;

        // Set active the firtst panel
        panel.GetComponent<CanvasGroup>().alpha = 1;
        panel.GetComponent<CanvasGroup>().interactable = true;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
        if (Input.GetMouseButtonDown(0))
        {
            if (index == 5)
            {
                var config = VuforiaConfiguration.Instance;
                var dbConfig = config.DatabaseLoad;

                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
                config.Vuforia.MaxSimultaneousImageTargets = 2;

                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj" };
                SceneManager.LoadScene("Loading");
                return;
            }

            if (!isrunning)
            {
                StartCoroutine(FadeScene());
            }
        }
    }

    IEnumerator FadeScene()
    {
        panel.GetComponent<FadeScene>().enabled = true;
        float time = panel.GetComponent<FadeScene>().BeginFade(1);
        isrunning = true;
        
        // End script
        panel.GetComponent<TextType>().enabled = false;
        audio = panel.transform.Find("Text").gameObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Stop();
            Destroy(audio);
        }

        yield return new WaitForSeconds(time);

        // Set false for the panel
        panel.GetComponent<CanvasGroup>().alpha = 0;
        panel.GetComponent<CanvasGroup>().interactable = false;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        panel.GetComponent<FadeScene>().BeginFade(-1);

        index++;
        panel = canvas.transform.Find("layout" + Convert.ToString(index)).gameObject;
        // Set active for the panel
        panel.GetComponent<CanvasGroup>().alpha = 1;
        panel.GetComponent<CanvasGroup>().interactable = true;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        // Start script
        panel.GetComponent<TextType>().enabled = true;

        isrunning = false;
    }
}
