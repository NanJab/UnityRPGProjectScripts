using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentMoney : MonoBehaviour
{
    [SerializeField]
    private PlayerStat playerMoney;
    private TextMeshProUGUI currentMoney;
    // Start is called before the first frame update
    void Start()
    {
        currentMoney = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMoney.text = " 소지 금액 : " + playerMoney.money;
    }
}
