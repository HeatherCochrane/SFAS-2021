using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Killable", menuName = "New Killable/Killable", order = 1)]
public class KillableData : ScriptableObject
{
    [System.Serializable]
    public struct Drops
    {
        [SerializeField]
        public GameObject drop;
        [SerializeField]
        public int amount;
    }
    public int health;
    public List<Drops> drop = new List<Drops>();
    public Killable.Species species;
}
