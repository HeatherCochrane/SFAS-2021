using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    Game dialogue;
    int choice = 0;

    private void OnEnable()
    {
        dialogue = GameObject.FindObjectOfType<Game>();
    }
    public void setChoiceNum(int n)
    {
        choice = n;
    }

    public int getChoice()
    {
        return choice;
    }

    public void setChoice()
    {
        Debug.Log("Button clicked!");
        dialogue.pickOption(choice);
    }
}
