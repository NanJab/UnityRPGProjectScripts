using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemSlotDrag_Drop : MonoBehaviour, IDropHandler
{
    public Quest quest;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // �巡�� ���� ������Ʈ
        GameObject draggedObject = eventData.pointerDrag;
        Debug.Log($"{draggedObject.name}");
        if (draggedObject != null)
        {
            // �巡�� ���� ������Ʈ�� �ִ� Ư�� ������Ʈ�� �����´�.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();
            ItemWear itemGrade = draggedObject.GetComponent<ItemWear>();

            // Ư�� ������Ʈ �����
            if (item != null && itemGrade.itemType == ItemType.Common)
            {
                Debug.Log("common�������� ����߽��ϴ�.");
                // �巡�� ���� ������Ʈ�� ��ġ�� ���Կ� ����
                draggedObject.transform.position = transform.position;
                quest.UpdateCurrentCount(1);
            }
            else
            {
                Debug.Log("�߸��� �������Դϴ�.");
            }
        }
    }
}

