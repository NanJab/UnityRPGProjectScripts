using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    public PlayerStat player;
    public QuestList list;
    public GameObject questUI;                         // 퀘스트 패널 UI 
    public GameObject questPrefab;                     
    public GameObject progressQuestSlot;               
    public GameObject completedQuestSlot;               
    public GameObject questItemDelivery_1;               
    public GameObject questItemDelivery_2;               
    public GameObject questionMark;               
    public GameObject exclamationMark;               
    public TextMeshProUGUI questDetailsText;           // 퀘스트 상세 내용 표시 텍스트
    public TextMeshProUGUI rewardText;                  
    public TextMeshProUGUI questListCurrentCount;                 
    public Button acceptButton;                        // 수락 버튼
    public Button completeButton;                      // 완료 버튼
    public bool questAccept;
    public bool questClear = false;
    public bool questItemSlotCheck = false;
    public int Quest_1_currentCount;
    public int Quest_2_currentCount;

    private Quest currentQuest;                        // 현재 선택된 퀘스트

    void Start()
    {
        questAccept = false;
    }

    private void Update()
    {
        MarkControl();
        BeforeReceivingReward();

        if (currentQuest != null && currentQuest.questName == "물자 수송" && questItemSlotCheck && questItemDelivery_1 != null && questItemDelivery_2 != null && currentQuest.isAccept)
        {
            questItemDelivery_1.SetActive(true);
            questItemDelivery_2.SetActive(true);
        }
        else
        {
            if(questItemDelivery_1 != null) { questItemDelivery_1.SetActive(false); }
            if(questItemDelivery_2 != null) { questItemDelivery_2.SetActive(false); }
            
        }

        if (currentQuest != null && currentQuest.isCompleted)
        {
            completeButton.gameObject.SetActive(true);
        }
        else
        {
            completeButton.gameObject.SetActive(false);
        }
    }

    public void ShowQuestDetails(Quest quest)
    {        
        currentQuest = quest;
        // 상세 내용 표시
        if(questUI.activeSelf)
        {
            questUI.SetActive(false);
        }

        questUI.SetActive(true);        

        questDetailsText.text = $"{currentQuest.description}\n";
        rewardText.text = $"보상: {currentQuest.reward}\n";

        if (currentQuest.isAccept)
        {
            acceptButton.gameObject.SetActive(false);
            return;
        }
        // 수락/완료 버튼 상태 변경
        acceptButton.gameObject.SetActive(!currentQuest.isCompleted);
        completeButton.gameObject.SetActive(currentQuest.isCompleted);
    }

    // 퀘스트 수락버튼 기능
    public void AcceptQuest()
    {
        currentQuest.isAccept = true;
        questItemSlotCheck = true;

        GameObject inProgressQuest = Instantiate(questPrefab, progressQuestSlot.transform);
        inProgressQuest.GetComponentInChildren<TextMeshProUGUI>().text = currentQuest.questName;

        QuestListDescription questDescription = inProgressQuest.GetComponent<QuestListDescription>();

        if(questDescription != null)
        {
            questDescription.quest = currentQuest;
        }

        list.questContent.Add(inProgressQuest);
        acceptButton.gameObject.SetActive(false);
    }

    // 완료되었지만 보상은 받지 않은 퀘스트
    public void BeforeReceivingReward()
    {
        // 완료했는지 확인
        if (currentQuest != null && currentQuest.isCompleted)
        {
            // 안의 내용이 제거될 경우 인덱스를 건너뛸 경우를 대비해 끝에서부터 검사
            for (int i = list.questContent.Count - 1; i >= 0; i--)
            {
                if (list.questContent[i] == null) continue;

                QuestListDescription questDesc = list.questContent[i].GetComponent<QuestListDescription>();
                // 현재 퀘스트 체크
                if (questDesc.quest == currentQuest)
                {
                    // 완료된 퀘스트들의 부모를 다시 설정
                    list.questContent[i].gameObject.transform.SetParent(completedQuestSlot.transform);

                    GameObject questCheckItem = list.questContent[i];

                    // 진행중 리스트에서 삭제 후
                    list.questContent.RemoveAt(i);

                    // 완료됨 리스트로 추가
                    list.completedContent.Add(questCheckItem);
                    break;
                }
            }
        }
    }

    // 퀘스트 완료버튼 기능
    public void CompleteQuest()
    {
        if (currentQuest != null && currentQuest.isCompleted)
        {
            Debug.Log($"퀘스트 완료: {currentQuest.questName}. 보상 {currentQuest.reward} 골드 지급.");

            // 보상 받았는지 확인
            currentQuest.rewardCheck = true;

            // 보상 받았을 경우
            if(currentQuest.rewardCheck)
            {
                player.money += currentQuest.reward;
                for (int i = list.completedContent.Count - 1; i >= 0; i--)
                {
                    GameObject quest = list.completedContent[i];

                    if (quest != null && quest.GetComponent<QuestListDescription>().quest == currentQuest)
                    {
                        // 완료된 오브젝트 삭제
                        list.completedContent.RemoveAt(i);

                        Destroy(currentQuest.gameObject);
                        Destroy(quest);
                        break;
                    }
                }
            }

            currentQuest = null;
            questDetailsText.text = ""; // 상세 내용 초기화
            rewardText.text = "";
        }
    }

    private void MarkControl()
    {
        foreach(Quest quest in quests)
        {
            if(!quest.isAccept)
            {
                exclamationMark.SetActive(true);
            }
            else if (quest.isAccept)
            {
                exclamationMark.SetActive(false);
            }

            if(quest.isCompleted)
            {
                questionMark.SetActive(true);
            }

            if(quest == null)
            {
                questionMark.SetActive(false);
            }
            
        }
    }

}
