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
        SceneManager.LoadScene("Help");
    }

    public void ShowSettingPanel()
    {
        //to do
        SceneManager.LoadScene("Set");
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
            SceneManager.LoadScene("BackGround1");
        }
        else
        {
            SceneManager.LoadScene("PetMenu");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
