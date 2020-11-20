using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderSlot : MonoBehaviour
{
    [SerializeField]
    Trader trader;

    Item slotItem;


    Weapon weapon;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
