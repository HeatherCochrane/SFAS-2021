using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    List<Character> charactersTalkedTo = new List<Character>();

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
}
