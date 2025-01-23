using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestListDescription : MonoBehaviour
{
    public Quest quest;
    public QuestManager questManager;

    public GameObject descriptionUI;
    public GameObject completedQuestSlot;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI currentCountText;
    public TextMeshProUGUI rewardText;
    public string description; // 퀘스트 내용
    public int currentCount;
    public int totalCount;
    public int reward; // 퀘스트 보상
    public bool questCheck = false;

    private void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        completedQuestSlot = GameObject.Find("completedQuestSlot");
        descriptionUI = GameObject.Find("Quest_Description");
        descriptionText = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        rewardText = GameObject.Find("Reward").GetComponent<TextMeshProUGUI>();
        currentCountText = GameObject.Find("CurrentCount").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        currentCount = quest.currentCount;
        totalCount = quest.totalCount;
        if(quest.currentCount >= quest.totalCount)
        {
            quest.isCompleted = true;           
            questCheck = quest.isCompleted;
        }

        if (quest.isCompleted)
        {
            gameObject.transform.SetParent(completedQuestSlot.transform);

        }
    }

    public void UpdateQuestCurrentCount()
    {
        if (quest == null) return;
        descriptionText.text = $"퀘스트 내용 : {quest.description}";
        currentCountText.text = $"목표 : {quest.currentCount} / {quest.totalCount}";
        rewardText.text = $"보상 : {quest.reward}";

    }

}


