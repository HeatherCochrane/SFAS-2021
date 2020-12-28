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

    public int amount = 0;

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

            if (slotData.GetType() == typeof(Healing))
            {
                item = slotData as Healing;
                setHealingMenu(item as Healing);
            }

            Player.instance.inventory.setActiveSlot(this);
        }
        else
        {
            Player.instance.inventory.emptyInfoBox();
        }
    }

    public Item getItem()
    {
        return slotData;
    }

    public void equipItem()
    {
        if (weapon != null)
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
        else if(item != null)
        {
            if(item.GetType() == typeof(Healing))
            {
                Player.instance.inventory.RemoveItemFromInventory("Use");

                Healing i = item as Healing;
                Player.instance.playerStatus.healPlayer(i.healingAmount);
            }
        }

    }

    public void setEquipMenu()
    {
        Player.instance.inventory.showItemInfoBox(weapon.itemSprite, weapon.name, "Damage: " +  weapon.damage.ToString(), "Distance: " + weapon.distance.ToString(), weapon.sellPrice.ToString(), this, true, true);
    }

    public void setItemMenu()
    {
        Player.instance.inventory.showItemInfoBox(item.itemSprite, item.name, "", "", item.sellPrice.ToString(), this, false, false);
    }

    public void setHealingMenu(Healing item)
    {
        Player.instance.inventory.showItemInfoBox(item.itemSprite, item.name, item.healingAmount.ToString(), "", item.sellPrice.ToString(), this, true, false);
    }


    public bool removeInventory(string interaction)
    {
        if (amount > 0)
        {
            GameObject o = default;

            if (interaction == "Drop")
            {
                o = Instantiate(slotData.worldObject);
                o.GetComponent<SpriteRenderer>().sprite = slotData.itemSprite;
                o.GetComponent<WorldItem>().setData(slotData);
                o.SetActive(true);
                o.transform.position = Player.instance.transform.position + new Vector3(Random.Range(-2, 2), 3, 0);
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
