using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotOption : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private InvenManager invenManager;
    public int slotCount;

    private bool slotsCreated = false;

    private void Start()
    {
        invenManager = GameObject.Find("InventoryUI").GetComponent<InvenManager>();

        if(invenManager == null)
        {
            Debug.Log("인벤매니저가 설정안됨");
            return;
        }

        if (!slotsCreated)
        {
            CreateSlots();
            slotsCreated = true;
        }

    }
    private void OnEnable()
    {
        

    }

    private void CreateSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            InvenSlot slot = GetComponentInChildren<InvenSlot>();

            invenManager.slots.Add(slot);
        }
    }
}
