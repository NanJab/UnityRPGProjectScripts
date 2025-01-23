using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatusSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;
    public GameObject rightHand;

    public GameObject Head;
    public GameObject Hair;
    public GameObject Hair02;
    public GameObject nakedBody;
    public GameObject basicGauntlets;
    public GameObject basicPants;
    public GameObject nakedFoot;

    public GameObject head;
    public GameObject chest;
    public GameObject leg;
    public GameObject weapon;

    Vector2 target;
    Vector2 mouseP;

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 중인 오브젝트
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // 드래그 중인 오브젝트에 있는 특정 컴포넌트를 가져온다.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();

            // 특정 컴포넌트 존재시
            if (item != null)
            {
                Debug.Log("아이템을 드롭했습니다.");
                // 드래그 중인 오브젝트의 위치는 슬롯에 고정
                draggedObject.transform.SetParent(transform);
                draggedObject.transform.position = transform.position;
            }
        }
    }

}
