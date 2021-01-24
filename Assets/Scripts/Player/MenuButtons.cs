using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [System.Serializable]
    public class Buttons
    {
        //each of these are seperate buttons
        [SerializeField]
        public List<GameObject> buttons;
        public ScrollRect scrollBar;
    }

    [SerializeField]
    List<Buttons> menuButtonList = new List<Buttons>();


    public List<Buttons> getMenuList()
    {
        return menuButtonList;
    }
}

