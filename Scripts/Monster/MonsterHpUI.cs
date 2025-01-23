using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    Monster monster;
    public TextMeshProUGUI hp;

    public Image monsterHpImg;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponentInParent<Monster>();

    }

    // Update is called once per frame
    void Update()
    {
        if(monster.health <= 0)
        {
            monster.health = 0;
        }
        monsterHpImg.fillAmount = monster.health / monster.maxHealth;
        monsterHpImg.fillAmount = Mathf.Lerp(monsterHpImg.fillAmount, monsterHpImg.fillAmount, 0.97f);

        hp.text = Mathf.RoundToInt(monster.health).ToString() + "/" + monster.maxHealth.ToString();
    }
}
