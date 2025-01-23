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
            pointerEventData.position = Input.mousePosition; // 마우스의 현재 위치를 기준으로 이벤트 데이터를 생성 마우스 포인터의 위치를 가져옴

            List<RaycastResult> results = new List<RaycastResult>(); // Raycast 결과를 저장할 리스트 생성 (Raycast는 클릭한 위치에서 UI 요소를 찾는 역할을 함)        
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
