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
        // 드래그 중인 오브젝트
        GameObject draggedObject = eventData.pointerDrag;
        Debug.Log($"{draggedObject.name}");
        if (draggedObject != null)
        {
            // 드래그 중인 오브젝트에 있는 특정 컴포넌트를 가져온다.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();
            ItemWear itemGrade = draggedObject.GetComponent<ItemWear>();

            // 특정 컴포넌트 존재시
            if (item != null && itemGrade.itemType == ItemType.Common)
            {
                Debug.Log("common아이템을 드롭했습니다.");
                // 드래그 중인 오브젝트의 위치는 슬롯에 고정
                draggedObject.transform.position = transform.position;
                quest.UpdateCurrentCount(1);
            }
            else
            {
                Debug.Log("잘못된 아이템입니다.");
            }
        }
    }
}

