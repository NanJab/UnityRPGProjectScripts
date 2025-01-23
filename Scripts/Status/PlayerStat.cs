using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public BossAttack bossDmg;
    public ParticleSystem levelUp;
    public GameObject skill2;
    public GameObject skill3;
    
    public int level;
    public float hp;
    public float maxHp;
    public float mp;
    public float maxMp;
    public int att;
    public float skillAtt;
    public int def;
    public float money;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BossAttack")
        {
            if (bossDmg.dmg - def <= 0) return;
            hp -= (bossDmg.dmg - def);
        }
        else if(other.tag == "BossSkill_1")
        {
            if (bossDmg.skillDmg - def <= 0) return;
            hp -= (bossDmg.skillDmg - def);
        }
        else if(other.tag == "BossSkill_2")
        {
            if (bossDmg.skillDmg - def <= 0) return;
            hp -= (bossDmg.skillDmg * 0.3f - def);
        }
        else if(other.tag == "BossSkill_3")
        {
            if (bossDmg.skillDmg - def <= 0) return;
            hp -= (bossDmg.skillDmg * 1.5f - def);
        }
    }
    // 플레이어의 초기 스탯
    void Start()
    {
        level = 1;
        maxHp = 500;
        maxMp = 100;
        att = 50;
        skillAtt = 70;
        def = 0;

        hp = maxHp;
        mp = maxMp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        if(money <= 0)
        {
            money = 0;
        }
    }

    // 레벨업시 스탯 상승 수치
    public void LevelUp()
    {
        if (level >= 10)
        {
            level = 10;
            return;
        }

        levelUp.Play();
        level++;
        maxHp += 50;
        maxMp += 50;
        att += 20;
        skillAtt += 20;

        if (level == 2)
        {
            skill2.SetActive(true);
        }
        else if (level == 3)
        {
            skill3.SetActive(true);
        }


    }
}
