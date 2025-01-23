using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SoundManager soundManager;
    [SerializeField]
    GameObject rightHand;
    Move move;
    Animator pAnimator;    
    public bool weaponCheck;
    // Start is called before the first frame update
    void Start()
    {
        weaponCheck = false;
        pAnimator = GetComponent<Animator>();
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rightHand.transform.childCount <= 0)
        {
            weaponCheck = false;
        }
        else if(rightHand.transform.childCount > 0)
        {
            weaponCheck = true;
        }

        Attack();

    }

    private void FixedUpdate()
    {

    }

    public void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !Cursor.visible && weaponCheck)
        {
            Debug.Log("attack");            
            pAnimator.SetTrigger("Attack1");
        }
    }

    public void AttackSound()
    {
        soundManager.PlaySfx("PlayerAttackSound");

    }
}
