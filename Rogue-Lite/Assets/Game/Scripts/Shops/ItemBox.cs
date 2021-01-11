using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public int itemIndex = 0;
    public int price = 300;

    public void BuyItem()
    {
        //if (price <= Config.data.gold)
        //{
            GetComponentInParent<ItemShop>().SelectItem(this);
            print("Item");
        //}
        
    }
}
