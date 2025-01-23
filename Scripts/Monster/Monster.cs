using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator anime;
    public SoundManager soundManager;
    public Quest quest;
    public PlayerStat playerStat;
    public GameObject rightHand;
    LevelUp level;
    public bool skillDmgCheck;
    public bool hitSoundCheck;
    public float health;
    public float maxHealth;
    public int questCount;
    int weaponDmg;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon") // ���⿡ ���� ������ ��
        {
            health -= playerStat.att;
            Debug.Log(playerStat.att);
            if(hitSoundCheck)
            {
                // �״� �ִϸ��̼� �߿� ���ݿ� �¾Ƶ� �ǰݼҸ��� ���� �ʰ� ����
                soundManager.PlaySfx("Slice");
            }
        }
        else if(other.tag == "Skill1" && skillDmgCheck) // ��ų1�� ���� ������ ��
        {
            health -= playerStat.skillAtt * 0.5f;
            skillDmgCheck = false; // ���ݵ������� �ѹ��� ������
            Debug.Log("SkillHit" + health);
            skillDmgCheck = true; // ���� ������ ���� �� �ְ� �ʱ�ȭ
        }
        else if(other.tag == "Skill2" && skillDmgCheck) // ��ų2�� ���� ������ ��
        {
            health -= playerStat.skillAtt * 0.8f;
            if (hitSoundCheck)
            {
                soundManager.PlaySfx("Skill2");
            }
            skillDmgCheck = false;
            Debug.Log("Skill_Hit2" + health);
            skillDmgCheck = true;
        }
        else if(other.tag == "Skill3") // ��ų3�� ���� ������ ��
        {
            anime.SetTrigger("hit");
            health -= playerStat.skillAtt * 1.2f;
            Debug.Log("Skill3_Hit" + health );
        }
    }
    void Start()
    {
        hitSoundCheck = true;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        quest = GameObject.Find("QuestName_1").GetComponent<Quest>();
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        level = GameObject.Find("Exp").GetComponent<LevelUp>();
        maxHealth = health;
        skillDmgCheck = true;
    }

    void Update()
    {
        if (health <= 0)
        {
            anime.SetTrigger("die");
        }

    }

    public void HitSoundOff()
    {
        hitSoundCheck = false;
    }

}
