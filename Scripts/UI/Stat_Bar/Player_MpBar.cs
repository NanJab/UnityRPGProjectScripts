using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_MpBar : MonoBehaviour
{
    private Image manaBar;
    PlayerStat character;
    public float lerpSpeed;
    private float currentFill;
    private float myMaxValue;
    private float currentValue;

    private float checkTime;
    public float mpRecoveryTime;
    public float recoveryMp;

    public float myCurrentValue;


    void Start()
    {
        manaBar = GetComponent<Image>();
        character = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();

        currentFill = character.mp / character.maxMp;
        manaBar.fillAmount = currentFill;

    }

    void Update()
    {
        if (Time.time > checkTime + mpRecoveryTime)
        {
            checkTime = Time.time;
            if (character.mp >= character.maxMp)
            {
                character.mp = character.maxMp;
                
            }
            else if(character.mp < character.maxMp)
            {
                character.mp += recoveryMp;            
            }

        }

        currentFill = character.mp / character.maxMp;

        if (manaBar.fillAmount != currentFill)
        {
            manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

        if (character.mp <= 0)
        {
            manaBar.fillAmount = 0;
        }
    }
}
