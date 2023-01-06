using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName = "ScriptableItems/Items")]
public class Item : ScriptableObject
{
    public int _itemType;
    public int _itemID;
    public string _itemName;
    public int _itemCost;
    public int _itemSellPrice;
    public Sprite _itemSprite;

    public Vector3 scaleTop;
    public Vector3 scaleDown;
    public Vector3 scaleRight;
    public Vector3 scaleLeft;

    public void Copy(Item item)
    {
        item._itemCost = _itemCost;
        item._itemType = _itemType;
        item._itemID = _itemID;
        item._itemName = _itemName;
        item._itemCost = _itemCost;
        item._itemSellPrice = _itemSellPrice;
        item._itemSprite = _itemSprite;
        item.scaleTop = scaleTop;
        item.scaleDown = scaleDown;
        item.scaleRight = scaleRight;
        item.scaleLeft = scaleLeft;
    }
}
