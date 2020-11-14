using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "New character/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    StoryData dialogue;

    public StoryData getDialogue()
    {
        return dialogue;
    }

    [SerializeField]
    string Name;

    public string getName()
    {
        return Name;
    }
}
