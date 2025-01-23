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
        // ���� �ʱ�ȭ
        Vector3 inputVector = new Vector3(0, 0, 0);

        // Forward Ű�� ������ ��� z������ 1��ŭ �̵�
        if (Input.GetKey(Forward))
        {
            inputVector.z += 1f;
        }

        // Backr Ű�� ������ ��� z������ -1��ŭ �̵�
        if (Input.GetKey(Back))
        {
            inputVector.z -= 1f;
        }

        // Left Ű�� ������ ��� x������ -1��ŭ �̵�
        if(Input.GetKey(Left))
        {
            inputVector.x -= 1f;
        }

        // Right Ű�� ������ ��� x������ 1��ŭ �̵�
        if(Input.GetKey(Right))
        {
            inputVector.x += 1f;
        }

        // �Է� ���͸� ����ȭ�Ͽ� �̵� ���� ���� ����
        // �̵� �ӵ��� �밢�� ���⿡���� ����
        Vector3 moveDir = inputVector.normalized;

        // ĳ���Ͱ� �����̰� �ִ��� Ȯ���ϴ� ���ǹ�
        // inputVector�� x�� z�� ��� 0�� �ƴ϶��, �����̰� �ִٴ� ��
        if(!(inputVector.x == 0 && inputVector.z == 0))
        {
            // ��ǥ ȸ�� ������ ���
            //                   ���� ������ ������ �������� ��ȯ
            //                                                 ������ �� ������ ��ȯ
            //                                                                     ī�޶� ���� �������� ��ǥ ���� ����
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            
            // ���� ������ ��ǥ ���� ���̸� �ε巴�� ȸ��
            //                   ù ��° ������ �� ��° ������ ��ȭ��Ű�� �Լ�
            //                                                                           ���� ��ȭ �ӵ� ����
            //                                                                                                  ȸ�� �ӵ�
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);

            // ��ǥ ������ �������� ȸ�� ��ȯ�� ���
            //                  y���� �������� targetAngle��ŭ ȸ��
            //                                                          ī�޶� ������ �������� �̵� ���� ����
            Vector3 Direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // ĳ���͸� ������ �������� �̵�
            transform.position += Direction * moveSpeed * Time.deltaTime;
            // ĳ���Ͱ� �̵� ������ �ٶ󺸰� ��
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        bool isMoving = Mathf.Abs(inputVector.x) + Mathf.Abs(inputVector.z) > 0f;

        //pAnimator.SetBool("isMove", isMoving);


    }
}
