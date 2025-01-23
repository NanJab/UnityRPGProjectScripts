using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    PlayerStat playerStat;
    [SerializeField]
    BoxCollider attackBox;
    public float dmg;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (dmg - playerStat.def <= 0) return;
            playerStat.hp -= (dmg - playerStat.def);
            Debug.Log("attack");
        }
    }
    void Start()
    {
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        attackBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(int a)
    {
        if(a == 0)
        {
            attackBox.enabled = false;
        }
        else if(a == 1)
        {
            attackBox.enabled = true;
        }
    }
}
