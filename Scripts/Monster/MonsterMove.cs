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
    public float speed; // 몬스터의 이동 속도
    public float moveSpeed; // 이동이 활성화될 때의 기본 속도
    public float attackCoolTime;
    private float lastAttackTime = 0f;
    public int minView; // 플레이어와의 최소 거리 (이 거리보다 가까워지면 멈춤)
    public int maxView; // 플레이어와의 최대 거리 (이 거리보다 멀어지면 움직임)
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
        // 초기 이동 속도를 speed 값으로 설정
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
            // 몬스터 위치에서 플레이어 위치까지의 거리 벡터
            Vector3 dir = transform.position - player.position;

            // 몬스터가 플레이어를 바라보는 방향 벡터 (정규화)
            Vector3 monsterRotate = (player.position - transform.position).normalized;            

            // 거리의 크기 (플레이어와의 거리 계산)
            float dicts = Vector3.Magnitude(dir);

            // 플레이어와의 거리가 minView 이하일 때
            if (dicts <= minView)
            {
                // 이동 상태를 false로 설정
                walkCheck = false;
                // 공격 쿨타임
                if(Time.time >= lastAttackTime + attackCoolTime)
                {
                    anime.SetTrigger("attack");
                    lastAttackTime = Time.time;
                    isAttacking = true;
                }
                                
            }
            // 플레이어와의 거리가 maxView 안에 있을 때
            else if (dicts <= maxView)
            {
                // 이동 상태를 true로 설정
                walkCheck = true;
            }
            // maxView보다 멀리 있을 때
            else if(dicts >= maxView)
            {
                // 정지
                walkCheck = false;
            }

            // 몬스터가 바라보는 방향으로 속도 설정하여 이동            
            if (walkCheck)
            {
                rigid.velocity = transform.forward * speed;
                // 플레이어를 향해 회전
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(monsterRotate), Time.deltaTime * rotateSpeed);
            }
            else if (!walkCheck)
            {
                rigid.velocity = Vector3.zero;
            }

            // 이동 애니메이션
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
