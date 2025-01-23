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
        if(other.tag == "Weapon") // 무기에 공격 당했을 때
        {
            health -= playerStat.att;
            Debug.Log(playerStat.att);
            if(hitSoundCheck)
            {
                // 죽는 애니메이션 중에 공격에 맞아도 피격소리가 나지 않게 설정
                soundManager.PlaySfx("Slice");
            }
        }
        else if(other.tag == "Skill1" && skillDmgCheck) // 스킬1에 공격 당했을 때
        {
            health -= playerStat.skillAtt * 0.5f;
            skillDmgCheck = false; // 공격데미지가 한번만 들어가도록
            Debug.Log("SkillHit" + health);
            skillDmgCheck = true; // 다음 공격이 통할 수 있게 초기화
        }
        else if(other.tag == "Skill2" && skillDmgCheck) // 스킬2에 공격 당했을 때
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
        else if(other.tag == "Skill3") // 스킬3에 공격 당했을 때
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
