using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HpBar : MonoBehaviour
{
    private Image healthBar;
    PlayerStat character;
    public float lerpSpeed;
    public float HpCheck;
    public float currentFill;
    public float myMaxValue;
    public float currentValue;

    public float myCurrentValue;


    void Start()
    {
        healthBar = GetComponent<Image>();
        character = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();

        currentFill = character.hp / character.maxHp;
        healthBar.fillAmount = currentFill;

    }

    void Update()
    {
        currentFill = character.hp / character.maxHp;

        if (healthBar.fillAmount != currentFill)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

        if (character.hp <= 0)
        {
            healthBar.fillAmount = 0;
        }
    }
}
