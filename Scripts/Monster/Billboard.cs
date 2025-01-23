using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera theCam;
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(theCam.transform);
    }
}
