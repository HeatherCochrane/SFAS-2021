using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public void addNewQuest(Quest n)
    {
        questsStarted.Add(n);
        questScreen.addActiveQuest(n);
    }

    public void questCompleted(Quest c)
    {
        //Add this quest to the completed list
        completedQuests.Add(c);

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

    public void bossesKilled(BossScene.BossNames b)
    {
        Player.instance.data.addBoss(b);
        checkQuestRequirements();
    }

    void checkQuestRequirements()
    {
        //Grab all the currently started quests and check each other their requirements against stored values
        for (int i = 0; i < questsStarted.Count; i++)
        {
            //Check to ensure all requirements have been met
            if (questsStarted[i].checkKills(totalKills) && questsStarted[i].checkCompletedQuests(completedQuests) && questsStarted[i].checkBossKilled())
            {
                Debug.Log("QUEST COMPLETED!");
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


    public List<Quest> getStartedQuests()
    {
        return questsStarted;
    }

    public List<Quest> getCompletedQuests()
    {
        return completedQuests;
    }
}
