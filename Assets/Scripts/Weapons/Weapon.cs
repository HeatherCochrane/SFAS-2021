using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon/Weapon", order = 1)]
public class Weapon : Item
{
    public int damage;
    public int distance;

    public enum WeaponType { MELEE, RANGED};

    public WeaponType weaponType;
}
