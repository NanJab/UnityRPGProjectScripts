using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmith : MonoBehaviour
{
    public BlackSmith_Text ui;
    public PlayerStat player;
    public GameObject ballon;
    public GameObject buyUI;
    public GameObject multipleUI;
    public GameObject quest;

    public float dis;
    void Start()
    {
        ballon.SetActive(false);
    }

    void Update()
    {
        UiOnOff();
    }

    void UiOnOff()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= dis)
        {
            ballon.SetActive(true);

        }
        else if(distance > dis)
        {
            ballon.SetActive(false);
            buyUI.SetActive(false);
            multipleUI.SetActive(false);
            ui.market.transform.position = ui.marketOffPos;
            ui.questPos.transform.position = ui.questOffScreenPos;
        }
    }
}
