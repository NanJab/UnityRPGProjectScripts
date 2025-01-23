using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // EventSystem ���� ��� (PointerEventData, Raycasting ��)�� ����ϱ� ���� ���ӽ����̽�
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
    // ui������Ʈ
    [Header("other")]
    private PlayerStat stat;
    private GameObject UI;
    private GameObject player;
    private GameObject rightHand;    
    private GameObject invenManager;
    private BlackSmith_Text BlackSmith;
    // ���� ���õ� �������� ������ ����
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
        // ���� Ȱ��ȭ�� EventSystem�� ������. EventSystem�� UI���� �߻��ϴ� �̺�Ʈ�� ����
        eventSystem = EventSystem.current;
        // ���� ȭ�鿡 �ִ� UI�� GraphicRaycaster ������Ʈ�� ������
        // �̸� ���� UI ��ҿ� ���� Raycast ó���� �� �� ����
        raycaster = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(eventSystem); // PointerEventData�� UI���� �߻��� �̺�Ʈ ������ �����ϴ� Ŭ����      
        player = GameObject.Find("Player");

        chest = GameObject.Find("SlotChest");
        helmet = GameObject.Find("SlotHead");
        leg = GameObject.Find("SlotShoes");
        weapon = GameObject.Find("SlotWeapon");

        // ������ ������ ����
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

        // �����ü ��ųʸ�                 ��ȯ���� ���� ��� action ��������Ʈ ���
        disarmActions = new Dictionary<string, Action>
        {
            //                     ���ٽ����� ���۽� ������� �ٷν������ �ʰ� ���� ���ϴ� ��쿡�� ����
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
       
        // ���� �������� �����ϰ�, ui�� ����, ui�� Ȱ��ȭ�� ������ ���
        if(currentItem != null && UI != null && UI.activeSelf)
        {
            FollowItem();
        }


    }

    // ������� �� ���� ui�� ���� �������� ����ٴϵ��� ����
    void FollowItem()
    {
        RectTransform itemRect = currentItem.GetComponent<RectTransform>();
        RectTransform textRect = UI.GetComponent<RectTransform>();

        Vector3 itemPosition = itemRect.position;
        // ������ �޴��� �������� ��ġ�� ����ٴϰ� ����
        textRect.position = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z); 

        UI.transform.SetAsLastSibling();
    }

    void OnOff()
    {
        if(UI == null)
        {
            return;
        }

        pointerEventData.position = Input.mousePosition; // ���콺�� ���� ��ġ�� �������� �̺�Ʈ �����͸� ���� ���콺 �������� ��ġ�� ������
                                                          
        List<RaycastResult> results = new List<RaycastResult>(); // Raycast ����� ������ ����Ʈ ���� (Raycast�� Ŭ���� ��ġ���� UI ��Ҹ� ã�� ������ ��)        
        raycaster.Raycast(pointerEventData, results); // raycaster.Raycast�� ���콺 ������ ��ġ���� UI ��ҵ��� Ȯ���ϰ�, �� ����� results ����Ʈ�� ����

        bool itemClick = false;

        foreach (RaycastResult result in results) // Raycast ������� Ŭ���� UI ��ҵ��� ��ȸ
        {
            // ���� Ŭ���� UI ����� �±װ� "Item"���� ������ ���
            if (result.gameObject.CompareTag("Item"))
            {
                currentItem = result.gameObject; // Ŭ���� ������ ����

                UI.SetActive(true);

                itemClick = true;

                RectTransform rectTransform = UI.GetComponent<RectTransform>(); // RectTransform�� �����ͼ� UI�� ��ġ�� ������ ���� (��ǥ�� �ǹ� ����)                
                rectTransform.pivot = new Vector2(0, 1); // �ǹ��� ���� ������� ����. �ǹ��� UI�� ��ġ�� ������ �� ������ �Ǵ� ����                
                rectTransform.position = Input.mousePosition; // UI�� ���콺 ��ġ�� ��Ÿ������ ����

                Button equipButton = GameObject.Find("EquipButton").GetComponent<Button>(); // UI���� ��ư ������Ʈ�� ������
                equipButton.onClick.RemoveAllListeners(); // ��ư�� ���� ��� ������ ����                
                equipButton.onClick.AddListener(ItemEquip); // ��� ��ư Ŭ�� �� ItemEquip�Լ��� ����ǵ��� �̺�Ʈ ������ ���

                Button disarmButton = GameObject.Find("DisarmButton").GetComponent<Button>();
                disarmButton.onClick.RemoveAllListeners();
                disarmButton.onClick.AddListener(ItemDisarm); // ���⸦ ������ ������ �ٲٱ�

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

    // �������� ���â�� �����ϴ� ����
    void ItemEquip()
    {
        if(IsInInventory(currentItem))
        {
            ItemAction(equipActions);
        }
        else
        {
            Debug.Log("��� ����");
        }
    }

    // ���â���� �������� �����ϴ� ����
    void ItemDisarm()
    {
        if(IsInEquipmentSlot(currentItem))
        {
            ItemAction(disarmActions);
        }
        else
        {
            Debug.Log("��������� ��� ����");
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
        // �κ��丮�� �θ� ������Ʈ�� Ȯ��
        return item.transform.parent.name.StartsWith("Inv");
    }

    bool IsInEquipmentSlot(GameObject item)
    {
        // ��� ������ �θ� ������Ʈ�� Ȯ��
        return item.transform.parent.name.StartsWith("Slot");
    }

    // ��ųʸ��� ����� ������ ã�Ƽ� ����
    void ItemAction(Dictionary<string, Action> item)
    {
        if(currentItem != null)
        {
            // ���� �������� �̸��� ���� ������ ������ ã�Ƽ� ����
            string itemName = currentItem.name;

            if (item.ContainsKey(itemName))
            {
                item[itemName].Invoke();
            }
            else
            {
                Debug.Log("������ �������� �����ϴ�.");
            }
        }
    }

    void EquipArmors(GameObject slots, List<string> objectDisable, List<string> objectAble)
    {
        // �����Ǿ� �ִ� ��� �κ��丮�� �̵�
        if (slots.transform.childCount > 0)
        {
            // ������ư ui ��Ȱ��ȭ
            UI.SetActive(false);

            // ���� �������� ��񽽷Կ� ����
            currentItem.transform.SetParent(slots.transform);
            currentItem.transform.localPosition = Vector3.zero;

            // ������ ���� �������� ���� �������� �κ��丮 �������� �̵�
            GameObject slotItem = slots.transform.GetChild(0).gameObject;
            invenManager.GetComponent<InvenManager>().AddInventoryImage(slotItem);

        }
        else
        {
            // ������ư ui ��Ȱ��ȭ
            UI.SetActive(false);
            // ���� �������� ��񽽷Կ� ����
            currentItem.transform.SetParent(slots.transform);
            currentItem.transform.localPosition = Vector3.zero;
        }

        // ���� �������� ���ȵ��� �÷��̾� ���ȿ� �߰�
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

        // ���� �����ۿ� �ش����� �ʴ� ��� ��Ȱ��ȭ
        foreach (string objDisable in objectDisable)
        {
            GameObject obj = player.transform.Find(objDisable).gameObject;
            obj.SetActive(false);
        }

        // ���� �����ۿ� �ش��ϴ� ��� Ȱ��ȭ
        foreach(string objAble in objectAble)
        {
            GameObject obj = player.transform.Find(objAble).gameObject;
            obj.SetActive(true);
        }
    }

    // ������ �����ü
    void DisarmArmors(GameObject slot, List<string> objectDisable, List<string> objectAble)
    {
        // ����� �������� ������쿡�� ����
        if(slot.transform.childCount > 0)
        {
            invenManager.GetComponent<InvenManager>().AddInventoryImage(currentItem);

            UI.SetActive(false);
        }

        currentItem.GetComponent<ItemDragDrop>().enabled = true;

        // ���� �������� ���ȵ��� �÷��̾� ���ȿ��� ����
        if (currentItem != null)
        {
            int itemDmg = currentItem.GetComponent<ItemWear>().Att;
            int itemSkillDmg = currentItem.GetComponent<ItemWear>().skillAtt;
            int itemDef = currentItem.GetComponent<ItemWear>().Def;
            stat.att -= itemDmg;
            stat.skillAtt -= itemSkillDmg;
            stat.def -= itemDef;
        }

        // ������ �����ۿ� �ش��ϴ� ��� ����
        foreach (string objDisable in objectDisable)
        {
            GameObject obj = player.transform.Find(objDisable).gameObject;
            obj.SetActive(false);
        }

        // ���� �� �⺻ ������ ���
        foreach (string objAble in objectAble)
        {
            GameObject obj = player.transform.Find(objAble).gameObject;
            obj.SetActive(true);
        }
    }

    // ���� ����
    void EquipWeapon(GameObject itemWeapon)
    {       
        // ���� ���� ���
        EquipArmors(weapon, new List<string>(), new List<string>());        

        // ���� ������Ʈ ����
        InstantiateWeapon(itemWeapon);      
    }

    // ���� ��������
    void DisarmWeapon()
    {
        // ���� �������
        DisarmArmors(weapon, new List<string>(), new List<string>());

        // ���� ������Ʈ ����
        foreach (Transform hand in rightHand.transform)
        {
            Destroy(hand.gameObject);
        }
    }

    // ���� ����
    void InstantiateWeapon(GameObject weapon)
    {
        // ������ ������ ���� ����
        foreach (Transform child in rightHand.transform)
        {
            Destroy(child.gameObject);
        }
        
        if(weapon != null)
        {
            // ���� ����
            GameObject instantiateWeapon = Instantiate(weapon, rightHand.transform.position, Quaternion.identity);
            // ���⸦ �������� �ڽ����� ����
            instantiateWeapon.transform.SetParent(rightHand.transform);
            // ���� ��ġ ����
            instantiateWeapon.transform.localPosition = new Vector3(0, 0, 0.1f);
            instantiateWeapon.transform.localRotation = Quaternion.Euler(new Vector3(56, -148, 20));
        }
        else
        {
            Debug.Log("���� ����");
        }                
        currentItem.transform.localPosition = Vector3.zero;
    }
}
