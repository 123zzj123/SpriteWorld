using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfflineFightManager : MonoBehaviour {
    [SerializeField]
    private Text TimeText;

    [SerializeField]
    private Text FightInfoText;

    [SerializeField]
    private Slider RemoteSlider;

    [SerializeField]
    private Slider LocalSlider;

    [SerializeField]
    private Image WinOrLossImage;

    [SerializeField]
    private Sprite SpriteWin;

    [SerializeField]
    private Sprite SpriteLose;

    private bool ReadyForFight = false;
    private float Timers = 10.0f;
    private int CurrentSkill = -1;
	private int originPerCount = SSDirector.Pet;
    private const int maxLifeValue = 3000;
    private const float PhysicalAttack = 500;
    private const float MagicAttack = 800;
    private const float HealValue = 300;
    private const float HenSaoQianJunAttack = 300;
    private const string skillName1 = "物理攻击";
    private const string skillName2 = "雷击";
    private const string skillName3 = "治疗术";
    private const string skillName4 = "横扫千军";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
     //   Debug.Log(SSDirector.Pet);
    }


    //计时
    //0-3 for 普通攻击/雷击/治疗术/横扫千军
    public void selectSkill(int index)
    {
        StartCoroutine(AudioManager.PlayMenuAudio("GameUIView"));
        switch (index)
        {
            case 0:
                FightInfoText.text = "我方使用：\"" + skillName1 + "\"";
                RemoteSlider.value -= PhysicalAttack / maxLifeValue;
                break;
            case 1:
                FightInfoText.text = "我方使用：\"" + skillName2 + "\"";
                RemoteSlider.value -= MagicAttack / maxLifeValue;
                break;
            case 2:
                FightInfoText.text = "我方使用：\"" + skillName3 + "\"";
                LocalSlider.value += HealValue / maxLifeValue;
                break;
            case 3:
                FightInfoText.text = "我方使用：\"" + skillName4 + "\"";
                RemoteSlider.value -= HenSaoQianJunAttack / maxLifeValue;
                break;
        }
        this.StartCoroutine("WaitToShowEnemyAttackInfo");   
        checkResult();
    }

    public IEnumerator WaitToShowEnemyAttackInfo()
    {
        yield return new WaitForSeconds(0.5f);
        enemyAttack();
        checkResult();
    }
    
    //敌方攻击
    void enemyAttack()
    {
        FightInfoText.text = "敌方使用：\"" + skillName1 + "\"";
        LocalSlider.value -= PhysicalAttack / maxLifeValue;
    }

    //检测战斗是否已经结束
    void checkResult()
    {
        if (LocalSlider.value <= 0)
        {
            WinOrLossImage.sprite = SpriteLose;
            WinOrLossImage.gameObject.SetActive(true);
        }
        else if (RemoteSlider.value <= 0)
        {
            WinOrLossImage.sprite = SpriteWin;
            WinOrLossImage.gameObject.SetActive(true);
            SSDirector.Pet = originPerCount + 1;
        }
    }

    //返回petMenu
    public void goToPermenu()
    {
        StartCoroutine(AudioManager.LoadingNextScene("Canvas", "PetMenu"));
    }
}
