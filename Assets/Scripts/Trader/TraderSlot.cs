using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderSlot : MonoBehaviour
{
    [SerializeField]
    Trader trader;

    Item slotItem;


    Weapon weapon;
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
            Debug.Log(slotItem);
            if (slotItem.GetType() == typeof(Weapon))
            {
                weapon = slotItem as Weapon;
                setInfoMenu();
            }
        }
    }

    void setInfoMenu()
    {
        trader.showWeaponInfoBox(weapon.itemSprite, weapon.name, weapon.damage.ToString(), weapon.distance.ToString(), weapon.price.ToString(), this, weapon);
    }
}
