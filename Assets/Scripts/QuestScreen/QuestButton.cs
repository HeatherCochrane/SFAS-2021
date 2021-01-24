using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    QuestScreen screen;

    Quest questData;

    public void setData(Quest d, QuestScreen s)
    {
        questData = d;
        screen = s;
    }

    public Quest getData()
    {
        return questData;
    }

    public void showQuest()
    {
        screen.showQuestData(questData);
    }
}
