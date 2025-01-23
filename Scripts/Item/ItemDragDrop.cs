using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CanvasGroup canvasGroup;
    Canvas uiCanvas;
    public GameObject UI;

    Vector2 resetPosition;

    void Start()
    {
        UI = GameObject.Find("ContextMenu");
        canvasGroup = GetComponent<CanvasGroup>();
        uiCanvas = GameObject.FindGameObjectWithTag("UI_Canvas").GetComponent<Canvas>();
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �����ϰ� ����
        canvasGroup.alpha = 0.6f;
        // ui�� ���԰��� ��ȣ�ۿ��� �������� �ʵ��� ����
        canvasGroup.blocksRaycasts = false;

        // �ʱ� ��ġ ����
        resetPosition = transform.position;

        // �κ��丮 ���Կ� ������ �ʰ� �θ� ���� canvas�� ����
        transform.SetParent(uiCanvas.transform);

        if (UI == null) return;
        // ������ ���� ui �巡�� ���۽� ���ֱ�
        UI.SetActive(false);
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        // �������� ���콺 ������ ������� ����
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = mousePosition;
    }

    // �巡�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        // ���� ������� ����
        canvasGroup.alpha = 1f;
        
        canvasGroup.blocksRaycasts = true;

        // ���Կ� �������� ������ �� �θ�� ����
        if(eventData.pointerEnter && eventData.pointerEnter.GetComponent<InvenSlot>())
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else if(eventData.pointerEnter && eventData.pointerEnter.GetComponent<QuestItemSlotDrag_Drop>())
        {
            transform.SetParent(eventData.pointerEnter.transform);
            
        }
        else
        {
            // ������ �ƴϸ� �ʱ� ��ġ�� ����
            transform.position = resetPosition;
        }
        
    }
}
