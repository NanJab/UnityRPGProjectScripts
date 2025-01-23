using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemStatDisplay : MonoBehaviour
{
    public static ItemStatDisplay Instance { get; private set; }

    Vector3 backPosition;
    public bool mouseCheck;
    public RectTransform canvasRect;
    RectTransform panelRect;
    public TextMeshProUGUI equipName;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI skillAttackText;
    public TextMeshProUGUI defenseText;
    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            // �� ��ȯ�� �ı����� �ʵ��� ���� ����
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �ߺ� �ν��Ͻ� ����
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        backPosition = transform.position;
        panelRect = GetComponent<RectTransform>();
    }

    public void ShowItemStats(string name, int attackPower,int skillAttPower, int defensePower)
    {
        equipName.text = name.ToString();
        attackText.text = "���ݷ� " + attackPower.ToString();
        skillAttackText.text = "��ų ���ݷ� " + skillAttPower.ToString();
        defenseText.text = "���� " + defensePower.ToString();
    }

    public void ItemStatsPosition()
    {
        if(mouseCheck)
        {
            panelRect.pivot = new Vector2(1, 1);
            panelRect.position = Input.mousePosition;

            Vector2 pos = panelRect.position;
            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;

            float panelWidth = panelRect.rect.width;
            float panelHeight = panelRect.rect.height;

            // x�� ��ġ�� ȭ�� ������ ������ �ݴ������� ����
            if (pos.x - panelWidth < 0) // �г��� ���� ȭ�� ��踦 �Ѵ� ���
            {
                pos.x += panelWidth;
            }
            else if (pos.x > canvasWidth) // ������ ȭ�� ��踦 �Ѵ� ���
            {
                pos.x -= panelWidth;
            }

            // y�� ��ġ�� ȭ�� ������ ������ �ݴ������� ����
            if (pos.y - panelHeight < 0) // �г��� �Ʒ��� ȭ�� ��踦 �Ѵ� ���
            {
                pos.y += panelHeight;
            }
            else if (pos.y > canvasHeight) // ���� ȭ�� ��踦 �Ѵ� ���
            {
                pos.y -= panelHeight;
            }

            panelRect.position = pos;
        }
        
    }

    public void ItemStatsBackPosition()
    {
        if(!mouseCheck) transform.position = backPosition;
    }
}
