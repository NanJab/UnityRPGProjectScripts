using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public string questName; // Äù½ºÆ® ÀÌ¸§
    public string description; // Äù½ºÆ® ³»¿ë
    public int reward; // Äù½ºÆ® º¸»ó
    public bool isCompleted = false;
    public bool isAccept = false;
    public int currentCount;
    public int totalCount;
    public bool rewardCheck = false;

    public void UpdateCurrentCount(int count)
    {
        if (!isAccept) return;

        currentCount += count;

        if(currentCount >= totalCount)
        {
            currentCount = totalCount;
            isAccept = true;
        }
    }
}
