using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class BG1Manager : MonoBehaviour {
    void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
        if (Input.GetMouseButtonDown(0))
        {
            var config = VuforiaConfiguration.Instance;
            var dbConfig = config.DatabaseLoad;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
            config.Vuforia.MaxSimultaneousImageTargets = 2;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj" };
            SceneManager.LoadScene("Loading");
        }
    }
}
