using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class MonsterMove : MonoBehaviour
{
    private SoundManager soundManager;
    private Transform player;
    private Rigidbody rigid;
    private Animator anime;
    public Quest quest;
    LevelUp level;

    public GameObject commonParticle;
    public GameObject moneyParticle;

    public float rotateSpeed;
    public float speed; // ������ �̵� �ӵ�
    public float moveSpeed; // �̵��� Ȱ��ȭ�� ���� �⺻ �ӵ�
    public float attackCoolTime;
    private float lastAttackTime = 0f;
    public int minView; // �÷��̾���� �ּ� �Ÿ� (�� �Ÿ����� ��������� ����)
    public int maxView; // �÷��̾���� �ִ� �Ÿ� (�� �Ÿ����� �־����� ������)
    public bool walkCheck;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        quest = GameObject.Find("QuestName_1").GetComponent<Quest>();
        level = GameObject.Find("Exp").GetComponent<LevelUp>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
        // �ʱ� �̵� �ӵ��� speed ������ ����
        moveSpeed = speed;
        attackCoolTime = 0.5f;
        walkCheck = true;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MonsterMoving();
    }

    void MonsterMoving()
    {
        if(player != null)
        {
            if (isAttacking) return;
            // ���� ��ġ���� �÷��̾� ��ġ������ �Ÿ� ����
            Vector3 dir = transform.position - player.position;

            // ���Ͱ� �÷��̾ �ٶ󺸴� ���� ���� (����ȭ)
            Vector3 monsterRotate = (player.position - transform.position).normalized;            

            // �Ÿ��� ũ�� (�÷��̾���� �Ÿ� ���)
            float dicts = Vector3.Magnitude(dir);

            // �÷��̾���� �Ÿ��� minView ������ ��
            if (dicts <= minView)
            {
                // �̵� ���¸� false�� ����
                walkCheck = false;
                // ���� ��Ÿ��
                if(Time.time >= lastAttackTime + attackCoolTime)
                {
                    anime.SetTrigger("attack");
                    lastAttackTime = Time.time;
                    isAttacking = true;
                }
                                
            }
            // �÷��̾���� �Ÿ��� maxView �ȿ� ���� ��
            else if (dicts <= maxView)
            {
                // �̵� ���¸� true�� ����
                walkCheck = true;
            }
            // maxView���� �ָ� ���� ��
            else if(dicts >= maxView)
            {
                // ����
                walkCheck = false;
            }

            // ���Ͱ� �ٶ󺸴� �������� �ӵ� �����Ͽ� �̵�            
            if (walkCheck)
            {
                rigid.velocity = transform.forward * speed;
                // �÷��̾ ���� ȸ��
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(monsterRotate), Time.deltaTime * rotateSpeed);
            }
            else if (!walkCheck)
            {
                rigid.velocity = Vector3.zero;
            }

            // �̵� �ִϸ��̼�
            anime.SetBool("isWalk", walkCheck);
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 basePosition = transform.position;

        float posX = basePosition.x + Random.Range(-2, 2);
        float posZ = basePosition.z + Random.Range(-2, 2);

        Vector3 spawnPos = new Vector3(posX, transform.position.y, posZ);
        return spawnPos;
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

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void DieCheck(int i)
    {
        if(i == 0)
        {
            isAttacking = false;
            walkCheck = false;
        }
        else if(i == 1)
        {
            Destroy(gameObject);
            quest.UpdateCurrentCount(1);
            level.ExpUp(25);
            MoneyInstantiate();
            GameObject commonItem = Instantiate(commonParticle, 
                transform.position, 
                Quaternion.Euler(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }
        
    }

    public void RtationYCheck(int i)
    {
        if(i == 0)
        {
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }
        else if(i == 1)
        {
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void Sound(int i)
    {
        if(i == 0)
        {
            soundManager.PlaySfx("Punch");
        }
    }
}
