using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public int damage;
    public int distance;
    public int price;

}
