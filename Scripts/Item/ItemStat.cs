using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject itemStat;

    ItemWear itemWear;

    private void Start()
    {
        itemWear = GetComponent<ItemWear>();
        itemStat = GameObject.Find("ArmorStat");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemStatDisplay.Instance.mouseCheck = true;
        ItemStatDisplay.Instance.ShowItemStats(itemWear.equipName, itemWear.Att, itemWear.skillAtt, itemWear.Def);
        ItemStatDisplay.Instance.ItemStatsPosition();        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemStatDisplay.Instance.mouseCheck = false;
        ItemStatDisplay.Instance.ItemStatsBackPosition();
    }
}
