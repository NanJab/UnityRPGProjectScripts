using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private BoxCollider attackBox;
    [SerializeField]
    private BoxCollider skill_1_Box;

    [SerializeField]
    private BoxCollider skill_2_Box;

    [SerializeField]
    private BoxCollider skill_3_Box;

    [SerializeField]
    private Animator anime;
    public Boss stat;

    public float dmg;
    public float skillDmg;
    public bool isAttacking;


    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    playerStat.hp -= dmg;
        //    Debug.Log("attack");
        //}
    }
    void Start()
    {
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        attackBox.enabled = false;
    }

    public void AttackAnimation()
    {
        List<int> attacks = new List<int>();

        // 기본공격은 기본
        attacks.Add(0);

        if (stat.health <= stat.maxHealth * 0.75f) attacks.Add(1);     // 스킬1은 체력 75% 아래로 내려가면 추가
        if (stat.health <= stat.maxHealth * 0.5f) attacks.Add(2); // 스킬2는 체력 50% 아래로 내려가면 추가
        if (stat.health <= stat.maxHealth * 0.3f) attacks.Add(3); // 스킬3은 체력 30% 아래로 내려가면 추가

        // 스킬에 등록된 갯수만큼 랜덤으로 돌려서 패턴 생성
        int attackIndex = attacks[Random.Range(0, attacks.Count)];
        UseAttack(attackIndex);        
    }

    // attackIndex에 해당 번호가 들어오면 애니메이션 실행
    private void UseAttack(int attackIndex)
    {
        switch(attackIndex)
        {
            case 0:
                anime.SetTrigger("attack"); // 기본공격
                break;
            case 1:
                anime.SetTrigger("Skill_1"); // 스킬1
                break;
            case 2:
                anime.SetTrigger("Skill_2"); // 스킬2
                break;
            case 3:
                anime.SetTrigger("Skill_3"); // 스킬3
                break;

        }
    }

    public void StartEndAttack(int a)
    {
        if (a == 0)
        {
            isAttacking = false;
        }
        else if (a == 1)
        {
            isAttacking = true;
        }
    }


    public void AttackColliderOnOff(int a)
    {
        if(a == 0)
        {
            attackBox.enabled = false;
        }
        else if (a == 1)
        {
            attackBox.enabled = true;
        }
    }

    public void Skill_1_ColliderOnOff(int a)
    {
        if (a == 0)
        {
            skill_1_Box.enabled = false;
        }
        else if (a == 1)
        {
            skill_1_Box.enabled = true;
        }
    }

    public void Skill_2_ColliderOnOff(int a)
    {
        if (a == 0)
        {
            skill_2_Box.enabled = false;
        }
        else if (a == 1)
        {
            skill_2_Box.enabled = true;
        }
    }

    public void Skill_3_ColliderOnOff(int a)
    {
        if (a == 0)
        {
            skill_3_Box.enabled = false;
        }
        else if (a == 1)
        {
            skill_3_Box.enabled = true;
        }
    }
}
