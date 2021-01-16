using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [System.Serializable]
    public class Buttons
    {
        //each of these are seperate buttons
        [SerializeField]
        public GameObject[] buttons;
    }

    [SerializeField]
    Buttons[] menuButtonList;

    public Buttons[] getMenuList()
    {
        return menuButtonList;
    }
}
