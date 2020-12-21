using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{

    [SerializeField]
    GameObject dialogueScreen;

    [SerializeField]
    TextMeshProUGUI dialogueText;

    [SerializeField]
    TextMeshProUGUI dialogueChoices;


    StoryData dialogueData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDialogue(StoryData s)
    {
        dialogueData = s;
    }
}
