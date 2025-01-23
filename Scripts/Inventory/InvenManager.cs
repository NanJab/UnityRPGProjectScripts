using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenManager : MonoBehaviour
{
    public List<InvenSlot> slots = new List<InvenSlot>(); // �� ����Ʈ�� �ʱ�ȭ
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

        Debug.Log("�κ��丮�� ����á���ϴ�.");
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

                slot.itemCount += canadd; // ���Կ� �߰�
                remainingcount -= canadd; // ���� ���� ����
                slot.gameObject.GetComponentInChildren<Market>().potionAmount += canadd; // ���Ľ� ������ ���� �ű�� ���� �� ���� ������Ʈ�� ������ ����
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
        Debug.Log("�κ��丮�� ����á���ϴ�.");
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
        // �κ��丮 �����۸� ���� (���â ����)
        List<(Transform item, ItemWear itemWear, InvenSlot originSlot)> itemsToSort = new List<(Transform, ItemWear, InvenSlot)>();

        // �κ��丮 ���Կ����� ������ ����
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

        // ���� ����: 1.equipType, 2.itemType
        // sort �޼���� ����Ʈ ���� �׸� ������ �����ϴ� ��
        // Comparison<T> ��������Ʈ -1 : a�� b���� �۴�. 0 : a�� b�� ����. 1 : a�� b���� ũ��.
        itemsToSort.Sort((a, b) =>
        {
            int itemTypeCompare = a.itemWear.itemType.CompareTo(b.itemWear.itemType);
            if (itemTypeCompare != 0)
            {
                // ������ Ÿ���� ���� ���� ��� ���⼭ ����Ʈ ���� ����
                return itemTypeCompare;
            }
            // ������ Ÿ���� ���� ��� ��� Ÿ������ �� �� ����Ʈ ���� ����
            return a.itemWear.equipType.CompareTo(b.itemWear.equipType);
        });

        // itemToSort�ȿ� ������� ���� �� �����۵��� ���ġ
            // �κ��丮 ���Կ� �������� ���ġ

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
