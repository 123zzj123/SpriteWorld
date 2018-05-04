using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ObjManager : MonoBehaviour {

	public void GoToPetMenu()
    {
        var config = VuforiaConfiguration.Instance;
        var dbConfig = config.DatabaseLoad;

        dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
        SceneManager.LoadScene("PetMenu");
    }
}
