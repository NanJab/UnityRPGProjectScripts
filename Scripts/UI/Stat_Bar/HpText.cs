using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    public PlayerStat stat;
    TextMeshProUGUI hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"HP : {stat.hp}";
    }
}
