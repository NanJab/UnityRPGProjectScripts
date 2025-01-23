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
            pointerEventData.position = Input.mousePosition; // ���콺�� ���� ��ġ�� �������� �̺�Ʈ �����͸� ���� ���콺 �������� ��ġ�� ������

            List<RaycastResult> results = new List<RaycastResult>(); // Raycast ����� ������ ����Ʈ ���� (Raycast�� Ŭ���� ��ġ���� UI ��Ҹ� ã�� ������ ��)        
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

        RectTransform rectTransform = uI.GetComponent<RectTransform>(); // RectTransform�� �����ͼ� UI�� ��ġ�� ������ ���� (��ǥ�� �ǹ� ����)                
        rectTransform.pivot = new Vector2(0, 1); // �ǹ��� ���� ������� ����. �ǹ��� UI�� ��ġ�� ������ �� ������ �Ǵ� ����                
        rectTransform.position = Input.mousePosition; // UI�� ���콺 ��ġ�� ��Ÿ������ ����

        Button buyButton = GameObject.Find("Buy").GetComponent<Button>(); // UI���� ��ư ������Ʈ�� ������
        buyButton.onClick.RemoveAllListeners(); // ��ư�� ���� ��� ������ ����                
        buyButton.onClick.AddListener(ItemBuy); // ��� ��ư Ŭ�� �� ItemEquip�Լ��� ����ǵ��� �̺�Ʈ ������ ���

        Button buyMultipleButton = GameObject.Find("BuyMultiple").GetComponent<Button>();
        buyMultipleButton.onClick.RemoveAllListeners();
        buyMultipleButton.onClick.AddListener(ItemBuyMultiple); // ���⸦ ������ ������ �ٲٱ�
    }


    void ItemBuy()
    {
        if(stat.money >= potionPrice)
        {
            // ���� �̹��� ����
            GameObject potionInstance = Instantiate(currentItem);
            // ���� �̹��� �κ��丮 �������� �̵�
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
            Debug.Log("��尡 ������");
        }
    }

    void ItemBuyMultiple()
    {
        if (multipleUI.activeSelf) { multipleUI.SetActive(false); } 

        multipleUI.SetActive(true);

        RectTransform rectTransform = multipleUI.GetComponent<RectTransform>(); // RectTransform�� �����ͼ� UI�� ��ġ�� ������ ���� (��ǥ�� �ǹ� ����)                
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
            text.text = "ü���� ����մϴ�.";
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
