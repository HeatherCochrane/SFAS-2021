using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    Item slotData;

    [SerializeField]
    GameObject equipMenu;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        setEquipMenu(false);
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
        if (slotData.GetType() == typeof(Weapon))
        {
            weapon = slotData as Weapon;
            setEquipMenu(true);
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

        setEquipMenu(false);
    }

    public void setEquipMenu(bool set)
    {
        equipMenu.SetActive(set);
    }

    public void dropItem()
    {
        if (slotData.worldObject != null)
        {
            slotData.worldObject.SetActive(true);
            slotData.worldObject.transform.position += new Vector3(5, 2, 0);
            Player.instance.inventory.removeItem(slotData);
        }

        setEquipMenu(false);
    }
}
