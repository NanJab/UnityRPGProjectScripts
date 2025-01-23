using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    float xMove;
    float yMove;
    public float distance;
    public GameObject player;
    Vector3 offset;

    void Start()
    {
        
    }
    
    void Update()
    {
        //if(!Cursor.visible)
        //{
        //    Rotate();
        //    CameraPlayerRotation();
        //}

        Rotate();
        
        CameraPlayerRotation();
    }

    void Rotate()
    {
        // ���콺�� �¿� �̵����� xmove�� �����Ͽ� Y�� ȸ�� ����
        xMove += Input.GetAxis("Mouse X");
        // ���콺�� ���� �̵����� ymove�� �����Ͽ� X�� ȸ�� ����
        yMove -= Input.GetAxis("Mouse Y"); 

        // yMove�� xMove ���� ����Ͽ� ī�޶� ȸ�� ����
        if(!Cursor.visible)
        {
            transform.rotation = Quaternion.Euler(yMove, xMove, 0);
        }
        // ī�޶� �÷��̾��� ��ġ���� ������ �Ÿ���ŭ �ڷ� ��ġ
        Vector3 reverseDistance = new Vector3(0.0f, -1.5f, distance);
        transform.position = player.transform.position - transform.rotation * reverseDistance;

        // �÷��̾ ���� ��� ��ġ ����
        if(!player)
        {
            transform.position = transform.position;
        }
    }

    void CameraPlayerRotation()
    {
        // �÷��̾��� Y�� ȸ���� ī�޶��� Y�� ȸ���� �����ϰ� �����մϴ�.
        Vector3 playerEulerAngles = player.transform.rotation.eulerAngles;
        playerEulerAngles.y = transform.rotation.eulerAngles.y;

        player.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
