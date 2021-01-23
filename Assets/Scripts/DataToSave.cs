using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    List<Character> charactersTalkedTo = new List<Character>();

    List<BossScene.BossNames> bossesDefeated = new List<BossScene.BossNames>();

    List<Character> cutSceneCharacters = new List<Character>();

    public bool hasBeenTalkedTo(Character c)
    {
        if(charactersTalkedTo.Contains(c))
        {
            return true;
        }

        return false;
    }

    public void addCharacter(Character c)
    {
        charactersTalkedTo.Add(c);
    }

    public bool hasBossBeenDefeated(BossScene.BossNames b)
    {
        if (bossesDefeated.Contains(b))
        {
            return true;
        }
        
        return false;
    }

    public void addBoss(BossScene.BossNames b)
    {
        bossesDefeated.Add(b);
    }

    public void cutscenePlayed(Character c)
    {
        cutSceneCharacters.Add(c);
    }

    public bool hasCutscenePlayed(Character c)
    {
        if(cutSceneCharacters.Contains(c))
        {
            return true;
        }

        return false;
    }
}
