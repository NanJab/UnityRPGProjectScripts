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
        // �¿� �Է� �� ��������
        float xMove = Input.GetAxis("Horizontal");
        // �յ� �Է� �� ��������
        float zMove = Input.GetAxis("Vertical");

        // ī�޶� �������� �̵� ���� ���
        Vector3 movement = cameraTransform.right * xMove + cameraTransform.forward * zMove;
        // ���� �̵� ����
        movement.y = 0f;

        // �̵� �Է��� ���� ��쿡�� ó��
        if (movement.magnitude > 0f)
        {
            // �̵� ���� ����ȭ �� �ӵ� ����
            movement = movement.normalized * speed;            

            // �̵� �������� ĳ���� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            // �ε巴�� ȸ�� 
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.15f));

            // ĳ���� �̵�
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);            
        }
        else
        {
            rb.velocity= Vector3.zero;
        }


        // �� �밢�� ���⿡ ���� �ִϸ��̼� ����
        pAnimator.SetBool("isDiagonalForwardLeft", zMove > 0f && xMove < 0f);
        pAnimator.SetBool("isDiagonalForwardRight", zMove > 0f && xMove > 0f);
        pAnimator.SetBool("isDiagonalBackLeft", zMove < 0f && xMove < 0f);
        pAnimator.SetBool("isDiagonalBackRight", zMove < 0f && xMove > 0f);

        // �� ���⿡ ���� �ִϸ��̼� ����
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
