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
        maxHpText.text = "최대체력 : " + playerStat.maxHp;
        maxMpText.text = "최대마나 : " + playerStat.maxMp;
        levelText.text = "레벨 : " + playerStat.level;
        attackText.text = "공격력 : " + playerStat.att;
        skillAttackText.text = "스킬 공격력 : " + playerStat.skillAtt;
        defenseText.text = "방어력 : " + playerStat.def;
    }
}
