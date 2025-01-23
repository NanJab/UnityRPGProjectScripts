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

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 투명하게 설정
        canvasGroup.alpha = 0.6f;
        // ui가 슬롯과의 상호작용을 차단하지 않도록 설정
        canvasGroup.blocksRaycasts = false;

        // 초기 위치 저장
        resetPosition = transform.position;

        // 인벤토리 슬롯에 가리지 않게 부모를 상위 canvas로 설정
        transform.SetParent(uiCanvas.transform);

        if (UI == null) return;
        // 아이템 착용 ui 드래그 시작시 없애기
        UI.SetActive(false);
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        // 아이템이 마우스 포인터 따라오게 설정
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = mousePosition;
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        // 투명도 원래대로 설정
        canvasGroup.alpha = 1f;
        
        canvasGroup.blocksRaycasts = true;

        // 슬롯에 아이템이 들어왔을 때 부모로 설정
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
            // 슬롯이 아니면 초기 위치로 복귀
            transform.position = resetPosition;
        }
        
    }
}
