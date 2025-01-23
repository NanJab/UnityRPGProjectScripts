using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    public float speed;
    public float range;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * range;

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
