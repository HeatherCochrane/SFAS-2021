using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestRequirements : MonoBehaviour
{
    [SerializeField]
    Quest quest;


    public bool needsQuestCompleted = false;

    public bool needsQuestStarted = false;

    private void OnEnable()
    {
        if (needsQuestCompleted)
        {
            if (Player.instance.playerQuests.getCompletedQuests().Contains(quest))
            {
                destroyObject();

            }
        }
        else if (needsQuestStarted)
        {
            if (Player.instance.playerQuests.getStartedQuests().Contains(quest))
            {
                destroyObject();

            }
        }
    }


    public void destroyObject()
    {
        Destroy(this.gameObject);
    }

}
