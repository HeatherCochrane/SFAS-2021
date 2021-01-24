using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    Weapon meleeWeapon;
    [SerializeField]
    Weapon longRangeWeapon;

    [SerializeField]
    Weapon standardMeleeWeapon;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();   
    }

    public void changeMelee(Weapon m)
    {
        meleeWeapon = m;
    }

    public void changeLongRange(Weapon r)
    {
        longRangeWeapon = r;
    }

    public Weapon getMeleeWeapon()
    {
        return meleeWeapon;
    }
    public Weapon getRangedWeapon()
    {
        return longRangeWeapon;
    }

    public void equipRangedWeapon(Weapon w)
    {
        longRangeWeapon = w;
        player.setRangedWeapon(longRangeWeapon);
    }

    public void unequipRangedWeapon()
    {
        longRangeWeapon = null;
        player.setRangedWeapon(null);
    }

    public void equipMeleeWeapon(Weapon w)
    {
        meleeWeapon = w;
        player.setMeleeWeapon(meleeWeapon);
    }

    public void unequipMeleeWeapon()
    {
        meleeWeapon = standardMeleeWeapon;
        player.setMeleeWeapon(meleeWeapon);
    }


    public void checkWeapon(List<PlayerInventory.InventoryItem> items)
    {
        bool meleeSafe = false;
        bool rangedSafe = false;
        for(int i =0; i < items.Count; i++)
        {
            if(items[i].item == meleeWeapon)
            {
                meleeSafe = true;
            }
            if(items[i].item == longRangeWeapon)
            {
                rangedSafe = true;
            }
        }

        if(!meleeSafe)
        {
            unequipMeleeWeapon();
        }
        if(!rangedSafe)
        {
            unequipRangedWeapon();
        }
    }
}
