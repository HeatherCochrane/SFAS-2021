using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueScreen;

    StoryData _data;

    [SerializeField]
    TextDisplay _output;
    private BeatData _currentBeat;
    private WaitForSeconds _wait;

    bool startDialogue = false;

    [SerializeField]
    Button choicePrefab;

    [SerializeField]
    GameObject choiceParent;

    bool spawnedChoices = false;

    [SerializeField]
    Button closeDialogue;

    bool dialogueFinished = false;

    private void OnEnable()
    {
        _currentBeat = null;
        _wait = new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        if(_output.IsIdle && startDialogue)
        {
            if (_currentBeat == null)
            {
                DisplayBeat(1);
            }
            else if(!spawnedChoices)
            {
                setUpChoiceButtons();
                spawnedChoices = true;
            }
        }
    }

    //Start the dialogue with the given character
    public void startNewDialogue(StoryData d)
    {
        dialogueScreen.GetComponent<Animator>().SetBool("Open", true);
        dialogueFinished = false;
        _data = d;
        startDialogue = true;
        spawnedChoices = false;
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_currentBeat != null)
            {
                if (_currentBeat.ID == 1)
                {
                    Application.Quit();
                }
                else
                {
                    DisplayBeat(1);
                }
            }
        }
        else
        {
            KeyCode alpha = KeyCode.Alpha1;
            KeyCode keypad = KeyCode.Keypad1;

            for (int count = 0; count < _currentBeat.Decision.Count; ++count)
            {
                if (alpha <= KeyCode.Alpha9 && keypad <= KeyCode.Keypad9)
                {
                    if (Input.GetKeyDown(alpha) || Input.GetKeyDown(keypad))
                    {
                        ChoiceData choice = _currentBeat.Decision[count];
                        DisplayBeat(choice.NextID);
                        break;
                    }
                }

                ++alpha;
                ++keypad;
            }
        }
    }

    void setUpChoiceButtons()
    {
        //Set up the correct amount of buttons as there is choices
        for(int i = 0; i < _currentBeat.Decision.Count; i++)
        {
            Button newChoice = Instantiate(choicePrefab);
            newChoice.GetComponent<DialogueChoice>().setChoiceNum(i);
            newChoice.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentBeat.Decision[i].DisplayText;
            newChoice.transform.SetParent(choiceParent.transform);
        }
    }
    public void pickOption(int p)
    {
        ChoiceData choice = _currentBeat.Decision[p];
        DisplayBeat(choice.NextID);

        if(choice.quest != null)
        {
            Debug.Log("Quest Started!: " + choice.quest);
            Player.instance.playerQuests.addNewQuest(choice.quest);
        }

        List<GameObject> b = new List<GameObject>();
        
        //Grab references to all spawned buttons
        for(int i =0; i < choiceParent.transform.childCount; i++)
        {
            b.Add(choiceParent.transform.GetChild(i).gameObject);
        }

        //Delete all buttons
        for(int i =0; i < b.Count; i++)
        {
            Destroy(b[i]);
        }

        b.Clear();
        spawnedChoices = false;
    }

    private void DisplayBeat(int id)
    {
        BeatData data = _data.GetBeatById(id);
        StartCoroutine(DoDisplay(data));
        _currentBeat = data;
    }

    private IEnumerator DoDisplay(BeatData data)
    {
        _output.Clear();

        while (_output.IsBusy)
        {
            yield return null;
        }

        _output.Display(data.DisplayText);

        while(_output.IsBusy)
        {
            yield return null;
        }

    }

    public void leaveDialogue()
    {
        startDialogue = false;
        _output.setIdle();
        _currentBeat = null;
        dialogueFinished = true;
        _output.Clear();

        dialogueScreen.GetComponent<Animator>().SetBool("Open", false);
    }

    public void showDialogueScreen(bool set)
    {
        dialogueScreen.SetActive(set);
    }

    public bool getIfFinished()
    {
        return dialogueFinished;
    }
}
