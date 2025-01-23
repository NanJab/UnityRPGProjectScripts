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

        // �⺻������ �⺻
        attacks.Add(0);

        if (stat.health <= stat.maxHealth * 0.75f) attacks.Add(1);     // ��ų1�� ü�� 75% �Ʒ��� �������� �߰�
        if (stat.health <= stat.maxHealth * 0.5f) attacks.Add(2); // ��ų2�� ü�� 50% �Ʒ��� �������� �߰�
        if (stat.health <= stat.maxHealth * 0.3f) attacks.Add(3); // ��ų3�� ü�� 30% �Ʒ��� �������� �߰�

        // ��ų�� ��ϵ� ������ŭ �������� ������ ���� ����
        int attackIndex = attacks[Random.Range(0, attacks.Count)];
        UseAttack(attackIndex);        
    }

    // attackIndex�� �ش� ��ȣ�� ������ �ִϸ��̼� ����
    private void UseAttack(int attackIndex)
    {
        switch(attackIndex)
        {
            case 0:
                anime.SetTrigger("attack"); // �⺻����
                break;
            case 1:
                anime.SetTrigger("Skill_1"); // ��ų1
                break;
            case 2:
                anime.SetTrigger("Skill_2"); // ��ų2
                break;
            case 3:
                anime.SetTrigger("Skill_3"); // ��ų3
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
