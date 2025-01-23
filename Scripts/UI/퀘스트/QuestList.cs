using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public QuestManager questManager;
    public RectTransform inProgress;
    public RectTransform completed;
    VerticalLayoutGroup layout;
    public List<GameObject> questContent = new List<GameObject>(); // 진행 중 퀘스트 목록
    public List<GameObject> completedContent = new List<GameObject>(); // 완료됨 퀘스트 목록
    public GameObject inProgressArrowIcon;   // 화살표 아이콘
    public GameObject completedArrowIcon;   // 화살표 아이콘


    private void Start()
    {
        layout = GetComponent<VerticalLayoutGroup>();
    }

    private void Update()
    {
        for(int i = questContent.Count - 1; i >= 0; i--)
        {
            GameObject quests = questContent[i];
            if (questContent[i].GetComponent<QuestListDescription>().questCheck)
            {
                questContent.Remove(quests);
                completedContent.Add(quests);
            }
        }
    }
}
