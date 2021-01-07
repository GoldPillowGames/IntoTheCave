using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class ItemShop : MonoBehaviour
{
    [SerializeField] NPCBehaviour npc;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject itemsPanel;
    [SerializeField] TextMeshProUGUI itemText;

    private ItemBox[] items;
    private ItemBox currentItem;
    // Start is called before the first frame update
    void Start()
    {
        if(buyButton)
            buyButton.interactable = false;
        if(itemsPanel)
            items = itemsPanel.GetComponentsInChildren<ItemBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem(ItemBox item)
    {
        this.currentItem = item;
        buyButton.interactable = true;
        itemText.text = "- " + item.price.ToString();
        for (int i = 0; i < items.Length; i++)
        {
            if(item == items[i])
            {
                items[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                items[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void BuyItem()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        GameObject item = Resources.Load<GameObject>(Path.Combine("PhotonPrefabs", "Item" + currentItem.itemIndex.ToString("00")));
        print("Item Spawned: item" + currentItem.itemIndex.ToString("00"));
        Instantiate(item, player.transform.position, Quaternion.identity);
        Config.data.gold -= currentItem.price;
        npc.CloseMenuPermanently();
        Destroy(this.gameObject);
    }

    public void EquipHelmet(int index)
    {
        FindObjectOfType<PlayerHelmetController>().EquipHelmet(index);
    }

    public void EquipSkin(int index)
    {
        FindObjectOfType<PlayerHelmetController>().EquipSkin(index);
    }

    public void CloseMenu()
    {
        if (itemsPanel)
        {
            foreach (ItemBox item in items)
            {
                item.GetComponent<Button>().interactable = true;
            }
            currentItem = null;
            buyButton.interactable = false;
            itemText.text = "";
            
        }
        this.gameObject.SetActive(false);
        npc.CloseMenu();
    }
}
