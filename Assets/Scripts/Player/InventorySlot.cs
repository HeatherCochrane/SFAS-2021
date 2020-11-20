using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    Item slotData;

    Weapon weapon;
    Item item;

    public int amount = 1;

    public TextMeshProUGUI stacked;

    // Start is called before the first frame update
    void Start()
    {
        stacked.text = amount.ToString();
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

            Player.instance.inventory.setActiveSlot(this);
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
        Player.instance.inventory.showItemInfoBox(weapon.itemSprite, weapon.name, "Damage: " +  weapon.damage.ToString(), "Distance: " + weapon.distance.ToString(), weapon.sellPrice.ToString(), this);
    }

    public void setItemMenu()
    {
        Player.instance.inventory.showItemInfoBox(item.itemSprite, item.name, "", "", item.sellPrice.ToString(), this);
    }


    public bool removeInventory(string interaction)
    {
        if (amount > 0)
        {
            GameObject o = default;

            if (interaction == "Drop")
            {
                o = Instantiate(slotData.worldObject);
                o.SetActive(true);
                o.transform.position += new Vector3(5, 2, 0);
            }
            
            amount -= 1;
            stacked.text = amount.ToString();

            return true;
        }

        return false;
    }

    public void updateStackedUI()
    {
        stacked.text = amount.ToString();
    }

    public void setSlotPos(int p)
    {
        slotData.setSlotPos(p);
    }

    public int getSlotPos()
    {
        return slotData.getSlotPos();
    }
}
