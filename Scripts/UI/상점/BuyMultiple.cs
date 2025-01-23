using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMultiple : MonoBehaviour
{
    public PlayerStat money;
    public InvenManager invenManager;
    public Market market;
    public UIImageCheck uiManager;

    public Slider buySlider;
    public TextMeshProUGUI countText;
    public Button buyButton;
    public Button cancelButton;

    public float currentCount = 0;
    public float potionPrice;
    public float resetTime;
    public float warningTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        potionPrice = market.potionPrice;
        buySlider.minValue = 0;
        buySlider.maxValue = 99;

    }

    private void Update()
    {
        currentCount = buySlider.value;
        countText.text = $"{currentCount}";

        if(uiManager.warningUI.transform.position == uiManager.warningSpawnPosition)
        {
            if (Time.time > resetTime + warningTime)
            {
                TextMeshProUGUI text = uiManager.warningUI.GetComponentInChildren<TextMeshProUGUI>();
                uiManager.warningUI.transform.position = uiManager.warningOffPosition;
                text.text = "";
                resetTime = Time.time;
            }
        }
    }

    public void MultipleBuy()
    {
        if(money.money >= potionPrice * currentCount)
        {
            GameObject potionInstance = Instantiate(market.currentItem);
            bool added = invenManager.Pluspotioninven(potionInstance, currentCount);

            if(added)
            {
                money.money -= potionPrice * currentCount;
                market.potionAmount += currentCount;
                if(!potionInstance.transform.parent)
                {
                    Destroy(potionInstance);
                }
            }
            else
            {
                Destroy(potionInstance);
            }
            gameObject.SetActive(false);
        }
        else
        {
            resetTime = Time.time;
            uiManager.warningUI.transform.position = uiManager.warningSpawnPosition;
            TextMeshProUGUI text = uiManager.warningUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "돈이 충분하지 않습니다.";
        }

    }
}
