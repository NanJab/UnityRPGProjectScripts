using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDmg : MonoBehaviour
{
    public BoxCollider bColl;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColliderOn()
    {
        bColl.enabled = true;
    }

    void ColliderOff()
    {
        bColl.enabled = false;
    }
}
