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
    public List<GameObject> questContent = new List<GameObject>(); // ���� �� ����Ʈ ���
    public List<GameObject> completedContent = new List<GameObject>(); // �Ϸ�� ����Ʈ ���
    public GameObject inProgressArrowIcon;   // ȭ��ǥ ������
    public GameObject completedArrowIcon;   // ȭ��ǥ ������


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
