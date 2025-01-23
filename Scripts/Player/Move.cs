using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Animator pAnimator;
    Rigidbody rb;
    public float speed;
    public float currentSpeed;
    public Transform cameraTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        // 좌우 입력 값 가져오기
        float xMove = Input.GetAxis("Horizontal");
        // 앞뒤 입력 값 가져오기
        float zMove = Input.GetAxis("Vertical");

        // 카메라 기준으로 이동 방향 계산
        Vector3 movement = cameraTransform.right * xMove + cameraTransform.forward * zMove;
        // 수직 이동 방지
        movement.y = 0f;

        // 이동 입력이 있을 경우에만 처리
        if (movement.magnitude > 0f)
        {
            // 이동 벡터 정규화 후 속도 적용
            movement = movement.normalized * speed;            

            // 이동 방향으로 캐릭터 회전
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            // 부드럽게 회전 
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.15f));

            // 캐릭터 이동
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);            
        }
        else
        {
            rb.velocity= Vector3.zero;
        }


        // 각 대각선 방향에 대한 애니메이션 설정
        pAnimator.SetBool("isDiagonalForwardLeft", zMove > 0f && xMove < 0f);
        pAnimator.SetBool("isDiagonalForwardRight", zMove > 0f && xMove > 0f);
        pAnimator.SetBool("isDiagonalBackLeft", zMove < 0f && xMove < 0f);
        pAnimator.SetBool("isDiagonalBackRight", zMove < 0f && xMove > 0f);

        // 각 방향에 대한 애니메이션 설정
        pAnimator.SetBool("isForwardMove", zMove > 0f && xMove == 0f);
        pAnimator.SetBool("isBackMove", zMove < 0f && xMove == 0f);
        pAnimator.SetBool("isLeftWalk", xMove < 0f && zMove == 0f);
        pAnimator.SetBool("isRightWalk", xMove > 0f && zMove == 0f);
    }

    void MoveControl(int controlNum)
    {
        if (controlNum == 0)
        {
            speed = 0;
        }
        else
        {
            speed = currentSpeed;

        }
    }
}
