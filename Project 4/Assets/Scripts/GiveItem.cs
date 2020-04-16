using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    Inventory playerInv;

    void Start()
    {
        playerInv = GameObject.Find("City").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        Item aWheat = new Item();
        aWheat.name = "Wheat";
        aWheat.discription = "It's just some wheat";
        aWheat.SetPicture("WheatImage");
        //aWheat.gameObject = new GameObject();
        aWheat.amount = 0;

        TranferItem(aWheat);
    }

    void TranferItem(Item thisItem)
    {
        playerInv.PickUpItem(thisItem);
        playerInv.CheckInventory();
    }
}
