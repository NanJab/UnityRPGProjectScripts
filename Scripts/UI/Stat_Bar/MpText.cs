using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MpText : MonoBehaviour
{
    public PlayerStat stat;
    TextMeshProUGUI mpText;
    // Start is called before the first frame update
    void Start()
    {
        mpText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        mpText.text = $"MP : {stat.mp}";
    }
}
