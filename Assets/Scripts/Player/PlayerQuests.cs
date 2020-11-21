using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    //List of all active quests
    [SerializeField]
    List<Quest> questsStarted = new List<Quest>();

    //Keep track of all quests completed
    [SerializeField]
    List<Quest> completedQuests = new List<Quest>();

    //The total 
    List<Quest.Kills> totalKills = new List<Quest.Kills>();

    [SerializeField]
    QuestScreen questScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewQuest(Quest n)
    {
        questsStarted.Add(n);
        questScreen.addActiveQuest(n);
    }

    public void questCompleted(Quest c)
    {
        //Add this quest to the completed list
        completedQuests.Add(c);

        //Gather the XP from that quest and add to the players xp pool
        Player.instance.levels.addXP(c.XPGained);

        //Remove this quest from the currently active quests
        questsStarted.Remove(c);

        //Check the requirements again, now looking for completed quests
        checkQuestRequirements();

        questScreen.addCompletedQuest(c);
    }

    public void speciesKilled(Killable.Species s)
    {
        bool counted = false;
        Quest.Kills t;

        //Check if this species has already been killed and add to the counter
        for (int i =0; i< totalKills.Count; i++)
        {
            if(s == totalKills[i].species)
            {
                t = totalKills[i];
                t.number += 1;
                totalKills[i] = t;
                counted = true;
                break;
            }
        }

        //If not counted, this is a new species not yet in the list
        if(!counted)
        {
            t.species = s;
            t.number = 1;
            totalKills.Add(t);
        }

        checkQuestRequirements();
    }

    void checkQuestRequirements()
    {
        //Grab all the currently started quests and check each other their requirements against stored values
        for (int i = 0; i < questsStarted.Count; i++)
        {
            //Check to ensure allrequirements have been met
            if (questsStarted[i].checkKills(totalKills) && questsStarted[i].checkCompletedQuests(completedQuests) && questsStarted[i].checkLevelRequirement(Player.instance.levels.level))
            {
                questCompleted(questsStarted[i]);
            }
        }
        
    }

    public int getTotalKills(Quest.Kills k)
    {
        for(int i =0; i < totalKills.Count; i++)
        {
            if(totalKills[i].species == k.species)
            {
                return totalKills[i].number;
            }
        }

        return 0;
    }
}
