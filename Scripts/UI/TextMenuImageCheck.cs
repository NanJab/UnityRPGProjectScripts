using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMenuImageCheck : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI disarmText;
    public Image[] images;
    public bool textImageCheck;
    void Start()
    {
        images = gameObject.GetComponentsInChildren<Image>();
        text = GameObject.Find("EquipText").GetComponent<TextMeshProUGUI>();
        disarmText = GameObject.Find("DisarmText").GetComponent<TextMeshProUGUI>();
        text.enabled = false;
        disarmText.enabled = false;
        foreach (Image image in images)
        {
            image.enabled = false;
        }

        textImageCheck = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(textImageCheck)
        {
            foreach(Image image in images)
            {
                image.enabled = true;                
            }
            text.enabled = true;
            disarmText.enabled = true;
        }
        else
        {
            foreach(Image image in images)
            {
                image.enabled = false;
            }
            text.enabled = false;
            disarmText.enabled = false;
        }
    }
}
