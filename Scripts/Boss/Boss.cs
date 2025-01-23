using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public PlayerStat playerStat;
    public SoundManager soundManager;

    LevelUp level;
    BossMove move;
    Animator anime;
    Rigidbody rigid;
    public GameObject commonParticle;
    public GameObject rareParticle;
    public GameObject moneyParticle;
    public bool skillDmgCheck;
    public bool playerCheck;
    public bool hitSoundCheck;

    public float health;
    public float maxHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon") // 무기에 공격 당했을 때
        {
            health -= playerStat.att;
            if(hitSoundCheck)
            {
                soundManager.PlaySfx("Slice");
            }
            Debug.Log(playerStat.att);
        }
        else if (other.tag == "Skill1" && skillDmgCheck) // 스킬1에 공격 당했을 때
        {
            health -= playerStat.skillAtt * 0.5f;
            skillDmgCheck = false; // 공격데미지가 한번만 들어가도록
            Debug.Log("SkillHit" + health);

            skillDmgCheck = true; // 다음 공격이 통할 수 있게 초기화
        }
        else if (other.tag == "Skill2" && skillDmgCheck) // 스킬2에 공격 당했을 때
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
        else if (other.tag == "Skill3") // 스킬3에 공격 당했을 때
        {
            health -= playerStat.skillAtt * 1.2f;
            Debug.Log("Skill3_Hit" + health);
        }
    }
    void Start()
    {
        hitSoundCheck = true;
        playerCheck = true;
        rigid = GetComponent<Rigidbody>();
        move = GetComponent<BossMove>();
        anime = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        level = GameObject.Find("Exp").GetComponent<LevelUp>();
        maxHealth = health;
        skillDmgCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            anime.SetTrigger("die");
            
        }

    }

    private Vector3 RandomPosition()
    {
        Vector3 basePosition = transform.position;

        float posX = basePosition.x + Random.Range(-2, 2);
        float posY = basePosition.y + Random.Range(-2, 2);

        Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);
        return spawnPos;
    }

    public void DieCheck(int i)
    {
        if(i == 0)
        {
            playerCheck = false;
            move.walkCheck = false;
            rigid.velocity = Vector3.zero;
            move.rotateSpeed = 0;
        }
        else if(i == 1)
        {
            level.ExpUp(25);
            // 몬스터 제거
            Destroy(gameObject);
            int a = Random.Range(0, 4);

            for(int j = 0; j < a; j++)
            {
                GameObject rareItem = Instantiate(rareParticle, transform.position, Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

            }

            MoneyInstantiate();
        }
        
    }

    void MoneyInstantiate()
    {
        int moneyCount = Random.Range(0, 4);

        for (int i = 0; i < moneyCount; i++)
        {
            Vector3 spawnPos = RandomPosition();

            GameObject instance = Instantiate(moneyParticle, spawnPos, Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }
    }
    public void HitSoundOff()
    {
        hitSoundCheck = false;
    }
}
