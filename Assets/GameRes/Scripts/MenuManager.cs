using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    const string player_pet = "PET";

    private void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
    }
    public void ShowHelpMessage()
    {
        StartCoroutine(AudioManager.LoadingNextScene("MainPanel", "Help"));
    }

    public void ShowSettingPanel()
    {
        //to do
        StartCoroutine(AudioManager.LoadingNextScene("MainPanel", "Set"));
    }

    public void ReStartGame()
    {
        //to do
    }

    public void SetMusic()
    {
        //to do
    }

    public void Play()
    {
        if (PlayerPrefs.HasKey(player_pet))
        {
            SSDirector.Pet = PlayerPrefs.GetInt(player_pet);
        }
        if(SSDirector.Pet == 0)
        {
            StartCoroutine(AudioManager.LoadingNextScene("MainPanel", "BackGround1"));
        }
        else
        {
            StartCoroutine(AudioManager.LoadingNextScene("MainPanel", "PetMenu"));
        }
    }

    public void Exit()
    {
        GameObject.Find("MainPanel").GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}
