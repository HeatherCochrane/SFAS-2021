using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item slotData;

    Weapon weapon;
    Item item;

    public int amount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setButtonData(Item item)
    {
        slotData = item;
    }

    public void getItemData()
    {
        if (slotData != null)
        {
            if (slotData.GetType() == typeof(Weapon))
            {
                weapon = slotData as Weapon;
                setEquipMenu();
            }

            if (slotData.GetType() == typeof(Item))
            {
                item = slotData as Item;
                setItemMenu();
            }
        }
    }

    public Item getItem()
    {
        return slotData;
    }

    public void equipItem()
    {       
        if (weapon.weaponType == Weapon.WeaponType.MELEE)
        {
            Player.instance.weapons.equipMeleeWeapon(weapon);
        }
        else
        {
            Player.instance.weapons.equipRangedWeapon(weapon);
        }

    }

    public void setEquipMenu()
    {
        Player.instance.inventory.showWeaponInfoBox(weapon.itemSprite, weapon.name, "Damage: " +  weapon.damage.ToString(), "Distance: " + weapon.distance.ToString(), weapon.sellPrice.ToString(), this);
    }

    public void setItemMenu()
    {
        Player.instance.inventory.showWeaponInfoBox(item.itemSprite, item.name, "", "", item.sellPrice.ToString(), this);
    }

    public void dropItem()
    {
        if (slotData.worldObject != null)
        {
            slotData.worldObject.SetActive(true);
            slotData.worldObject.transform.position += new Vector3(5, 2, 0);
            Player.instance.inventory.removeItem(slotData);

            slotData = null;
        }
;    }

    public void setSlotPos(int p)
    {
        slotData.setSlotPos(p);
    }

    public int getSlotPos()
    {
        Debug.Log(slotData.getSlotPos());
        return slotData.getSlotPos();
    }
}
