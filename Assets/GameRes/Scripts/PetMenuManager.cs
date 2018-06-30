using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

struct PropertyNumber
{
    public int Level;
    public int Experence;
    public int Intimacy;
    public int Hamlet;
    public int Attack;
    public int Defend;
}
public class PetMenuManager : MonoBehaviour
{

    public GameObject[] Pets = new GameObject[5];
    public GameObject[] Story = new GameObject[5];
    public GameObject Null;
    public GameObject Property;
    private int curretPet = -1;
    private int currentLayout = 0;
    private PropertyNumber[] Propertys = new PropertyNumber[5];

    public GameObject Storys;
    public GameObject Bound;
    public GameObject Rebound;

    void Start()
    {
        InitProperty(0, 1, 180, 0, 1035, 97, 73);
        InitProperty(1, 1, 180, 0, 939, 123, 71);
        InitProperty(2, 1, 180, 0, 960, 114, 68);
        InitProperty(3, 1, 180, 0, 1046, 99, 71);
        InitProperty(4, 1, 180, 0, 907, 123, 66);

        if (SSDirector.Pet != 0)
        {
            curretPet = SSDirector.CurrentPet;
            ShowPet(curretPet);
            Property.SetActive(true);
        }
        else
        {
            Null.SetActive(true);
            Property.SetActive(false);
        }
    }

    private void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
        Debug.Log(SSDirector.Pet);
    }

    public void Search()
    {
        int index = SSDirector.Pet + 1;
        Debug.Log(index);
        if (index <= 5)
        {
            var config = VuforiaConfiguration.Instance;
            var dbConfig = config.DatabaseLoad;

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
            config.Vuforia.MaxSimultaneousImageTargets = 2;

            //    dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj_OT" };

            dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] { "zzj" };
            SceneManager.LoadScene("NewElf" + index);
        }
    }

    public void NetBattle()
    {
        if (SSDirector.Pet != 0)
        {
            SceneManager.LoadScene("NetRoom");
        }
    }

    public void Feed()
    {
        if (SSDirector.Pet != 0)
        {
            SceneManager.LoadScene("Feed");
        }
    }

    public void Walk()
    {
        if (SSDirector.Pet != 0)
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
        if (curretPet > SSDirector.Pet)
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
        if (curretPet == 0)
        {
            curretPet = SSDirector.Pet;
        }
        ShowPet(curretPet);
        SSDirector.CurrentPet = curretPet;
    }

    private void ShowPet(int ID)
    {
        Pets[ID - 1].transform.position = new Vector3(0, 0, 8);
        if (ID == 1)
        {
            Pets[ID - 1].transform.position = new Vector3(-0.4f, -0.8f, 8.0f);
        }

        // show property
        GameObject.Find("Level").GetComponent<Text>().text = "Level." + Propertys[ID - 1].Level.ToString();
        GameObject.Find("Intimacy").GetComponent<Text>().text = "亲密度：" + Propertys[ID - 1].Intimacy.ToString();
        GameObject.Find("Hamlet").GetComponent<Text>().text = "生命值：" + Propertys[ID - 1].Hamlet.ToString();
        GameObject.Find("Experence").GetComponent<Text>().text = "经验值：" + Propertys[ID - 1].Experence.ToString();
        GameObject.Find("Attack").GetComponent<Text>().text = "攻击力：" + Propertys[ID - 1].Attack.ToString();
        GameObject.Find("Defend").GetComponent<Text>().text = "防御力：" + Propertys[ID - 1].Defend.ToString();
    }

    private void DonShowPet(int ID)
    {
        Pets[ID - 1].transform.position = new Vector3(0, 0, -10);
    }

    private void InitProperty(int id, int Level, int Experence, int Intimacy, int Hamlet, int Attack, int Defend)
    {
        Propertys[id].Level = Level;
        Propertys[id].Experence = Experence;
        Propertys[id].Intimacy = Intimacy;
        Propertys[id].Hamlet = Hamlet;
        Propertys[id].Attack = Attack;
        Propertys[id].Defend = Defend;
    }

    public void ShowStory()
    {
        Storys.SetActive(true);
        Bound.SetActive(false);
        Rebound.SetActive(true);
        ShowNowStory();
    }
    public void HideStory()
    {
        Storys.SetActive(false);
        Bound.SetActive(true);
        Rebound.SetActive(false);
        HideNowStory();
    }
    public void ShowNowStory()
    {
        Pets[curretPet - 1].SetActive(false);
        Story[curretPet-1].SetActive(true);
        Debug.Log(Story[curretPet - 1].name);
        Story[curretPet - 1].transform.Find("layout1").gameObject.SetActive(true);
        currentLayout = 1;
    }
    public void HideNowStory()
    {
        Pets[curretPet - 1].SetActive(true);
    }
    public void NextLayout()
    {
        int count = Story[curretPet - 1].GetComponent<Transform>().childCount;
        Debug.Log(count);
        Debug.Log(curretPet);
        if (currentLayout <= count)
        {
            Story[curretPet - 1].transform.Find("layout" + currentLayout.ToString()).gameObject.SetActive(false);
            currentLayout++;
            Story[curretPet - 1].transform.Find("layout" + currentLayout.ToString()).gameObject.SetActive(true);
        }
    }

    public void LastLayout()
    {
        if (currentLayout == 0)
        {
            return;
        }
        Story[curretPet - 1].transform.Find("layout" + currentLayout.ToString()).gameObject.SetActive(false);
        currentLayout--;
        Story[curretPet - 1].transform.Find("layout" + currentLayout.ToString()).gameObject.SetActive(true);
    }
}
