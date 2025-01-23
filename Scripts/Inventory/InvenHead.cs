using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenHead : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform targetTr; //�̵��� UI��Transform

    private Vector2 beginPoint; // �巡�� ���� ���� UI ��ġ ����
    private Vector2 moveBegin; // �巡�� ���� ���� ���콺 ��ġ ����

    private void Awake()
    {
        // Ÿ���� �������� ���� ���, �θ� ��ü�� ����
        if(targetTr == null)
        {
            targetTr = transform.parent;
        }
    }

    // �巡�� ���� �� ȣ��Ǵ� �޼��� (���콺�� Ŭ������ ��)
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // UI�� ���� ��ġ�� ���콺 Ŭ�� ���� ���� ����
        beginPoint = targetTr.position; // �κ��丮�� ���� ��ġ ����
        moveBegin = eventData.position; // �巡�� ���� ������ ���콺 ��ġ ����
    }

    // �巡�� �� ȣ��Ǵ� �޼��� (���콺�� �巡���� ��)
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // �巡���� ��ŭ UI�� ��ġ�� ����
        targetTr.position = beginPoint + (eventData.position - moveBegin);
        //                   ���� ��ġ +        ���콺 �̵� �Ÿ�
    }
}
