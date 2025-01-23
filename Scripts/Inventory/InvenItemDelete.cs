using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenItemDelete : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;

        if(draggedObject.CompareTag("Item"))
        {
            Destroy(draggedObject);
        }
    }
}
