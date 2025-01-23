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
        // 마우스의 좌우 이동량을 xmove에 누적하여 Y축 회전 조정
        xMove += Input.GetAxis("Mouse X");
        // 마우스의 상하 이동량을 ymove에 누적하여 X축 회전 조정
        yMove -= Input.GetAxis("Mouse Y"); 

        // yMove와 xMove 값을 사용하여 카메라 회전 설정
        if(!Cursor.visible)
        {
            transform.rotation = Quaternion.Euler(yMove, xMove, 0);
        }
        // 카메라를 플레이어의 위치에서 일정한 거리만큼 뒤로 배치
        Vector3 reverseDistance = new Vector3(0.0f, -1.5f, distance);
        transform.position = player.transform.position - transform.rotation * reverseDistance;

        // 플레이어가 없을 경우 위치 고정
        if(!player)
        {
            transform.position = transform.position;
        }
    }

    void CameraPlayerRotation()
    {
        // 플레이어의 Y축 회전을 카메라의 Y축 회전과 동일하게 설정합니다.
        Vector3 playerEulerAngles = player.transform.rotation.eulerAngles;
        playerEulerAngles.y = transform.rotation.eulerAngles.y;

        player.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
