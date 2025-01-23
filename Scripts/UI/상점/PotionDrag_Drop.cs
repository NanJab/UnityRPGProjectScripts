using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionDrag_Drop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI itemCountText;

    public int potionCount;

    private CanvasGroup canvasGroup;
    private RectTransform rect;

    Vector2 resetPosition;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        resetPosition = transform.position;  
        itemCountText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("Market"))
        {
            Debug.Log("���� �������� �巡���� �� �����ϴ�.");
            return;
        }

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        itemCountText.enabled = false;

        resetPosition = transform.position;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("Market")) return;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("Market")) return;

        canvasGroup.alpha = 1f;
        itemCountText.enabled = true;
        canvasGroup.blocksRaycasts = true;

        // ���Կ� �������� ������ �� �θ�� ����
        if (eventData.pointerEnter && eventData.pointerEnter.GetComponent<InvenSlot>())
        {
            transform.SetParent(eventData.pointerEnter.transform);
            rect.anchoredPosition = Vector2.zero;
        }
        else
        {
            // ������ �ƴϸ� �ʱ� ��ġ�� ����
            transform.position = resetPosition;
        }
    }
}
