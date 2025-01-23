using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] basicItems;
    public GameObject[] commonItems;
    public GameObject[] rareItems;
    public static UIManager Instance;
    public bool Inven;
    public bool Status;
    public bool market;
    public bool questList;
    public bool pauseMenu = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // �κ��丮�� �������ͽ�â�� ��� �������� ���� ���콺Ŀ���� ������� Ŀ���� ��Ȱ��ȭ �ȴ�.
        if (!UIManager.Instance.Inven && !UIManager.Instance.Status && !UIManager.Instance.market && !UIManager.Instance.questList && !UIManager.Instance.pauseMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {   
            // �� ���� ��Ȳ�� Ŀ�� Ȱ��ȭ
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
