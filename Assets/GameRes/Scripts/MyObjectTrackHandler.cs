using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyObjectTrackHandler : DefaultTrackableEventHandler
{

    override protected void Start()
    {
        base.Start();
    }
    #region PROTECTED_METHODS

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        Debug.Log("found");
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
    }

    #endregion
}
