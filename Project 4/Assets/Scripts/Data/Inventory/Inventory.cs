using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    public List<Item> playerInventory = new List<Item>();

    Item lastItem;

    int inventoryMaxSize = 10;
    int fullStack = 1000;

    public void PickUpItem(Item item, int amount)
    {
        //Checks if the player already has the item
        foreach (var currentItem in playerInventory)
        {
            if (currentItem.name == item.name)
            {
                //Checks if the item in the inventory isn't already full
                if (currentItem.amount + amount <= fullStack)
                {
                    currentItem.amount += amount;
                    return;
                }
            }
        }

        playerInventory.Add(new Item(item.name, amount));
    }

    void CannotGetItem()
    {
        //Add logic here
    }

    public void CheckInventory()
    {
        int timesRun = 0;
        foreach (var item in playerInventory)
        {
            //print(timesRun + " - " + item.amount);
            timesRun++;
        }
        //print("Last Item: " + lastItem.amount);
    }
}
