using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    Image CoolTimeImage;
    public float maxSkillCollTime;
    public float currentSkillCollTime;

    public bool skillUseAble;
    void Start()
    {
        CoolTimeImage = GetComponent<Image>();
        currentSkillCollTime = maxSkillCollTime;
        skillUseAble = true;
    }

    // Update is called once per frame
    void Update()
    {
        CoolTimeImage.fillAmount = currentSkillCollTime / maxSkillCollTime;

        if(currentSkillCollTime > 0 && currentSkillCollTime < maxSkillCollTime)
        {
            skillUseAble = false;
        }

        if(!skillUseAble)
        {
            currentSkillCollTime -= Time.deltaTime;

            if(currentSkillCollTime <= 0)
            {
                currentSkillCollTime = maxSkillCollTime;
                skillUseAble = true;
            }
        }

    }
}
