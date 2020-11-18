using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField]
    public Sprite itemSprite;
    [SerializeField]
    public int buyPrice;
    [SerializeField]
    public int sellPrice;
    [SerializeField]
    public GameObject worldObject;
    [SerializeField]
    public bool stackable;

    int slotPos;

    public void setSlotPos(int p)
    {
        slotPos = p;
    }

    public int getSlotPos()
    {
        return slotPos;
    }

}
