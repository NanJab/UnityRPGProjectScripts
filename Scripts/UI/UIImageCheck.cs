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
        // ����Ʈ â �ʱ� ��ġ�� ȭ�� �� ��ġ�� ����
        questStartPos = questPos.transform.position; // ���� ��ġ ����
        questOffScreenPos = new Vector2(questStartPos.x, questStartPos.y - 1000f); // ȭ�� �� ��ġ (�Ʒ������� �̵�)
        questPos.transform.position = questOffScreenPos; // ó������ ����Ʈ UI�� ȭ�� ������ ����

        // �κ��丮 �ʱ� ��ġ
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
            // ����Ʈ UI�� ȭ�� ������ �̵�
            questPos.transform.position = questOffScreenPos;
            isQuestVisible = false;
        }
        else
        {
            // ����Ʈ UI�� ȭ�� ������ �̵�
            questPos.transform.position = questStartPos;
            isQuestVisible = true;
        }

    }

    void ToggleInventory()
    {
        // �κ��丮 UI�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
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
