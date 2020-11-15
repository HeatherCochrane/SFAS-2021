using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "New Quest/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public int XPGained;

    [System.Serializable]
    public struct Kills
    {
        [SerializeField]
        public Killable.Species species;
        [SerializeField]
        public int number;
    }

    [SerializeField]
    public int levelRequirement;

    [SerializeField]
    public List<Quest> questsCompleted = new List<Quest>();

    [SerializeField]
    public List<Kills> totalKillsNeeded = new List<Kills>();


    public bool checkKills(List<Kills> k)
    {
        if (k.Count > 0 && totalKillsNeeded.Count > 0)
        {
            for (int i = 0; i < k.Count; i++)
            {
                if (i < totalKillsNeeded.Count)
                {
                    if (k[i].number == totalKillsNeeded[i].number && k[i].species == totalKillsNeeded[i].species)
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool checkCompletedQuests(List<Quest> q)
    {
        if (q.Count > 0 && questsCompleted.Count > 0)
        {         
            for (int i = 0; i < q.Count; i++)
            {
                for (int j = 0; j < questsCompleted.Count; j++)
                {
                    if (q[i] == questsCompleted[j])
                    {
                        return true;
                    }
                }
                
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool checkLevelRequirement(int l)
    {
        if(l >= levelRequirement)
        {
            return true;
        }

        return false;
    }
}
