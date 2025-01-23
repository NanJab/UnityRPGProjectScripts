using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public PlayerStat stat;
    [SerializeField]
    Image expBar;
    public int exp;
    public int requiredExp = 100;
    public float lerpSpeed;
    void Start()
    {
        exp = 0;
        expBar.fillAmount = 0;
    }

    void Update()
    {
        UpdateXpUi();

        // 최고 레벨에서 경험치 멈춤 처리
        if (stat.level == 10)
        {
            exp = 0;
        }
    }

    // 몬스터 처치시 경험치 획득
    public void ExpUp(int a)
    {
        exp += a;

        // 레벨업
        if(exp >= 100)
        {
            stat.LevelUp();
            exp = 0; // 경험치 초기화            
        }
        Debug.Log(exp);
        UpdateXpUi();
    }

    public void UpdateXpUi()
    {
        float xpFraction = (float)exp / requiredExp;
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, xpFraction, Time.deltaTime * lerpSpeed);
    }
}
