using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FeedManager : MonoBehaviour {

    private bool active_input = false;

    #region Public Var
    public GameObject Mid_Air_Stage;
    public GameObject FeedMenu;
    public AnchorInputListenerBehaviour ILB;
    public MidAirPositionerBehaviour MAPB;
    public GameObject[] Pets = new GameObject[5];
    #endregion

    private void Start()
    {
        Pets[SSDirector.CurrentPet-1].transform.parent = Mid_Air_Stage.transform;
        
    }

    // Update is called once per frame
    void Update () {
		if(!active_input && Input.GetMouseButtonDown(0))
        {
            active_input = true;
            ILB.enabled = false;
            FeedMenu.SetActive(true);
        }
	}
}
