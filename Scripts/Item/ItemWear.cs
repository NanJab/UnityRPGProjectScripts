using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // EventSystem 관련 기능 (PointerEventData, Raycasting 등)을 사용하기 위한 네임스페이스
using System;
using UnityEngine.Rendering;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using System.Security.Cryptography;
using TMPro;

public class ItemWear : MonoBehaviour
{
    [Header("Type")]
    public EquipType equipType;
    public ItemType itemType;
    [Header("weapon")]
    public GameObject basicWeapon;
    public GameObject commonWeapon;
    public GameObject rareWeapon;

    [Header("Particle")]
    public GameObject commonParticle;
    public GameObject rareParticle;
    public GameObject Basic1_1;
    public GameObject Basic2_1;

    [Header("Stat")]
    public string equipName;
    public int Att;
    public int skillAtt;
    public int Def;

    [Header("RandomStat")]
    public int minAtt;
    public int maxAtt;
    public int minSkillAtt;
    public int maxSkillAtt;
    public int minDef;
    public int maxDef;
    // ui오브젝트
    [Header("other")]
    private PlayerStat stat;
    private GameObject UI;
    private GameObject player;
    private GameObject rightHand;    
    private GameObject invenManager;
    private BlackSmith_Text BlackSmith;
    // 현재 선택된 아이템을 저장할 변수
    public GameObject currentItem;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    GameObject chest;
    GameObject helmet;
    GameObject leg;
    GameObject weapon;

    private Dictionary<string, Action> equipActions;
    private Dictionary<string, Action> disarmActions;
    void Start()
    {
        Att = UnityEngine.Random.Range(minAtt, maxAtt + 1);
        skillAtt = UnityEngine.Random.Range(minSkillAtt, maxSkillAtt + 1);
        Def = UnityEngine.Random.Range(minDef, maxDef + 1);

        basicWeapon = GameObject.Find("BasicSword");
        commonWeapon = GameObject.Find("CommonSword");
        rareWeapon = GameObject.Find("RareSword");

        commonParticle = GameObject.Find("Common");
        rareParticle = GameObject.Find("Rare");
        Basic1_1 = GameObject.Find("Basic1_1");
        Basic2_1 = GameObject.Find("Basic2_1");

        invenManager = GameObject.Find("InventoryUI");
        BlackSmith = GameObject.FindWithTag("BlackSmithUI").GetComponent<BlackSmith_Text>();
        UI = GameObject.Find("ContextMenu");

        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        rightHand = GameObject.Find("PT_RightHandThumb3");
        // 현재 활성화된 EventSystem을 가져옴. EventSystem은 UI에서 발생하는 이벤트를 관리
        eventSystem = EventSystem.current;
        // 현재 화면에 있는 UI의 GraphicRaycaster 컴포넌트를 가져옴
        // 이를 통해 UI 요소에 대한 Raycast 처리를 할 수 있음
        raycaster = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(eventSystem); // PointerEventData는 UI에서 발생한 이벤트 정보를 저장하는 클래스      
        player = GameObject.Find("Player");

        chest = GameObject.Find("SlotChest");
        helmet = GameObject.Find("SlotHead");
        leg = GameObject.Find("SlotShoes");
        weapon = GameObject.Find("SlotWeapon");

        // 아이템 정보를 저장
        equipActions = new Dictionary<string, Action>
        {
            { "BasicChest(Clone)",  () => EquipArmors(chest, new List<string>{ "NakedBody", "CommonChest", "CommonCape", "CommonGauntlets", "RareGauntlets", "RareCape", "RareBody" },
            new List<string>{ "BasicBody" }) },

            { "BasicWeapon(Clone)", () => EquipWeapon(basicWeapon) },

            { "CommonChest(Clone)", () => EquipArmors(chest, new List<string>{ "NakedBody", "BasicGauntlets", "BasicBody" }, 
            new List<string>{ "CommonChest", "CommonCape", "CommonGauntlets" }) },

            { "CommonHelmet(Clone)", () => EquipArmors(helmet, new List<string>{ "Hair", "Hair02", "RareHelmet" }, 
            new List<string>{ "CommonHelmet" }) },

            { "CommonLeg(Clone)", () => EquipArmors(leg, new List<string>{ "NakedFoot", "BasicPants", "RareLeg", "RareBoots" }, 
            new List<string>{ "CommonLeg", "CommonBoots" }) },

            { "CommonWeapon(Clone)", () => EquipWeapon(commonWeapon) },

            { "RareChest(Clone)", () => EquipArmors(chest, new List<string>{ "NakedBody", "BasicGauntlets", "BasicBody" }, 
            new List<string>{ "RareGauntlets", "RareCape", "RareBody" }) },

            { "RareHelmet(Clone)", () => EquipArmors(helmet, new List<string>{ "Hair", "Hair02", "CommonHelmet" }, 
            new List<string>{ "RareHelmet" }) },

            { "RareLeg(Clone)", () => EquipArmors(leg, new List<string>{ "NakedFoot", "BasicPants", "CommonLeg", "CommonBoots" }, 
            new List<string>{ "RareLeg", "RareBoots" }) },

            { "RareWeapon(Clone)", () => EquipWeapon(rareWeapon) }
        };

        // 장비해체 딕셔너리                 반환값이 없는 경우 action 델리게이트 사용
        disarmActions = new Dictionary<string, Action>
        {
            //                     람다식으로 시작시 내용들이 바로실행되지 않게 지연 원하는 경우에만 실행
            { "BasicChest(Clone)", () => DisarmArmors(chest, new List<string>{ "BasicBody", "CommonChest", "CommonCape", "CommonGauntlets", "RareGauntlets", "RareCape", "RareBody"  }, 
            new List<string>{ "NakedBody" }) },

            { "BasicWeapon(Clone)", () => DisarmWeapon() },

            { "CommonChest(Clone)", () => DisarmArmors(chest, new List<string>{ "CommonChest", "CommonCape", "CommonGauntlets", "BasicBody" }, new List<string>{ "NakedBody", "BasicGauntlets" }) },

            { "CommonHelmet(Clone)", () => DisarmArmors(helmet, new List<string>{ "CommonHelmet" }, new List<string>{ "Hair", "Hair02" }) },

            { "CommonLeg(Clone)", () => DisarmArmors(leg, new List<string>{ "CommonLeg", "CommonBoots" }, new List<string>{ "NakedFoot", "BasicPants" }) },

            { "CommonWeapon(Clone)", () => DisarmWeapon() },

            { "RareChest(Clone)", () => DisarmArmors(chest, new List<string>{ "RareGauntlets", "RareCape", "RareBody", "BasicBody" }, new List<string>{ "NakedBody", "BasicGauntlets" }) },

            { "RareHelmet(Clone)", () => DisarmArmors(helmet, new List<string>{ "RareHelmet" }, new List<string>{ "Hair", "Hair02" }) },

            { "RareLeg(Clone)", () => DisarmArmors(leg, new List<string>{ "RareLeg", "RareBoots" }, new List<string>{ "NakedFoot", "BasicPants" }) },

            { "RareWeapon(Clone)", () => DisarmWeapon() },
        };

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {           
            OnOff();            
        }
       
        // 현재 아이템이 존재하고, ui가 존재, ui가 활성화된 상태일 경우
        if(currentItem != null && UI != null && UI.activeSelf)
        {
            FollowItem();
        }


    }

    // 장비장착 및 해제 ui가 현재 아이템을 따라다니도록 설정
    void FollowItem()
    {
        RectTransform itemRect = currentItem.GetComponent<RectTransform>();
        RectTransform textRect = UI.GetComponent<RectTransform>();

        Vector3 itemPosition = itemRect.position;
        // 아이템 메뉴를 아이템의 위치로 따라다니게 만듦
        textRect.position = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z); 

        UI.transform.SetAsLastSibling();
    }

    void OnOff()
    {
        if(UI == null)
        {
            return;
        }

        pointerEventData.position = Input.mousePosition; // 마우스의 현재 위치를 기준으로 이벤트 데이터를 생성 마우스 포인터의 위치를 가져옴
                                                          
        List<RaycastResult> results = new List<RaycastResult>(); // Raycast 결과를 저장할 리스트 생성 (Raycast는 클릭한 위치에서 UI 요소를 찾는 역할을 함)        
        raycaster.Raycast(pointerEventData, results); // raycaster.Raycast는 마우스 포인터 위치에서 UI 요소들을 확인하고, 그 결과를 results 리스트에 저장

        bool itemClick = false;

        foreach (RaycastResult result in results) // Raycast 결과에서 클릭된 UI 요소들을 순회
        {
            // 만약 클릭된 UI 요소의 태그가 "Item"으로 설정된 경우
            if (result.gameObject.CompareTag("Item"))
            {
                currentItem = result.gameObject; // 클릭된 아이템 저장

                UI.SetActive(true);

                itemClick = true;

                RectTransform rectTransform = UI.GetComponent<RectTransform>(); // RectTransform을 가져와서 UI의 위치와 기준을 설정 (좌표와 피벗 설정)                
                rectTransform.pivot = new Vector2(0, 1); // 피벗을 왼쪽 상단으로 설정. 피벗은 UI의 위치를 결정할 때 기준이 되는 지점                
                rectTransform.position = Input.mousePosition; // UI를 마우스 위치에 나타나도록 설정

                Button equipButton = GameObject.Find("EquipButton").GetComponent<Button>(); // UI에서 버튼 컴포넌트를 가져옴
                equipButton.onClick.RemoveAllListeners(); // 버튼의 기존 모든 리스너 제거                
                equipButton.onClick.AddListener(ItemEquip); // 장비 버튼 클릭 시 ItemEquip함수를 실행되도록 이벤트 리스너 등록

                Button disarmButton = GameObject.Find("DisarmButton").GetComponent<Button>();
                disarmButton.onClick.RemoveAllListeners();
                disarmButton.onClick.AddListener(ItemDisarm); // 여기를 아이템 해제로 바꾸기

                Button sellButton = GameObject.Find("Sell").GetComponent<Button>();
                sellButton.onClick.RemoveAllListeners();
                sellButton.onClick.AddListener(ItemSell);

                
            }

            if (!itemClick)
            {
                UI.SetActive(false);
            }
        }

        
    }

    // 아이템을 장비창에 장착하는 역할
    void ItemEquip()
    {
        if(IsInInventory(currentItem))
        {
            ItemAction(equipActions);
        }
        else
        {
            Debug.Log("장비 없음");
        }
    }

    // 장비창에서 아이템을 해제하는 역할
    void ItemDisarm()
    {
        if(IsInEquipmentSlot(currentItem))
        {
            ItemAction(disarmActions);
        }
        else
        {
            Debug.Log("장비해제할 장비 없음");
        }
    }

    void ItemSell()
    {
        if(BlackSmith.market.transform.position == BlackSmith.marketSpawnPos)
        {
            if(currentItem.GetComponent<ItemWear>().itemType == ItemType.Basic1)
            {
                Destroy(currentItem);
                stat.money += 500;
                UI.SetActive(false);
            }
            else if(currentItem.GetComponent<ItemWear>().itemType == ItemType.Basic2)
            {
                Destroy(currentItem);
                stat.money += 500;
                UI.SetActive(false);
            }
            else if(currentItem.GetComponent<ItemWear>().itemType == ItemType.Common)
            {
                Destroy(currentItem);
                stat.money += 1000;
                UI.SetActive(false);
            }
            else if(currentItem.GetComponent<ItemWear>().itemType == ItemType.Rare)
            {
                Destroy(currentItem);
                stat.money += 2000;
                UI.SetActive(false);
            }
        }
    }

    bool IsInInventory(GameObject item)
    {
        // 인벤토리의 부모 오브젝트를 확인
        return item.transform.parent.name.StartsWith("Inv");
    }

    bool IsInEquipmentSlot(GameObject item)
    {
        // 장비 슬롯의 부모 오브젝트를 확인
        return item.transform.parent.name.StartsWith("Slot");
    }

    // 딕셔너리에 저장된 정보를 찾아서 실행
    void ItemAction(Dictionary<string, Action> item)
    {
        if(currentItem != null)
        {
            // 현재 아이템의 이름과 같은 내용의 정보를 찾아서 실행
            string itemName = currentItem.name;

            if (item.ContainsKey(itemName))
            {
                item[itemName].Invoke();
            }
            else
            {
                Debug.Log("장착된 아이템이 없습니다.");
            }
        }
    }

    void EquipArmors(GameObject slots, List<string> objectDisable, List<string> objectAble)
    {
        // 장착되어 있는 장비를 인벤토리로 이동
        if (slots.transform.childCount > 0)
        {
            // 장착버튼 ui 비활성화
            UI.SetActive(false);

            // 현재 아이템을 장비슬롯에 장착
            currentItem.transform.SetParent(slots.transform);
            currentItem.transform.localPosition = Vector3.zero;

            // 기존에 장비된 아이템은 현재 아이템의 인벤토리 슬롯으로 이동
            GameObject slotItem = slots.transform.GetChild(0).gameObject;
            invenManager.GetComponent<InvenManager>().AddInventoryImage(slotItem);

        }
        else
        {
            // 장착버튼 ui 비활성화
            UI.SetActive(false);
            // 현재 아이템을 장비슬롯에 장착
            currentItem.transform.SetParent(slots.transform);
            currentItem.transform.localPosition = Vector3.zero;
        }

        // 현재 아이템의 스탯들을 플레이어 스탯에 추가
        if (currentItem != null)
        {
            int itemDmg = currentItem.GetComponent<ItemWear>().Att;
            int itemSkillDmg = currentItem.GetComponent<ItemWear>().skillAtt;
            int itemDef = currentItem.GetComponent<ItemWear>().Def;
            stat.att += itemDmg;
            stat.skillAtt += itemSkillDmg;
            stat.def += itemDef;
        }
        currentItem.GetComponent<ItemDragDrop>().enabled = false;

        // 현재 아이템에 해당하지 않는 장비 비활성화
        foreach (string objDisable in objectDisable)
        {
            GameObject obj = player.transform.Find(objDisable).gameObject;
            obj.SetActive(false);
        }

        // 현재 아이템에 해당하는 장비 활성화
        foreach(string objAble in objectAble)
        {
            GameObject obj = player.transform.Find(objAble).gameObject;
            obj.SetActive(true);
        }
    }

    // 아이템 장비해체
    void DisarmArmors(GameObject slot, List<string> objectDisable, List<string> objectAble)
    {
        // 장비한 아이템이 있을경우에만 해제
        if(slot.transform.childCount > 0)
        {
            invenManager.GetComponent<InvenManager>().AddInventoryImage(currentItem);

            UI.SetActive(false);
        }

        currentItem.GetComponent<ItemDragDrop>().enabled = true;

        // 현재 아이템의 스탯들을 플레이어 스탯에서 감소
        if (currentItem != null)
        {
            int itemDmg = currentItem.GetComponent<ItemWear>().Att;
            int itemSkillDmg = currentItem.GetComponent<ItemWear>().skillAtt;
            int itemDef = currentItem.GetComponent<ItemWear>().Def;
            stat.att -= itemDmg;
            stat.skillAtt -= itemSkillDmg;
            stat.def -= itemDef;
        }

        // 해제한 아이템에 해당하는 장비 해제
        foreach (string objDisable in objectDisable)
        {
            GameObject obj = player.transform.Find(objDisable).gameObject;
            obj.SetActive(false);
        }

        // 해제 후 기본 아이템 장비
        foreach (string objAble in objectAble)
        {
            GameObject obj = player.transform.Find(objAble).gameObject;
            obj.SetActive(true);
        }
    }

    // 무기 장착
    void EquipWeapon(GameObject itemWeapon)
    {       
        // 현재 무기 장비
        EquipArmors(weapon, new List<string>(), new List<string>());        

        // 무기 오브젝트 생성
        InstantiateWeapon(itemWeapon);      
    }

    // 무기 장착해제
    void DisarmWeapon()
    {
        // 무기 장비해제
        DisarmArmors(weapon, new List<string>(), new List<string>());

        // 무기 오브젝트 삭제
        foreach (Transform hand in rightHand.transform)
        {
            Destroy(hand.gameObject);
        }
    }

    // 무기 생성
    void InstantiateWeapon(GameObject weapon)
    {
        // 기존에 장착된 무기 삭제
        foreach (Transform child in rightHand.transform)
        {
            Destroy(child.gameObject);
        }
        
        if(weapon != null)
        {
            // 무기 생성
            GameObject instantiateWeapon = Instantiate(weapon, rightHand.transform.position, Quaternion.identity);
            // 무기를 오른손의 자식으로 설정
            instantiateWeapon.transform.SetParent(rightHand.transform);
            // 무기 위치 설정
            instantiateWeapon.transform.localPosition = new Vector3(0, 0, 0.1f);
            instantiateWeapon.transform.localRotation = Quaternion.Euler(new Vector3(56, -148, 20));
        }
        else
        {
            Debug.Log("무기 없음");
        }                
        currentItem.transform.localPosition = Vector3.zero;
    }
}
