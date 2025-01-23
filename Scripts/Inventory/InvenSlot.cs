using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour, IDropHandler 
{
    public GameObject item;
    [SerializeField]
    public float itemCount = 0;
    public int max_Count = 99;

    private void Update()
    {        
        if(transform.childCount == 0)
        {
            item = null;
            return;
        }
        else
        {
            item = transform.GetChild(0).gameObject;
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 중인 오브젝트
        GameObject draggedObject = eventData.pointerDrag;

        if(draggedObject != null)
        {
            // 드래그 중인 오브젝트에 있는 특정 컴포넌트를 가져온다.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();

            // 특정 컴포넌트 존재시
            if(item != null)
            {
                Debug.Log("아이템을 드롭했습니다.");
                // 드래그 중인 오브젝트의 위치는 슬롯에 고정
                draggedObject.transform.position = transform.position;
            }
        }
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public void AddItem(GameObject newItem)
    {
        if(newItem.CompareTag("Potion"))
        {
            if(item == null)
            {
                item = newItem;
                itemCount = 1;
                item.GetComponent<Market>().potionAmount = itemCount;

                item.transform.SetParent(transform);
                Vector2 pos = new Vector2(0, 0);
                item.transform.localPosition = pos;
            }
            else
            {
                itemCount++;
            }
        }
        else
        {
            if (item != null)
            {
                return;
            }
            Vector2 pos = new Vector2(0, 0);
            item = newItem;
            // 슬롯을 부모로 삼고 슬롯의 위치로 아이템 생성
            Instantiate(newItem, transform.position, Quaternion.identity, transform);
            newItem.transform.localPosition = pos;
        }        
    }

    public bool CanAddStack()
    {
        return itemCount < max_Count;
    }

    public void AddStack()
    {
        if (itemCount < max_Count)
        {
            itemCount++;
        }
    }  
}
