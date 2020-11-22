using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    //Experience
    float XP = 0;

    //Level
    public int level = 0;

    //XP needed to advance to the next level
    float XpNeeded = 0;

    //Player level details
    [SerializeField]
    TextMeshProUGUI playerLevel;

    [SerializeField]
    TextMeshProUGUI experience;
    // Start is called before the first frame update
    void Start()
    {
        levelUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addXP(float x)
    {
        XP += x;

        experience.text = "XP: " + XP.ToString() + " / " + XpNeeded.ToString();

        if (XP >= XpNeeded)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        XP = XP - XpNeeded;
        XP = Mathf.Clamp(XP, 0, Mathf.Infinity);

        level += 1;
        playerLevel.text = level.ToString();

        XpNeeded = level * 100;
        experience.text = "XP: " + XP.ToString() + " / " + XpNeeded.ToString();
    }
}
