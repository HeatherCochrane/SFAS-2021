﻿using System.Collections;
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
    TextMeshProUGUI loadedLongRange;

    // Start is called before the first frame update
    void Start()
    {
        levelUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        loadedLongRange.text = Player.instance.getIfHolding();
    }
    public void addXP(float x)
    {
        XP += x;

        if(XP >= XpNeeded)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        level += 1;
        playerLevel.text = level.ToString();

        XpNeeded = level * 100;
    }
}
