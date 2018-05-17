using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

    #region Private Var
    private static SSDirector instance;
    #endregion

    #region Public Var
    public static int music = 0;
    public static int Pet = 2;
    public static int CurrentPet = 1;
    #endregion

    #region Public Method
    public static SSDirector getInstance()
    {
        if(instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
    #endregion

}
