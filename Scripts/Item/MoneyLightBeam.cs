using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLightBeam : MonoBehaviour
{
    public float collectRadius;
    public int money;
    private PlayerStat player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance <= collectRadius) { CollectMoney(); }
        }
    }

    void CollectMoney()
    {
        player.money += money;
        Destroy(gameObject);
    }
}
