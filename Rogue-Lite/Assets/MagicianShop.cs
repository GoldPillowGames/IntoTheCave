using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagicianShop : ItemShop
{
    [SerializeField] int currentHPLevel;
    [SerializeField] int currentStrengthLevel;
    [SerializeField] int currentDungeonLevel;
    [SerializeField] int currentPlayerLevel;
    [SerializeField] int goldToPay;

    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI currentHPLevelText;
    [SerializeField] TextMeshProUGUI currentDungeonLevelText;
    [SerializeField] TextMeshProUGUI currentStrengthLevelText;
    [SerializeField] TextMeshProUGUI goldToPayText;

    [SerializeField] Button payButton;

    [SerializeField] TextMeshProUGUI dungeonLevelTitle;
    [SerializeField] GameObject dungeonLevelSelector;

    [SerializeField] Vector2[] hpTitlePos;
    [SerializeField] Vector2[] hpPos;
    [SerializeField] Vector2[] strengthTitlePos;
    [SerializeField] Vector2[] strengthPos;
    [SerializeField] RectTransform hpTitle;
    [SerializeField] RectTransform hp;
    [SerializeField] RectTransform strengthTitle;
    [SerializeField] RectTransform strength;

    private int price = 15;

    public void HealthPlus()
    {
        if(currentHPLevel + 1 < 100)
        {
            currentHPLevel++;
            goldToPay += price * currentPlayerLevel;
            currentPlayerLevel++;
            
        }
    }

    public void HealthSubtract()
    {
        if(currentHPLevel > Config.data.hpLevel)
        {
            currentHPLevel--;
            
            currentPlayerLevel--;
            goldToPay -= price * currentPlayerLevel;
        }
    }

    public void StrengthPlus()
    {
        if (currentStrengthLevel + 1 < 100)
        {
            currentStrengthLevel++;
            goldToPay += price * currentPlayerLevel;
            currentPlayerLevel++;
            
        }
    }

    public void StrengthSubtract()
    {
        if (currentStrengthLevel > Config.data.strengthLevel)
        {
            currentStrengthLevel--;
            
            currentPlayerLevel--;
            goldToPay -= price * currentPlayerLevel;
        }
    }

    public void DungeonLevelPlus()
    {
        if (currentDungeonLevel + 1 < 100)
        {
            currentDungeonLevel++;
        }
    }

    public void DungeonLevelSubtract()
    {
        if (currentDungeonLevel > 1)
        {
            currentDungeonLevel--;
        }
    }

    public override void CloseMenu()
    {
        Config.data.dungeonLevel = currentDungeonLevel;
        base.CloseMenu();

    }

    public void ApplyChanges()
    {
        Config.data.hpLevel = currentHPLevel;
        Config.data.strengthLevel = currentStrengthLevel;
        Config.data.gold -= goldToPay;
        Config.data.dungeonLevel = currentDungeonLevel;
        FindObjectOfType<PlayerStatus>().UpdateStatus();
        FindObjectOfType<PlayerController>().health = FindObjectOfType<PlayerStatus>().health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        currentDungeonLevel = Config.data.dungeonLevel;

        hpTitle.localPosition = (Config.data.dungeonsCompleted > 0) ? hpTitlePos[0] : hpTitlePos[1];
        hp.localPosition = (Config.data.dungeonsCompleted > 0) ? hpPos[0] : hpPos[1];
        strengthTitle.localPosition = (Config.data.dungeonsCompleted > 0) ? strengthTitlePos[0] : strengthTitlePos[1];
        strength.localPosition = (Config.data.dungeonsCompleted > 0) ? strengthPos[0] : strengthPos[1];
        dungeonLevelTitle.gameObject.SetActive((Config.data.dungeonsCompleted > 0) ? true : false);
        dungeonLevelSelector.SetActive((Config.data.dungeonsCompleted > 0) ? true : false);
        currentHPLevel = Config.data.hpLevel;
        currentStrengthLevel = Config.data.strengthLevel;
        currentPlayerLevel = Config.data.playerLevel;
        goldToPay = 0;

        //int cont = 1;
        //while(cont != Config.data.playerLevel)
        //{
        //    goldToPay += price * cont;
        //    cont++;
        //}

        if(currentStrengthLevelText)
            currentStrengthLevelText.text = currentStrengthLevel.ToString();
        if(currentHPLevelText)
            currentHPLevelText.text = currentHPLevel.ToString();
        if(currentLevelText)
            currentLevelText.text = currentPlayerLevel.ToString();
        if (goldToPayText)
        {
            switch (Config.data.language)
            {
                case Language.EN:
                    goldToPayText.text = "Cost: " + goldToPay.ToString();
                    break;
                case Language.ES:
                    goldToPayText.text = "Coste: " + goldToPay.ToString();
                    break;
                case Language.DE:
                    goldToPayText.text = "Preis: " + goldToPay.ToString();
                    break;
                default:
                    goldToPayText.text = "Cost: " + goldToPay.ToString();
                    break;
            }
        }
            
        if(currentDungeonLevelText)
            currentDungeonLevelText.text = currentDungeonLevel.ToString();

        this.gameObject.SetActive(false);

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
        {
            switch (Config.data.language)
            {
                case Language.EN:
                    goldToPayText.text = "Cost: " + goldToPay.ToString();
                    break;
                case Language.ES:
                    goldToPayText.text = "Coste: " + goldToPay.ToString();
                    break;
                case Language.DE:
                    goldToPayText.text = "Preis: " + goldToPay.ToString();
                    break;
                default:
                    goldToPayText.text = "Cost: " + goldToPay.ToString();
                    break;
            }
        }
        if (currentDungeonLevelText)
            currentDungeonLevelText.text = currentDungeonLevel.ToString();

        payButton.interactable = goldToPay <= Config.data.gold && Config.data.playerLevel < currentPlayerLevel ? true : false;
    }
}
