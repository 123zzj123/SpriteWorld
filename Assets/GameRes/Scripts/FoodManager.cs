using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {
    public delegate void Feed();
    public static event Feed successToFeed;
    public static event Feed failToFeed;

    // Use this for initialization
    void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {
        //食物被扔出了场外
		if(transform.position.y < 1.0f)
        {
            failToFeed();
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "floor")
        {//撞到地面
            Debug.Log("hit floor");
            failToFeed();
        }
        if (other.gameObject.tag == "pet")
        {//撞到墙，重新生成巡逻路线
            Debug.Log("hit pet");
            successToFeed();
        }
    }
}
