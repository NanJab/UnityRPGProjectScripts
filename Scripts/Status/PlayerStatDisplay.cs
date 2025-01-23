using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    public PlayerStat playerStat;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI maxMpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI skillAttackText;
    public TextMeshProUGUI defenseText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxHpText.text = "�ִ�ü�� : " + playerStat.maxHp;
        maxMpText.text = "�ִ븶�� : " + playerStat.maxMp;
        levelText.text = "���� : " + playerStat.level;
        attackText.text = "���ݷ� : " + playerStat.att;
        skillAttackText.text = "��ų ���ݷ� : " + playerStat.skillAtt;
        defenseText.text = "���� : " + playerStat.def;
    }
}
