using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLightBeam : MonoBehaviour
{
    public float collectRadius; // 아이템을 획득할 수 있는 반경
    public GameObject player;
    public InvenManager invenManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invenManager = GameObject.Find("InventoryUI").GetComponent<InvenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {            
            // 플레이어와 빛기둥 사이의 거리 계산
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // 플레이어가 정해진 반경 안에 들어오면 아이템 획득
            if(distance <= collectRadius && this.name == "Common(Clone)")
            {
                CollectCommonItem();
            }
            else if(distance <= collectRadius && this.name == "Rare(Clone)")
            {
                CollectRareItem();
            }
            else if(distance <= collectRadius && this.name == "Basic1")
            {
                CollectBasicItem(0);
            }
            else if(distance <= collectRadius && this.name == "Basic2")
            {
                CollectBasicItem(1);
            }
            else if(distance <= collectRadius && this.name == "Basic1_1(Clone)")
            {
                CollectBasicItem(0);
            }
            else if(distance <= collectRadius && this.name == "Basic2_1(Clone)")
            {
                CollectBasicItem(1);
            }
        }
    }

    void CollectRareItem()
    {
        int r = Random.Range(0, 4);

        bool itemAdded = invenManager.AddItemInventory(UIManager.Instance.rareItems[r]);

        if(itemAdded)
        {
            // 빛기둥 제거
            Destroy(gameObject);
        }
        else
        {
            //아이템 추가 실패, 빛줄기 유지
        }
        // 아이템 획득 로직 
        // 예: 인벤토리에 아이템 추가
        Debug.Log("레어 아이템 획득");
    }

    void CollectCommonItem()
    {
        int r = Random.Range(0, 4);

        bool itemAdded = invenManager.AddItemInventory(UIManager.Instance.commonItems[r]);

        if (itemAdded)
        {
            // 빛기둥 제거
            Destroy(gameObject);
        }
        else
        {
            //아이템 추가 실패, 빛줄기 유지
        }
        // 아이템 획득 로직 
        // 예: 인벤토리에 아이템 추가
        Debug.Log("보통 아이템 획득");
    }

    void CollectBasicItem(int i)
    {
        bool itemAdded = invenManager.AddItemInventory(UIManager.Instance.basicItems[i]);

        if (itemAdded)
        {
            // 빛기둥 제거
            Destroy(gameObject);
        }
        else
        {
            //아이템 추가 실패, 빛줄기 유지
        }
        // 아이템 획득 로직 
        // 예: 인벤토리에 아이템 추가
        Debug.Log("기본 아이템 획득");
    }

    
}
