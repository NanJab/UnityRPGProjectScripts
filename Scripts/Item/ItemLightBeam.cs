using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLightBeam : MonoBehaviour
{
    public float collectRadius; // �������� ȹ���� �� �ִ� �ݰ�
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
            // �÷��̾�� ����� ������ �Ÿ� ���
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // �÷��̾ ������ �ݰ� �ȿ� ������ ������ ȹ��
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
            // ����� ����
            Destroy(gameObject);
        }
        else
        {
            //������ �߰� ����, ���ٱ� ����
        }
        // ������ ȹ�� ���� 
        // ��: �κ��丮�� ������ �߰�
        Debug.Log("���� ������ ȹ��");
    }

    void CollectCommonItem()
    {
        int r = Random.Range(0, 4);

        bool itemAdded = invenManager.AddItemInventory(UIManager.Instance.commonItems[r]);

        if (itemAdded)
        {
            // ����� ����
            Destroy(gameObject);
        }
        else
        {
            //������ �߰� ����, ���ٱ� ����
        }
        // ������ ȹ�� ���� 
        // ��: �κ��丮�� ������ �߰�
        Debug.Log("���� ������ ȹ��");
    }

    void CollectBasicItem(int i)
    {
        bool itemAdded = invenManager.AddItemInventory(UIManager.Instance.basicItems[i]);

        if (itemAdded)
        {
            // ����� ����
            Destroy(gameObject);
        }
        else
        {
            //������ �߰� ����, ���ٱ� ����
        }
        // ������ ȹ�� ���� 
        // ��: �κ��丮�� ������ �߰�
        Debug.Log("�⺻ ������ ȹ��");
    }

    
}
