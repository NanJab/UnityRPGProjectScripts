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
        // 인벤토리와 스테이터스창이 모두 꺼져있을 때만 마우스커서가 사라지고 커서는 비활성화 된다.
        if (!UIManager.Instance.Inven && !UIManager.Instance.Status && !UIManager.Instance.market && !UIManager.Instance.questList && !UIManager.Instance.pauseMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {   
            // 그 외의 상황은 커서 활성화
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
