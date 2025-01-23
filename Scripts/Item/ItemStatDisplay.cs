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
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            // 씬 전환시 파괴되지 않도록 설정 가능
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 중복 인스턴스 제거
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
        attackText.text = "공격력 " + attackPower.ToString();
        skillAttackText.text = "스킬 공격력 " + skillAttPower.ToString();
        defenseText.text = "방어력 " + defensePower.ToString();
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

            // x축 위치가 화면 밖으로 나가면 반대쪽으로 조정
            if (pos.x - panelWidth < 0) // 패널이 왼쪽 화면 경계를 넘는 경우
            {
                pos.x += panelWidth;
            }
            else if (pos.x > canvasWidth) // 오른쪽 화면 경계를 넘는 경우
            {
                pos.x -= panelWidth;
            }

            // y축 위치가 화면 밖으로 나가면 반대쪽으로 조정
            if (pos.y - panelHeight < 0) // 패널이 아래쪽 화면 경계를 넘는 경우
            {
                pos.y += panelHeight;
            }
            else if (pos.y > canvasHeight) // 위쪽 화면 경계를 넘는 경우
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
