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
    public GameObject questUI;                         // ����Ʈ �г� UI 
    public GameObject questPrefab;                     
    public GameObject progressQuestSlot;               
    public GameObject completedQuestSlot;               
    public GameObject questItemDelivery_1;               
    public GameObject questItemDelivery_2;               
    public GameObject questionMark;               
    public GameObject exclamationMark;               
    public TextMeshProUGUI questDetailsText;           // ����Ʈ �� ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI rewardText;                  
    public TextMeshProUGUI questListCurrentCount;                 
    public Button acceptButton;                        // ���� ��ư
    public Button completeButton;                      // �Ϸ� ��ư
    public bool questAccept;
    public bool questClear = false;
    public bool questItemSlotCheck = false;
    public int Quest_1_currentCount;
    public int Quest_2_currentCount;

    private Quest currentQuest;                        // ���� ���õ� ����Ʈ

    void Start()
    {
        questAccept = false;
    }

    private void Update()
    {
        MarkControl();
        BeforeReceivingReward();

        if (currentQuest != null && currentQuest.questName == "���� ����" && questItemSlotCheck && questItemDelivery_1 != null && questItemDelivery_2 != null && currentQuest.isAccept)
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
        // �� ���� ǥ��
        if(questUI.activeSelf)
        {
            questUI.SetActive(false);
        }

        questUI.SetActive(true);        

        questDetailsText.text = $"{currentQuest.description}\n";
        rewardText.text = $"����: {currentQuest.reward}\n";

        if (currentQuest.isAccept)
        {
            acceptButton.gameObject.SetActive(false);
            return;
        }
        // ����/�Ϸ� ��ư ���� ����
        acceptButton.gameObject.SetActive(!currentQuest.isCompleted);
        completeButton.gameObject.SetActive(currentQuest.isCompleted);
    }

    // ����Ʈ ������ư ���
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

    // �Ϸ�Ǿ����� ������ ���� ���� ����Ʈ
    public void BeforeReceivingReward()
    {
        // �Ϸ��ߴ��� Ȯ��
        if (currentQuest != null && currentQuest.isCompleted)
        {
            // ���� ������ ���ŵ� ��� �ε����� �ǳʶ� ��츦 ����� ���������� �˻�
            for (int i = list.questContent.Count - 1; i >= 0; i--)
            {
                if (list.questContent[i] == null) continue;

                QuestListDescription questDesc = list.questContent[i].GetComponent<QuestListDescription>();
                // ���� ����Ʈ üũ
                if (questDesc.quest == currentQuest)
                {
                    // �Ϸ�� ����Ʈ���� �θ� �ٽ� ����
                    list.questContent[i].gameObject.transform.SetParent(completedQuestSlot.transform);

                    GameObject questCheckItem = list.questContent[i];

                    // ������ ����Ʈ���� ���� ��
                    list.questContent.RemoveAt(i);

                    // �Ϸ�� ����Ʈ�� �߰�
                    list.completedContent.Add(questCheckItem);
                    break;
                }
            }
        }
    }

    // ����Ʈ �Ϸ��ư ���
    public void CompleteQuest()
    {
        if (currentQuest != null && currentQuest.isCompleted)
        {
            Debug.Log($"����Ʈ �Ϸ�: {currentQuest.questName}. ���� {currentQuest.reward} ��� ����.");

            // ���� �޾Ҵ��� Ȯ��
            currentQuest.rewardCheck = true;

            // ���� �޾��� ���
            if(currentQuest.rewardCheck)
            {
                player.money += currentQuest.reward;
                for (int i = list.completedContent.Count - 1; i >= 0; i--)
                {
                    GameObject quest = list.completedContent[i];

                    if (quest != null && quest.GetComponent<QuestListDescription>().quest == currentQuest)
                    {
                        // �Ϸ�� ������Ʈ ����
                        list.completedContent.RemoveAt(i);

                        Destroy(currentQuest.gameObject);
                        Destroy(quest);
                        break;
                    }
                }
            }

            currentQuest = null;
            questDetailsText.text = ""; // �� ���� �ʱ�ȭ
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
