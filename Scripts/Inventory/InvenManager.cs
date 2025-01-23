using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenManager : MonoBehaviour
{
    public List<InvenSlot> slots = new List<InvenSlot>(); // 빈 리스트를 초기화
    public GameObject slotParent;
    public bool menuCheck;

    private void Start()
    {
        foreach (Transform child in slotParent.transform)
        {
            InvenSlot slot = child.GetComponentInChildren<InvenSlot>();
            if (slot != null)
            {
                slots.Add(slot);
            }
        }
    }

    public bool AddPotionInventory(GameObject newItem)
    {
        foreach (InvenSlot slot in slots)
        {
            if (slot.item != null && slot.item.CompareTag("Potion") && slot.CanAddStack())
            {
                slot.itemCount++;
                slot.gameObject.GetComponentInChildren<Market>().potionAmount = slot.itemCount;
                return true;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(newItem);
                return true;
            }
        }

        Debug.Log("인벤토리가 가득찼습니다.");
        return false;
    }

    public bool Pluspotioninven(GameObject newitem, float count)
    {
        if (!newitem.CompareTag("Potion")) return false;

        float remainingcount = count;

        foreach (InvenSlot slot in slots)
        {
            if (slot.item != null && slot.item.CompareTag("Potion") && slot.CanAddStack())
            {
                float canadd = Mathf.Min(slot.max_Count - slot.itemCount, remainingcount);

                slot.itemCount += canadd; // 슬롯에 추가
                remainingcount -= canadd; // 남은 수량 감소
                slot.gameObject.GetComponentInChildren<Market>().potionAmount += canadd; // 정렬시 수량도 같이 옮기기 위해 각 포션 오브젝트의 수량도 수정
                if (remainingcount <= 0) return true;
            }
        }


        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(newitem);
                slots[i].itemCount = remainingcount;
                slots[i].gameObject.GetComponentInChildren<Market>().potionAmount = remainingcount;

                return true;
            }
        }

        return false;
    }

    public bool AddItemInventory(GameObject newItem)
    {        
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null) 
            {
                slots[i].AddItem(newItem);                

                return true;   
            }
        }
        Debug.Log("인벤토리가 가득찼습니다.");
        return false;
    }

    public void AddInventoryImage(GameObject newItem)
    {
        for(int i = 0; i < slots.Count; i++)
        {

            if (slots[i].transform.childCount == 0)
            {
                newItem.transform.SetParent(slots[i].transform);
                newItem.transform.localPosition = Vector3.zero;

                return;
            }
        }
    }

    public void SortInventory()
    {
        // 인벤토리 아이템만 수집 (장비창 제외)
        List<(Transform item, ItemWear itemWear, InvenSlot originSlot)> itemsToSort = new List<(Transform, ItemWear, InvenSlot)>();

        // 인벤토리 슬롯에서만 아이템 수집
        foreach (InvenSlot slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.transform.name.StartsWith("Inv"))
            {
                Transform item = slot.transform.GetChild(0);
                ItemWear itemWear = item.GetComponent<ItemWear>();
                if (itemWear != null)
                {
                    itemsToSort.Add((item, itemWear, slot));
                }
            }
        }

        // 정렬 기준: 1.equipType, 2.itemType
        // sort 메서드는 리스트 내의 항목 순서를 변경하는 것
        // Comparison<T> 델리게이트 -1 : a가 b보다 작다. 0 : a와 b가 같다. 1 : a가 b보다 크다.
        itemsToSort.Sort((a, b) =>
        {
            int itemTypeCompare = a.itemWear.itemType.CompareTo(b.itemWear.itemType);
            if (itemTypeCompare != 0)
            {
                // 아이템 타입이 같지 않을 경우 여기서 리스트 순서 결정
                return itemTypeCompare;
            }
            // 아이템 타입이 같을 경우 장비 타입으로 비교 후 리스트 순서 결정
            return a.itemWear.equipType.CompareTo(b.itemWear.equipType);
        });

        // itemToSort안에 순서대로 정렬 된 아이템들을 재배치
            // 인벤토리 슬롯에 아이템을 재배치

        int slotIndex = 0;
        foreach(var sortItem in itemsToSort)
        {
            sortItem.originSlot.itemCount = 0;
            sortItem.originSlot.item = null;
            if (slotIndex < slots.Count)
            {
                sortItem.item.SetParent(slots[slotIndex].transform);
                sortItem.item.localPosition = Vector3.zero;
                
                if(sortItem.item.CompareTag("Potion"))
                {
                    slots[slotIndex].itemCount = sortItem.item.GetComponent<Market>().potionAmount;
                }
                slotIndex++;
            }
            
        }

        
    }
}
