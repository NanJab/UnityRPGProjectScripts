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
        // �巡�� ���� ������Ʈ
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // �巡�� ���� ������Ʈ�� �ִ� Ư�� ������Ʈ�� �����´�.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();

            // Ư�� ������Ʈ �����
            if (item != null)
            {
                Debug.Log("�������� ����߽��ϴ�.");
                // �巡�� ���� ������Ʈ�� ��ġ�� ���Կ� ����
                draggedObject.transform.SetParent(transform);
                draggedObject.transform.position = transform.position;
            }
        }
    }

}
