using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionUse : MonoBehaviour
{
    private PointerEventData pointerEventData;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    public GameObject currentItem;
    public Button useButton;
    public Button cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        pointerEventData = new PointerEventData(eventSystem);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pointerEventData.position = Input.mousePosition; // ���콺�� ���� ��ġ�� �������� �̺�Ʈ �����͸� ���� ���콺 �������� ��ġ�� ������

            List<RaycastResult> results = new List<RaycastResult>(); // Raycast ����� ������ ����Ʈ ���� (Raycast�� Ŭ���� ��ġ���� UI ��Ҹ� ã�� ������ ��)        
            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {              
                if (result.gameObject.CompareTag("Potion") && result.gameObject.transform.parent.CompareTag("Inven"))
                {
                    currentItem = result.gameObject;
                }
            }
        }
    }
}
