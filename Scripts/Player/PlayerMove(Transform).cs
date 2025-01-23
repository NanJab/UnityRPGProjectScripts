using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform mainCamera;
    public Rigidbody rb;
    Animator pAnimator;

    public int moveSpeed;
    public int runSpeed;

    public float turnSmoothVelocity;
    public float turnSpeed;

    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode Left;
    public KeyCode Right;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        // 벡터 초기화
        Vector3 inputVector = new Vector3(0, 0, 0);

        // Forward 키가 눌렸을 경우 z축으로 1만큼 이동
        if (Input.GetKey(Forward))
        {
            inputVector.z += 1f;
        }

        // Backr 키가 눌렸을 경우 z축으로 -1만큼 이동
        if (Input.GetKey(Back))
        {
            inputVector.z -= 1f;
        }

        // Left 키가 눌렸을 경우 x축으로 -1만큼 이동
        if(Input.GetKey(Left))
        {
            inputVector.x -= 1f;
        }

        // Right 키가 눌렸을 경우 x축으로 1만큼 이동
        if(Input.GetKey(Right))
        {
            inputVector.x += 1f;
        }

        // 입력 벡터를 정규화하여 이동 방향 벡터 생성
        // 이동 속도가 대각선 방향에서도 일정
        Vector3 moveDir = inputVector.normalized;

        // 캐릭터가 움직이고 있는지 확인하는 조건문
        // inputVector의 x와 z가 모두 0이 아니라면, 움직이고 있다는 뜻
        if(!(inputVector.x == 0 && inputVector.z == 0))
        {
            // 목표 회전 각도를 계산
            //                   현재 방향의 각도를 라디안으로 반환
            //                                                 라디안을 도 단위로 반환
            //                                                                     카메라 방향 기준으로 목표 각도 설정
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            
            // 현재 각도와 목표 각도 사이를 부드럽게 회전
            //                   첫 번째 값에서 두 번째 값으로 변화시키는 함수
            //                                                                           각도 변화 속도 조절
            //                                                                                                  회전 속도
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);

            // 목표 각도를 기준으로 회전 변환을 계산
            //                  y축을 기준으로 targetAngle만큼 회전
            //                                                          카메라 방향을 기준으로 이동 방향 설정
            Vector3 Direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // 캐릭터를 설정된 방향으로 이동
            transform.position += Direction * moveSpeed * Time.deltaTime;
            // 캐릭터가 이동 방향을 바라보게 함
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        bool isMoving = Mathf.Abs(inputVector.x) + Mathf.Abs(inputVector.z) > 0f;

        //pAnimator.SetBool("isMove", isMoving);


    }
}
