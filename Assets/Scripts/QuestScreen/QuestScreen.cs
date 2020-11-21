using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScreen : MonoBehaviour
{
    [SerializeField]
    GameObject questButtonPrefab;

    GameObject newButton;

    Quest currentQuestToggled;

    [SerializeField]
    GameObject questParent;


    [SerializeField]
    GameObject infoBox;

    [SerializeField]
    TextMeshProUGUI stringPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addActiveQuest(Quest data)
    {
        newButton = Instantiate(questButtonPrefab);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = data.name;
        newButton.transform.SetParent(questParent.transform);
        newButton.GetComponent<QuestButton>().setData(data, this);
    }

    public void showQuestData(Quest data)
    {
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
                    
                    string enemyString = "Enemies to kill: " + currentQuestToggled.totalKillsNeeded[i].number.ToString();
                    TextMeshProUGUI newString = Instantiate(stringPrefab);
                    newString.text = enemyString;
                    newString.transform.SetParent(infoBox.transform);

                }
                else if(currentQuestToggled.totalKillsNeeded[i].species == Killable.Species.SHEEP)
                {
                    string sheepString = "Sheep to kill: " + currentQuestToggled.totalKillsNeeded[i].number.ToString();
                    TextMeshProUGUI newString = Instantiate(stringPrefab);
                    newString.text = sheepString;
                    newString.transform.SetParent(infoBox.transform);
                }
            }
        }

        if (currentQuestToggled.questsCompleted.Count > 0)
        {
            for (int i = 0; i < currentQuestToggled.questsCompleted.Count; i++)
            {
                string questToComplete = "Quest needed: " + currentQuestToggled.questsCompleted[i].name;
                TextMeshProUGUI newString = Instantiate(stringPrefab);
                newString.text = questToComplete;
                newString.transform.SetParent(infoBox.transform);
            }
        }

     
    }

}
