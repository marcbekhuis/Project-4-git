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

    public void SetPicture(string objectName)
    {
        itemImage = GameObject.Find(objectName).GetComponent<Image>();
    }
}
