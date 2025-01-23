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

        // �ְ� �������� ����ġ ���� ó��
        if (stat.level == 10)
        {
            exp = 0;
        }
    }

    // ���� óġ�� ����ġ ȹ��
    public void ExpUp(int a)
    {
        exp += a;

        // ������
        if(exp >= 100)
        {
            stat.LevelUp();
            exp = 0; // ����ġ �ʱ�ȭ            
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
