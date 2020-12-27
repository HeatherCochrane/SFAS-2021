using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScreen : MonoBehaviour
{
    [SerializeField]
    GameObject questScreen;


    [SerializeField]
    GameObject questButtonPrefab;

    GameObject newButton;

    Quest currentQuestToggled;

    [SerializeField]
    GameObject questParent;

    [SerializeField]
    GameObject finishedQuestParent;

    [SerializeField]
    GameObject infoBox;

    [SerializeField]
    TextMeshProUGUI stringPrefab;

    List<GameObject> startedQuests = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        questScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void toggleQuestScreen()
    {
        if(questScreen.activeSelf)
        {
            questScreen.SetActive(false);
            Player.instance.setMovement(false);
        }
        else
        {
            questScreen.SetActive(true);
            clearInfoBox();
            Player.instance.setMovement(true);
        }
    }

    public void addActiveQuest(Quest data)
    {
        newButton = Instantiate(questButtonPrefab);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = data.questName;
        newButton.transform.SetParent(questParent.transform);
        newButton.transform.SetSiblingIndex(0);
        newButton.GetComponent<QuestButton>().setData(data, this);

        startedQuests.Add(newButton);
    }

    public void addCompletedQuest(Quest data)
    {
        removeQuest(data);

        newButton = Instantiate(questButtonPrefab);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = data.questName;
        newButton.transform.SetParent(finishedQuestParent.transform);
        newButton.GetComponent<QuestButton>().setData(data, this);
    }

    void removeQuest(Quest data)
    {
        for(int i =0; i < startedQuests.Count; i++)
        {
            if(startedQuests[i].GetComponent<QuestButton>().getData() == data)
            {
                GameObject b = startedQuests[i];
                startedQuests.RemoveAt(i);
                Destroy(b);
                break;
            }
        }
    }

    public void showQuestData(Quest data)
    {
        clearInfoBox();

        currentQuestToggled = data;

        getQuestRequirements();

    }

    void getQuestRequirements()
    {
        if (currentQuestToggled.totalKillsNeeded.Count > 0)
        {         
            for(int i =0; i < currentQuestToggled.totalKillsNeeded.Count; i++)
            {
                if(currentQuestToggled.totalKillsNeeded[i].species == Killable.Species.ENEMY)
                {
                    
                    string enemyString = "Enemies to kill: " + Player.instance.playerQuests.getTotalKills(currentQuestToggled.totalKillsNeeded[i]) + "/" + currentQuestToggled.totalKillsNeeded[i].number.ToString();
                    TextMeshProUGUI newString = Instantiate(stringPrefab);
                    newString.text = enemyString;
                    newString.transform.SetParent(infoBox.transform);

                }
                else if(currentQuestToggled.totalKillsNeeded[i].species == Killable.Species.SHEEP)
                {
                    string sheepString = "Sheep to kill: " + Player.instance.playerQuests.getTotalKills(currentQuestToggled.totalKillsNeeded[i]) + "/" + currentQuestToggled.totalKillsNeeded[i].number.ToString();
                    TextMeshProUGUI newString = Instantiate(stringPrefab);
                    newString.text = sheepString;
                    newString.transform.SetParent(infoBox.transform);
                }
            }
        }

        if (currentQuestToggled.questsCompleted.Count > 0)
        {
            string questToComplete = "Quest needed: ";

            for (int i = 0; i < currentQuestToggled.questsCompleted.Count; i++)
            {
                questToComplete += currentQuestToggled.questsCompleted[i].name + "\n";
                TextMeshProUGUI newString = Instantiate(stringPrefab);
                newString.text = questToComplete;
                newString.transform.SetParent(infoBox.transform);
            }
        }

        if(currentQuestToggled.bossName != BossScene.BossNames.NONE)
        {
            string sheepString = "Boss to kill: " + currentQuestToggled.bossName;
            TextMeshProUGUI newString = Instantiate(stringPrefab);
            newString.text = sheepString;
            newString.transform.SetParent(infoBox.transform);
        }
    }

    void clearInfoBox()
    {
        for(int i =0; i < infoBox.transform.childCount; i++)
        {
            Destroy(infoBox.transform.GetChild(i).gameObject);
        }
    }

}
