using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Killable", menuName = "New Killable/Killable", order = 1)]
public class KillableData : ScriptableObject
{
    public int health;
    public GameObject drop;
    public float XP;
    public Killable.Species species;
}
