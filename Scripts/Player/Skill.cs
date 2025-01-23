using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillCoolTime[] skillCoolTime;
    public SoundManager soundManager;
    public GameObject skill2;
    public GameObject skill3;
    PlayerAttack attack;
    Animator pAnimator;
    [SerializeField]
    PlayerStat stat;
    public EffectInfo[] effects;

    [System.Serializable]
    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter;
    }
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        pAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stat.mp >= 10 && Input.GetKeyDown(KeyCode.Alpha1) && !Cursor.visible && attack.weaponCheck && skillCoolTime[0].skillUseAble)
        {
            pAnimator.SetTrigger("Skill1");
            soundManager.PlaySfx("Spell_02");
            skillCoolTime[0].skillUseAble = false;
            //stat.mp -= 10;
        }
        else if(stat.mp >= 20 && Input.GetKeyDown(KeyCode.Alpha2) && !Cursor.visible && attack.weaponCheck && skill2.activeSelf && skillCoolTime[1].skillUseAble)
        {
            pAnimator.SetTrigger("Skill2");            
            skillCoolTime[1].skillUseAble = false;
        }
        else if(stat.mp >= 30 && Input.GetKeyDown(KeyCode.Alpha3) && !Cursor.visible && attack.weaponCheck && skill3.activeSelf && skillCoolTime[2].skillUseAble)
        {
            pAnimator.SetTrigger("Skill3");
            skillCoolTime[2].skillUseAble = false;
        }
    }

    void InstantiateEffect(int effectNum)
    {
        if(effectNum == 0)
        {
            var instance = Instantiate(effects[0].Effect,
                                   effects[0].StartPositionRotation.position,
                                   effects[0].StartPositionRotation.rotation);

            instance.transform.position = effects[0].StartPositionRotation.position;

            Destroy(instance, effects[0].DestroyAfter);
        }
        else if(effectNum == 1)
        {
            var instance = Instantiate(effects[1].Effect,
                                   effects[1].StartPositionRotation.position,
                                   effects[1].StartPositionRotation.rotation);

            instance.transform.position = effects[1].StartPositionRotation.position;

            Destroy(instance, effects[1].DestroyAfter);

        }
        else if(effectNum == 2)
        {
            var instance = Instantiate(effects[2].Effect,
                                   effects[2].StartPositionRotation.position,
                                   effects[2].StartPositionRotation.rotation);

            instance.transform.position = effects[2].StartPositionRotation.position + Vector3.up;

            Destroy(instance, effects[2].DestroyAfter);
        }
        else if (effectNum == 3)
        {
            var instance = Instantiate(effects[3].Effect,
                                   effects[3].StartPositionRotation.position,
                                   effects[3].StartPositionRotation.rotation);

            instance.transform.position = effects[3].StartPositionRotation.position + Vector3.up;

            Destroy(instance, effects[3].DestroyAfter);
        }
        else if (effectNum == 4)
        {
            var instance = Instantiate(effects[4].Effect,
                                   effects[4].StartPositionRotation.position,
                                   effects[4].StartPositionRotation.rotation);

            instance.transform.position = effects[4].StartPositionRotation.position + Vector3.up * 0.5f;

            Destroy(instance, effects[4].DestroyAfter);
        }

    }

    public void MpUse(int skillIndex)
    {
        int manaCount = 0;

        switch(skillIndex)
        {
            case 1: manaCount = 10; break;
            case 2: manaCount = 20; break;
            case 3: manaCount = 30; break;
        }    

        if(stat.mp >= manaCount)
        {
            stat.mp -= manaCount;
        }
    }

    public void SkillSound(int i)
    {
        if(i == 3)
        {
            soundManager.PlaySfx("Skill3");

        }

    }
}
