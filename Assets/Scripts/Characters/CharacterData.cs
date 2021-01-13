using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "New character/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    //List of all the dialogues the character will have, including opening and random
    [SerializeField]
    List<StoryData> dialogue = new List<StoryData>();

    [SerializeField]
    string characterName;

    [SerializeField]
    Sprite characterSprite;

    public StoryData getDialogue(int index)
    {
        if(index != 0)
        {
            int ran = Random.Range(1, dialogue.Count);
            return dialogue[ran];
        }

        return dialogue[0];
    }

    public int getDialogueCount()
    {
        return dialogue.Count;
    }
    public string getName()
    {
        return characterName;
    }

    public Sprite getCharacterSprite()
    {
        return characterSprite;
    }
}
