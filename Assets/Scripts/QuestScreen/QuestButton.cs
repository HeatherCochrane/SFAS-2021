using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    QuestScreen screen;

    Quest questData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
