using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

public class FeedManager : MonoBehaviour {

    #region Public Var
    public GameObject FeedMenu;
    public GameObject food;
    public GameObject ScoreText;
    public GameObject TimeText;
    public GameObject[] Pets = new GameObject[5];
    public Transform[] targetPos = new Transform[5];
    private int curTarget;
    private float speed;
    private int curPet;
    private bool canFeed;
    private Vector3 foodInitSpeed;
    private Vector3 startPos;
    private Vector3 endPos;
    private int score;
    private Vector3 originFoodPos;
    private int totalTime;

    #endregion

     void Start()
    {
        curPet = SSDirector.CurrentPet - 1;
        Pets[curPet].transform.position = new Vector3(0, 2.7f, 0);
        curTarget = 2;
        speed = 0.5f;
        canFeed = true;
        score = 0;
        totalTime = 60;
        ScoreText.GetComponent<Text>().text = "分数: " + score.ToString();
        TimeText.GetComponent<Text>().text = "时间: " + totalTime.ToString();
        originFoodPos = food.transform.position;
        FoodManager.failToFeed += initFood;
        FoodManager.successToFeed += addScore;
        StartCoroutine("Timer", totalTime);
    }

    // Update is called once per frame
    void Update () {
        //检测宠物是否走到了目标位置，是则更换目的地。
        checkGetToTargetPosition();
        if(Input.GetMouseButtonDown(0) && canFeed)
        {
            canFeed = false;
            startPos = Input.mousePosition;
            Debug.Log("start: " + startPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            Debug.Log("end  " + endPos);
            foodInitSpeed = endPos - startPos;
            foodInitSpeed.z = foodInitSpeed.magnitude;
            foodInitSpeed = foodInitSpeed / 1000.0f * 4.0f;
            food.GetComponent<Rigidbody>().useGravity = true;
            food.GetComponent<Rigidbody>().velocity = foodInitSpeed;
        }
    }

    void addScore()
    {
        score++;
        Debug.Log(score);
        ScoreText.GetComponent<Text>().text = "分数: " + score.ToString();
    }

    void initFood()
    {
        food.GetComponent<Rigidbody>().useGravity = false;
        food.GetComponent<Rigidbody>().velocity = Vector3.zero;
        food.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        food.transform.position = originFoodPos;
    }

    void checkGetToTargetPosition()
    {
        if((Pets[curPet].transform.position - targetPos[curTarget].position).magnitude < 0.1f)
        {
            curTarget = Mathf.FloorToInt(Random.Range(1.0f, 4.99f));
        }
        Pets[curPet].transform.position = Vector3.MoveTowards(Pets[curPet].transform.position, targetPos[curTarget].position, Time.deltaTime * speed);
    }

    IEnumerator Timer(int totalTime)
    {
        while(totalTime > 0)
        {
            yield return new WaitForSeconds(1);
            totalTime--;
            TimeText.GetComponent<Text>().text = "时间: " + totalTime.ToString();
        }
        TimeText.GetComponent<Text>().text = "时间: 0";
        canFeed = false;
    }

    public void GoToPetMenu()
    {
        var config = VuforiaConfiguration.Instance;
        var dbConfig = config.DatabaseLoad;

        dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
        SceneManager.LoadScene("PetMenu");
    }
}
