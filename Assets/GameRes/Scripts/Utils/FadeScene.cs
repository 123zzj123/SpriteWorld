﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScene : MonoBehaviour {
    public Texture blackTexture;
    private float alpha = 1f;
    public float fadeSpeed = 0.5f;
    private int fadeDir = -1;

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }

    public float BeginFade(int direction)
    {
        alpha = (direction == -1) ? 1 : 0;
        fadeDir = direction;

        return 1 / fadeSpeed;
    }

    public void OnLevelWasLoaLed()
    {
        BeginFade(-1);
    }
}
