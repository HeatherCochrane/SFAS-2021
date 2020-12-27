using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    List<Character> charactersTalkedTo = new List<Character>();

    List<BossScene.BossNames> bossesDefeated = new List<BossScene.BossNames>();

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

    public bool hasBossBeenDefeated(BossScene.BossNames b)
    {
        foreach(BossScene.BossNames boss in bossesDefeated)
        {
            if(boss == b)
            {
                return true;
            }
        }

        return false;
    }

    public void addBoss(BossScene.BossNames b)
    {
        bossesDefeated.Add(b);
    }
}
