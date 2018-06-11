using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextType : MonoBehaviour
{
    #region Public Attributes 
    public Text m_tweenText;
    [Range(1, 30)]
    public float m_speed = 1.0f;
    public AudioClip m_audio;
    #endregion

    #region Private Attributes 
    private bool m_canTween = false; // 是否可以开始动画
    private string m_totalStr;
    private float m_timeChange = 0.0f;
    #endregion

    #region Private Methods
    private void BeginTextShow()
    {
        m_totalStr = m_tweenText.text;
        m_tweenText.text = "";
        m_timeChange = 0f;
        //增加音频组件
        m_tweenText.gameObject.AddComponent<AudioSource>().clip = m_audio;
        m_tweenText.GetComponent<AudioSource>().loop = true;
        // 播放音频
        m_tweenText.GetComponent<AudioSource>().Play();
        // 开始动画
        m_canTween = true;
    }

    private void TextTweenFinish()
    {
        m_canTween = false;
        m_tweenText.text = m_totalStr;
        m_timeChange = 0.0f;
        m_tweenText.GetComponent<AudioSource>().Stop();
        DestroyObject(m_tweenText.GetComponent<AudioSource>());
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        BeginTextShow();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果可以开始动画，则开始动画
        if (m_canTween)
        {
            // 判断文本是否应该结束动画
            if (m_tweenText.text.Equals(m_totalStr))
            {
                // 结束
                TextTweenFinish();
            }
            else
            {
                // 未结束
                m_tweenText.text = m_totalStr.Substring(0, (int)(m_speed * m_timeChange));
                m_timeChange += Time.deltaTime;
            }
        }
    }
}