using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public BoxCollider coll;
    public GameObject rightHand;

    private void Update()
    {
        coll = rightHand.GetComponentInChildren<BoxCollider>();
    }

    void EnableCollider()
    {
        coll.enabled = true;
    }

    void UnenableCollider()
    {
        coll.enabled = false;
    }
}
