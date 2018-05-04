using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBtnHandler : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject vbtn;
    public GameObject Message;

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("pressed");
        Message.SetActive(true);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("released");
        Message.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        vbtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}
}
