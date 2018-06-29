using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour {
    [SerializeField]
    private GameObject enter;

    private float alpha;
    private bool isAdd;
    private bool buttonIsAble;
	// Use this for initialization
	void Start () {
        alpha = 1.0f;
        isAdd = false;
        buttonIsAble = false;
        StartCoroutine(PlayBackground());
    }
	
	// Update is called once per frame
	void Update () {
		if (buttonIsAble)
        {
            if (isAdd)
            {
                enter.GetComponent<CanvasGroup>().alpha += 1f * Time.deltaTime;
                if (enter.GetComponent<CanvasGroup>().alpha >= 1.0f)
                {
                    isAdd = false;
                }
            }
            else
            {
                enter.GetComponent<CanvasGroup>().alpha -= 1f * Time.deltaTime;
                if (enter.GetComponent<CanvasGroup>().alpha <= 0f)
                {
                    isAdd = true;
                }
            }
        }
	}

    IEnumerator PlayBackground ()
    {
        yield return new WaitForSeconds(2);

        buttonIsAble = true;
        enter.GetComponent<CanvasGroup>().alpha = 1;
        enter.GetComponent<CanvasGroup>().interactable = true;
        enter.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void Enter()
    {
        SceneManager.LoadScene("Menu");
    }
}
