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
    public GameObject gameObject;
    public int amount;

    public void SetPicture(string objectName)
    {
        itemImage = GameObject.Find(objectName).GetComponent<Image>();
    }
}
