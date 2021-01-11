using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagicianShop : ItemShop
{
    [SerializeField] int currentHPLevel;
    [SerializeField] int currentStrengthLevel;
    [SerializeField] int currentPlayerLevel;
    [SerializeField] int goldToPay;

    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI currentHPLevelText;
    [SerializeField] TextMeshProUGUI currentStrengthLevelText;
    [SerializeField] TextMeshProUGUI goldToPayText;

    [SerializeField] Button payButton;

    public void HealthPlus()
    {
        if(currentHPLevel + 1 < 100)
        {
            currentHPLevel++;
            goldToPay += 100 * currentPlayerLevel;
            currentPlayerLevel++;
            
        }
    }

    public void HealthSubtract()
    {
        if(currentHPLevel > Config.data.hpLevel)
        {
            currentHPLevel--;
            
            currentPlayerLevel--;
            goldToPay -= 100 * currentPlayerLevel;
        }
    }

    public void StrengthPlus()
    {
        if (currentStrengthLevel + 1 < 100)
        {
            currentStrengthLevel++;
            goldToPay += 100 * currentPlayerLevel;
            currentPlayerLevel++;
            
        }
    }

    public void StrengthSubtract()
    {
        if (currentStrengthLevel > Config.data.hpLevel)
        {
            currentStrengthLevel--;
            
            currentPlayerLevel--;
            goldToPay -= 100 * currentPlayerLevel;
        }
    }

    public void ApplyChanges()
    {
        Config.data.hpLevel = currentHPLevel;
        Config.data.strengthLevel = currentStrengthLevel;
        Config.data.gold -= goldToPay;
        FindObjectOfType<PlayerStatus>().UpdateStatus();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHPLevel = Config.data.hpLevel;
        currentStrengthLevel = Config.data.strengthLevel;
        currentPlayerLevel = Config.data.playerLevel;
        goldToPay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentStrengthLevelText)
            currentStrengthLevelText.text = currentStrengthLevel.ToString();
        if(currentHPLevelText)
            currentHPLevelText.text = currentHPLevel.ToString();
        if(currentLevelText)
            currentLevelText.text = currentPlayerLevel.ToString();
        if (goldToPayText)
            goldToPayText.text = "Cost: " + goldToPay.ToString();

        payButton.interactable = goldToPay <= Config.data.gold && Config.data.playerLevel < currentPlayerLevel ? true : false;
    }
}
