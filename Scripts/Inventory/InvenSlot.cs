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
        // �巡�� ���� ������Ʈ
        GameObject draggedObject = eventData.pointerDrag;

        if(draggedObject != null)
        {
            // �巡�� ���� ������Ʈ�� �ִ� Ư�� ������Ʈ�� �����´�.
            ItemDragDrop item = draggedObject.GetComponent<ItemDragDrop>();

            // Ư�� ������Ʈ �����
            if(item != null)
            {
                Debug.Log("�������� ����߽��ϴ�.");
                // �巡�� ���� ������Ʈ�� ��ġ�� ���Կ� ����
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
            // ������ �θ�� ��� ������ ��ġ�� ������ ����
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
