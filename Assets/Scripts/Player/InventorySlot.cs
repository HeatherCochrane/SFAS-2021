using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item slotData;

    Weapon weapon;

    
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
        }
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
        Player.instance.inventory.showWeaponInfoBox(weapon.itemSprite, weapon.name, weapon.damage.ToString(), weapon.distance.ToString(), this);
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

        Player.instance.inventory.showInfoBox(false);
;    }
}
