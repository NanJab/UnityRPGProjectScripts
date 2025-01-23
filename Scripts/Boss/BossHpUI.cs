using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUI : MonoBehaviour
{
    Boss boss;
    public TextMeshProUGUI hp;

    public Image monsterHpImg;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<Boss>();

    }

    // Update is called once per frame
    void Update()
    {
        if(boss.health <= 0)
        {
            boss.health = 0;
        }
        monsterHpImg.fillAmount = boss.health / boss.maxHealth;
        monsterHpImg.fillAmount = Mathf.Lerp(monsterHpImg.fillAmount, monsterHpImg.fillAmount, 0.97f);

        hp.text = Mathf.RoundToInt(boss.health).ToString() + "/" + boss.maxHealth.ToString();
    }
}
