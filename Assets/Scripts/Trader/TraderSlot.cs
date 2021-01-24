using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderSlot : MonoBehaviour
{
    Trader trader;

    Item slotItem;


    Weapon weapon;
    Item item;

    public void setTrader(Trader t)
    {
        trader = t;
    }

    public void setButtonData(Item t)
    {
        slotItem = t;
    }

    //When this button is clicked
    public void getItemData()
    {
        if (slotItem != null)
        {
            if (slotItem.GetType() == typeof(Weapon))
            {
                weapon = slotItem as Weapon;
                setWeaponInfoMenu();
            }
            if (slotItem.GetType() == typeof(Item))
            {
                item = slotItem as Item;
                setInfoMenu();
            }
            if(slotItem.GetType() == typeof(Healing))
            {
                item = slotItem as Healing;
                setHealingMenu(item as Healing);
            }
        }
    }

    void setWeaponInfoMenu()
    {
        trader.showItemInfoBox(weapon.itemSprite, weapon.name, weapon.damage.ToString(), weapon.distance.ToString(), weapon.buyPrice.ToString(), this, weapon);
    }
    void setInfoMenu()
    {
        trader.showItemInfoBox(item.itemSprite, item.name, "", "", item.buyPrice.ToString(), this, item);
    }

    void setHealingMenu(Healing item)
    {
        trader.showItemInfoBox(item.itemSprite, item.name, item.healingAmount.ToString(), "", item.buyPrice.ToString(), this, item);
    }
}
