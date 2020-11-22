using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "New character/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    StoryData dialogue;

    [SerializeField]
    string characterName;

    [SerializeField]
    Sprite characterSprite;

    public StoryData getDialogue()
    {
        return dialogue;
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
