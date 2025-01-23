using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public ItemType type;
    public PlayerStat stat;
    public UIImageCheck ui;

    private PointerEventData pointerEventData;    
    private EventSystem eventSystem;
    public TextMeshProUGUI countText;
    public GraphicRaycaster raycaster;
    public InvenManager invenManager;

    public GameObject parentSlot;
    public GameObject uI;
    //public GameObject useUI;
    public GameObject multipleUI;
    public GameObject currentItem;
    public GameObject countObject;

    public int potionPrice;
    public float potionAmount;
    public float resetTime;
    public float warningTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        countObject = transform.GetChild(0).gameObject;
        eventSystem = EventSystem.current;
        pointerEventData = new PointerEventData(eventSystem);
        invenManager = GameObject.Find("InventoryUI").GetComponent<InvenManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            parentSlot = transform.parent.gameObject;
        }

        UpdateCountText();

        if (Input.GetMouseButtonDown(1))
        {
            pointerEventData.position = Input.mousePosition; // 마우스의 현재 위치를 기준으로 이벤트 데이터를 생성 마우스 포인터의 위치를 가져옴

            List<RaycastResult> results = new List<RaycastResult>(); // Raycast 결과를 저장할 리스트 생성 (Raycast는 클릭한 위치에서 UI 요소를 찾는 역할을 함)        
            raycaster.Raycast(pointerEventData, results);

            bool clickCheck = false;

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Potion") && result.gameObject.transform.parent.CompareTag("Market"))
                {
                    currentItem = result.gameObject;
                    BuyPotion();
                    clickCheck = true;
                }
                else if (result.gameObject.CompareTag("Potion") && result.gameObject.transform.parent.CompareTag("Inven"))
                {
                    currentItem = result.gameObject;
                    UsePotion();
                    clickCheck= true;
                }
                
            }

            if (!clickCheck)
            {
                uI.SetActive(false);
                multipleUI.SetActive(false);
            }
                       
        }

        if (ui.warningUI.transform.position == ui.warningSpawnPosition)
        {
            if (Time.time > resetTime + warningTime)
            {
                TextMeshProUGUI text = ui.warningUI.GetComponentInChildren<TextMeshProUGUI>();
                ui.warningUI.transform.position = ui.warningOffPosition;
                text.text = "";
                resetTime = Time.time;
            }
        }

    }

    void BuyPotion()
    {
        //if(uI.activeSelf) { uI.SetActive(false); }

        uI.SetActive(true);

        RectTransform rectTransform = uI.GetComponent<RectTransform>(); // RectTransform을 가져와서 UI의 위치와 기준을 설정 (좌표와 피벗 설정)                
        rectTransform.pivot = new Vector2(0, 1); // 피벗을 왼쪽 상단으로 설정. 피벗은 UI의 위치를 결정할 때 기준이 되는 지점                
        rectTransform.position = Input.mousePosition; // UI를 마우스 위치에 나타나도록 설정

        Button buyButton = GameObject.Find("Buy").GetComponent<Button>(); // UI에서 버튼 컴포넌트를 가져옴
        buyButton.onClick.RemoveAllListeners(); // 버튼의 기존 모든 리스너 제거                
        buyButton.onClick.AddListener(ItemBuy); // 장비 버튼 클릭 시 ItemEquip함수를 실행되도록 이벤트 리스너 등록

        Button buyMultipleButton = GameObject.Find("BuyMultiple").GetComponent<Button>();
        buyMultipleButton.onClick.RemoveAllListeners();
        buyMultipleButton.onClick.AddListener(ItemBuyMultiple); // 여기를 아이템 해제로 바꾸기
    }


    void ItemBuy()
    {
        if(stat.money >= potionPrice)
        {
            // 포션 이미지 생성
            GameObject potionInstance = Instantiate(currentItem);
            // 포션 이미지 인벤토리 슬롯으로 이동
            bool added = invenManager.AddPotionInventory(potionInstance);

            if(added)
            {
                stat.money -= potionPrice;
                
                //potionAmount++;
                uI.SetActive(false);
            }

        }
        else
        {
            Debug.Log("골드가 부족함");
        }
    }

    void ItemBuyMultiple()
    {
        if (multipleUI.activeSelf) { multipleUI.SetActive(false); } 

        multipleUI.SetActive(true);

        RectTransform rectTransform = multipleUI.GetComponent<RectTransform>(); // RectTransform을 가져와서 UI의 위치와 기준을 설정 (좌표와 피벗 설정)                
        rectTransform.pivot = new Vector2(-0.2f, 0.7f);   
        rectTransform.position = Input.mousePosition;
    }

    public void UsePotion()
    {        
        InvenSlot count = parentSlot.GetComponent<InvenSlot>();

        if (stat.hp > stat.maxHp)
        {
            resetTime = Time.time;
            ui.warningUI.transform.position = ui.warningSpawnPosition;
            TextMeshProUGUI text = ui.warningUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "체력이 충분합니다.";
            return;
        }


        PotionApply();

        if(count.itemCount != 0)
        {
            count.itemCount--;
        }

        if (count.itemCount <= 0)
        {
            Destroy(count.item);
            count.item = null;
        }
        UpdateCountText();
    }

    public void UpdateCountText()
    {
        InvenSlot count = parentSlot.GetComponent<InvenSlot>();

        if (countText != null && parentSlot.CompareTag("Inven"))
        {
            countText.text = count.itemCount > 0 ? count.itemCount.ToString() : "";
            if (ui.invenOn)
            {
                countObject.SetActive(true);
            }
            else if (!ui.invenOn)
            {
                countObject.SetActive(false);
            }
        }
    }

    public void PotionApply()
    {
        stat.hp += 50;

        if (stat.hp > stat.maxHp)
        { 
            stat.hp = stat.maxHp;
        }
    }
}
