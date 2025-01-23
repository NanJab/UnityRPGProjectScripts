using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith_Text : MonoBehaviour
{
    public TextMeshProUGUI fullText;
    public GameObject text;
    public GameObject market;
    public GameObject questPos;

    public GameObject buyUI;
    public GameObject MultipleBuyUI;
    private string dialogue = "안녕하신가 여기는 대장간이네!!";
    private Coroutine typing;
    private int currentIndex = 0;
    private float timer = 0f;
    public float typingSpeed = 0.05f;
    public bool isDialogueActive;
    public bool questListCheck;

    private Vector3 questStartPos;
    public Vector3 questOffScreenPos;

    public Vector3 marketSpawnPos;
    public Vector3 marketOffPos;

    void Start()
    {
        questListCheck = false;
        isDialogueActive = true; // 대사 한번만 실행하기 위한 체크기능

        // 초기 위치와 화면 밖 위치를 설정
        questStartPos = questPos.transform.position; // 현재 위치 저장
        questOffScreenPos = questStartPos + new Vector3(-10000, 0, 0); // 화면 밖 위치

        // 처음에는 퀘스트 UI를 화면 밖으로 숨김
        questPos.transform.position = questOffScreenPos;

        marketSpawnPos = market.transform.position;
        marketOffPos = marketSpawnPos + new Vector3(-10000, 0, 0);

        market.transform.position = marketOffPos;
    }

    void Update()
    {
        if(market.transform.position == marketSpawnPos || questPos.transform.position == questStartPos) 
        {
            UIManager.Instance.market = true;
        }
        else if(market.transform.position == marketOffPos && questPos.transform.position == questOffScreenPos)
        {
            UIManager.Instance.market = false;
        }

        if(questListCheck)
        {
            questStartPos = questPos.transform.position;
        }

        if(text.activeSelf) // 대사창 활성화 되었을때
        {
            if(isDialogueActive)
            {
                ShowTextByTimer();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleMarketUI();
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                ToggleQuestUI();
            }
        }
        else if(!text.activeSelf && !isDialogueActive) // 대사창 비활성화 되었을때
        {
            ResetText();
        }
    }

    private void ToggleQuestUI()
    {
        if (questPos.transform.position == questStartPos)
        {
            // 퀘스트 UI를 화면 밖으로 이동
            questPos.transform.position = questOffScreenPos;
            questListCheck = false;
        }
        else if(questPos.transform.position == questOffScreenPos)
        {
            // 퀘스트 UI를 화면 안으로 이동
            questPos.transform.position = questStartPos;
            questListCheck = true;
        }
    }

    private void ToggleMarketUI()
    {
        if(market.transform.position == marketSpawnPos)
        {
            market.transform.position = marketOffPos;
            buyUI.SetActive(false);
            MultipleBuyUI.SetActive(false);
        }
        else if(market.transform.position == marketOffPos)
        {
            market.transform.position = marketSpawnPos;
        }
    }

    private void ShowTextByTimer()
    {
        timer += Time.deltaTime;

        if (timer >= typingSpeed && currentIndex < dialogue.Length)
        {
            fullText.text += dialogue[currentIndex];
            currentIndex++;
            timer = 0f;
        }
        else if (currentIndex >= dialogue.Length)
        {
            isDialogueActive = false;
        }
    }

    public void ResetText()
    {
        fullText.text = "";
        currentIndex = 0;
        timer = 0f;
        isDialogueActive = true;       
    }
}
