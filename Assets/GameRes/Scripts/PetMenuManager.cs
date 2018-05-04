using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class PetMenuManager : MonoBehaviour {

    public GameObject[] Pets = new GameObject[5];
    public GameObject Null;
    private int curretPet = -1;

	void Start () {
		if(SSDirector.Pet != 0)
        {
            curretPet = SSDirector.CurrentPet;
            ShowPet(curretPet);
        }
        else
        {
            Null.SetActive(true);
        }
	}

    private void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
    }

    public void Search()
    {
        int index = SSDirector.Pet + 1;
        if (index <= 5)
        {
            var config = VuforiaConfiguration.Instance;
            var dbConfig = config.DatabaseLoad;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
            config.Vuforia.MaxSimultaneousImageTargets = 2;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj_OT" };
            SceneManager.LoadScene("Elf" + index);
        }
    }

    public void NetBattle()
    {
        if(SSDirector.Pet != 0)
        {
            SceneManager.LoadScene("NetRoom");
        }
    }

    public void Feed()
    {
        if(SSDirector.Pet != 0)
        {
            SceneManager.LoadScene("Feed");
        }
    }

    public void Walk()
    {
        if(SSDirector.Pet != 0)
        {
            var config = VuforiaConfiguration.Instance;
            var dbConfig = config.DatabaseLoad;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
            config.Vuforia.MaxSimultaneousImageTargets = 2;
            config.Vuforia.MaxSimultaneousObjectTargets = 1;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj" };
            SceneManager.LoadScene("Walk");
        }
    }

    public void NextPet()
    {
        if (curretPet == -1)
            return;
        DonShowPet(curretPet);
        ++curretPet;
        if(curretPet > SSDirector.Pet)
        {
            curretPet = curretPet - SSDirector.Pet;
        }
        ShowPet(curretPet);
        SSDirector.CurrentPet = curretPet;
    }

    public void LastPet()
    {
        if (curretPet == -1)
            return;
        DonShowPet(curretPet);
        --curretPet;
        if(curretPet == 0)
        {
            curretPet = SSDirector.Pet;
        }
        ShowPet(curretPet);
        SSDirector.CurrentPet = curretPet;
    }

    private void ShowPet(int ID)
    {
        Pets[ID-1].transform.position = new Vector3(0, 0, 8);
    }

    private void DonShowPet(int ID)
    {
        Pets[ID-1].transform.position = new Vector3(0, 0, -10);
    }
}
