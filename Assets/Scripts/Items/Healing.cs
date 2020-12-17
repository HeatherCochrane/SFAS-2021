using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "New Healing/Healing", order = 1)]
public class Healing : Item
{
    [SerializeField]
    public int healingAmount;

}
