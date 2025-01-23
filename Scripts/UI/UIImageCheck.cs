using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageCheck : MonoBehaviour
{
    public SoundManager soundManager;

    public GameObject inven;
    public GameObject stat;
    public GameObject questPos;
    public GameObject UI;
    public GameObject warningUI;
    public GameObject gamePause;
    private Vector3 questStartPos;
    private Vector3 questOffScreenPos;
    private Vector3 invenOffPosition;
    private Vector3 invenSpawnPosition;
    private Vector3 statOffPosition;
    private Vector3 statSpawnPosition;
    public Vector3 warningSpawnPosition;
    public Vector3 warningOffPosition;
    private bool isQuestVisible = false;
    public bool invenOn = false;
    private bool statOn = false;

    // Start is called before the first frame update
    void Start()
    {
        warningSpawnPosition = warningUI.transform.position;
        warningOffPosition = new Vector3(-10000, 0, 0);
        warningUI.transform.position = warningOffPosition;
        // 퀘스트 창 초기 위치와 화면 밖 위치를 설정
        questStartPos = questPos.transform.position; // 현재 위치 저장
        questOffScreenPos = new Vector2(questStartPos.x, questStartPos.y - 1000f); // 화면 밖 위치 (아래쪽으로 이동)
        questPos.transform.position = questOffScreenPos; // 처음에는 퀘스트 UI를 화면 밖으로 숨김

        // 인벤토리 초기 위치
        invenSpawnPosition = inven.transform.position;

        invenOffPosition = invenSpawnPosition + new Vector3(-10000, 0, 0);

        inven.transform.position = invenOffPosition;

        statSpawnPosition = stat.transform.position;
        statOffPosition = statSpawnPosition + new Vector3(-10000, 0, 0);
        stat.transform.position = statOffPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleQuestUI();
            if (questPos.transform.position == questOffScreenPos)
            {
                UIManager.Instance.questList = false;
                return;
            }

            UIManager.Instance.questList = true;
        }
        else if(Input.GetKeyDown(KeyCode.I)) 
        {

            ToggleInventory();       

            if (inven.transform.position == invenOffPosition)
            {
                UIManager.Instance.Inven = false;
                UI.SetActive(false);
                return;
            }

            UIManager.Instance.Inven = true;
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleStat();
            if(stat.transform.position == statOffPosition)
            {
                UIManager.Instance.Status = false;
                UI.SetActive(false);
                return;
            }

            UIManager.Instance.Status = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(!UIManager.Instance.pauseMenu)
            {
                GamePause();
            }

        }
    }

    private void ToggleQuestUI()
    {
        if (isQuestVisible)
        {
            // 퀘스트 UI를 화면 밖으로 이동
            questPos.transform.position = questOffScreenPos;
            isQuestVisible = false;
        }
        else
        {
            // 퀘스트 UI를 화면 안으로 이동
            questPos.transform.position = questStartPos;
            isQuestVisible = true;
        }

    }

    void ToggleInventory()
    {
        // 인벤토리 UI가 활성화되어 있는지 확인
        if (invenOn)
        {
            inven.transform.position = invenOffPosition;
            invenOn = false;
        }
        else if (!invenOn)
        {
            inven.transform.position = invenSpawnPosition;
            invenOn = true;
        }
    }

    void ToggleStat()
    {
        if(statOn)
        {
            stat.transform.position = statOffPosition;
            statOn= false;
        }
        else if(!statOn)
        {
            stat.transform.position = statSpawnPosition;
            statOn = true;
        }
    }

    public void GamePause()
    {
        UIManager.Instance.pauseMenu = true;
        gamePause.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        soundManager.bgmSource.Pause();
    }

    public void GameResume()
    {
        UIManager.Instance.pauseMenu = false;
        gamePause.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        soundManager.bgmSource.UnPause();
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
