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
    public float speed; // 몬스터의 이동 속도
    public float moveSpeed; // 이동이 활성화될 때의 기본 속도
    public float attackCoolTime;
    private float lastAttackTime = 0f;
    public int minView; // 플레이어와의 최소 거리 (이 거리보다 가까워지면 멈춤)
    public int maxView; // 플레이어와의 최대 거리 (이 거리보다 멀어지면 움직임)
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
        // 초기 이동 속도를 speed 값으로 설정
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

            // 몬스터 위치에서 플레이어 위치까지의 거리 벡터
            Vector3 dir = transform.position - player.position;
            float dicts = Vector3.Magnitude(dir);

            // 플레이어를 바라보는 방향 벡터
            Vector3 monsterRotate = (player.position - transform.position).normalized;

            // maxView 거리 밖일 때 초기 위치로 복귀 로직
            if (dicts >= maxView)
            {
                // 초기 위치로 회전 및 이동
                Vector3 resetDir = (resetPosition - transform.position).normalized;
                               
                // 초기 위치 근처에 도착하면 멈춤
                if (Vector3.Distance(transform.position, resetPosition) <= 0.5f)
                {
                    walkCheck = false;
                    rigid.velocity = Vector3.zero;
                    // 초기 위치로 이동
                    transform.position = resetPosition;
                    // 초기 바라보는 방향으로 회전
                    transform.rotation = resetRotation;
                    bossStat.health = bossStat.maxHealth;  // 체력 초기화
                }
                else
                {
                    walkCheck = true;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(resetDir), Time.deltaTime * rotateSpeed);
                    rigid.velocity = transform.forward * moveSpeed;  // 속도를 절반으로 줄임
                }
            }
            else
            {
                // 플레이어를 향해 회전
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(monsterRotate), Time.deltaTime * rotateSpeed);

                // 이동 속도 설정
                rigid.velocity = transform.forward * speed;

                if (walkCheck)
                {
                    speed = moveSpeed;
                }
                else
                {
                    speed = 0;
                }

                // 플레이어와의 거리에 따른 행동 설정
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

            // 애니메이션 업데이트
            anime.SetBool("isWalk", walkCheck);
        }

    }
}
