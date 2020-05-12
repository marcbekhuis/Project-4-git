using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string name;
    public string discription;
    public Image itemImage;
    public GameObject gameObject = null;
    public int amount = 0;

    public Item(string Name, int Amount = 0)
    {
        name = Name;
        amount = Amount;
    }

    public void SetPicture(string objectName)
    {
        itemImage = GameObject.Find(objectName).GetComponent<Image>();
    }
}
