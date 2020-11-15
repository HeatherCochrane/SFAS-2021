using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    Weapon meleeWeapon;
    [SerializeField]
    Weapon longRangeWeapon;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void equipMeleeWeapon(Weapon w)
    {
        meleeWeapon = w;
        player.setMeleeWeapon(meleeWeapon);
    }
}
