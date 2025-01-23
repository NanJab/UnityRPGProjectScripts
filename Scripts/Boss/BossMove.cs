using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    BossAttack attack;
    Boss bossStat;
    public Transform target;
    private Rigidbody rigid;
    private Animator anime;
    public float rotateSpeed;
    public float speed; // ������ �̵� �ӵ�
    public float moveSpeed; // �̵��� Ȱ��ȭ�� ���� �⺻ �ӵ�
    public float attackCoolTime;
    private float lastAttackTime = 0f;
    public int minView; // �÷��̾���� �ּ� �Ÿ� (�� �Ÿ����� ��������� ����)
    public int maxView; // �÷��̾���� �ִ� �Ÿ� (�� �Ÿ����� �־����� ������)
    public bool walkCheck;

    private Vector3 resetPosition;
    private Quaternion resetRotation;

    void Start()
    {
        resetRotation = transform.rotation;
        resetPosition = transform.position;
        bossStat = GetComponent<Boss>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
        // �ʱ� �̵� �ӵ��� speed ������ ����
        moveSpeed = speed;
        walkCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bossStat.playerCheck)
        {
            player = null;
        }
    }

    private void FixedUpdate()
    {
        MonsterMoving();
    }

    void MonsterMoving()
    {
        if (player != null)
        {
            if (attack.isAttacking) return;

            // ���� ��ġ���� �÷��̾� ��ġ������ �Ÿ� ����
            Vector3 dir = transform.position - player.position;
            float dicts = Vector3.Magnitude(dir);

            // �÷��̾ �ٶ󺸴� ���� ����
            Vector3 monsterRotate = (player.position - transform.position).normalized;

            // maxView �Ÿ� ���� �� �ʱ� ��ġ�� ���� ����
            if (dicts >= maxView)
            {
                // �ʱ� ��ġ�� ȸ�� �� �̵�
                Vector3 resetDir = (resetPosition - transform.position).normalized;
                               
                // �ʱ� ��ġ ��ó�� �����ϸ� ����
                if (Vector3.Distance(transform.position, resetPosition) <= 0.5f)
                {
                    walkCheck = false;
                    rigid.velocity = Vector3.zero;
                    // �ʱ� ��ġ�� �̵�
                    transform.position = resetPosition;
                    // �ʱ� �ٶ󺸴� �������� ȸ��
                    transform.rotation = resetRotation;
                    bossStat.health = bossStat.maxHealth;  // ü�� �ʱ�ȭ
                }
                else
                {
                    walkCheck = true;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(resetDir), Time.deltaTime * rotateSpeed);
                    rigid.velocity = transform.forward * moveSpeed;  // �ӵ��� �������� ����
                }
            }
            else
            {
                // �÷��̾ ���� ȸ��
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(monsterRotate), Time.deltaTime * rotateSpeed);

                // �̵� �ӵ� ����
                rigid.velocity = transform.forward * speed;

                if (walkCheck)
                {
                    speed = moveSpeed;
                }
                else
                {
                    speed = 0;
                }

                // �÷��̾���� �Ÿ��� ���� �ൿ ����
                if (dicts <= minView)
                {
                    walkCheck = false;
                    if (Time.time >= lastAttackTime + attackCoolTime)
                    {
                        attack.AttackAnimation();
                        lastAttackTime = Time.time;
                    }
                }
                else if (dicts <= maxView)
                {
                    walkCheck = true;
                }
            }

            // �ִϸ��̼� ������Ʈ
            anime.SetBool("isWalk", walkCheck);
        }

    }
}
