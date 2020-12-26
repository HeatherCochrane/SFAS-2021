using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    List<Character> charactersTalkedTo = new List<Character>();

    List<BossEnemy> bossesDefeated = new List<BossEnemy>();
    public bool hasBeenTalkedTo(Character c)
    {
        foreach(Character character in charactersTalkedTo)
        {
            if(character == c)
            {
                return true;
            }
        }

        return false;
    }

    public void addCharacter(Character c)
    {
        charactersTalkedTo.Add(c);
    }

    public bool hasBossBeenDefeated(BossEnemy b)
    {
        foreach(BossEnemy boss in bossesDefeated)
        {
            if(boss == b)
            {
                return true;
            }
        }

        return false;
    }

    public void addBoss(BossEnemy b)
    {
        bossesDefeated.Add(b);
    }
}
