using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrops : MonoBehaviour
{
    public enum PlayerAbilities { DASH, WALLJUMP}

    [SerializeField]
    PlayerAbilities drop;

    public PlayerAbilities getAbility()
    {
        return drop;
    }
}
