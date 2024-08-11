using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text ItemName;
    public Image itemImage;
    public Text itemPrice;

    public Item item;

    public void BuyItem()
    {

        Inventory inventory = Inventory.instance;

        if (inventory.coinsCount >= item.Price)
        {
            inventory.content.Add(item);
            inventory.UpdateInventoryUI();
            inventory.coinsCount -= item.Price;
            inventory.UpdateTextUI();
        }
    }
}
