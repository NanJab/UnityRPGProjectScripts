using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenHead : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform targetTr; //이동될 UI의Transform

    private Vector2 beginPoint; // 드래그 시작 시의 UI 위치 저장
    private Vector2 moveBegin; // 드래그 시작 시의 마우스 위치 저장

    private void Awake()
    {
        // 타겟이 설정되지 않은 경우, 부모 객체로 설정
        if(targetTr == null)
        {
            targetTr = transform.parent;
        }
    }

    // 드래그 시작 시 호출되는 메서드 (마우스를 클릭했을 때)
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // UI의 현재 위치와 마우스 클릭 시작 지점 저장
        beginPoint = targetTr.position; // 인벤토리의 시작 위치 저장
        moveBegin = eventData.position; // 드래그 시작 시점의 마우스 위치 저장
    }

    // 드래그 중 호출되는 메서드 (마우스를 드래그할 때)
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // 드래그한 만큼 UI의 위치를 변경
        targetTr.position = beginPoint + (eventData.position - moveBegin);
        //                   시작 위치 +        마우스 이동 거리
    }
}
