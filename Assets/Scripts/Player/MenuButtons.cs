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
        public GameObject[] buttons;
    }

    [SerializeField]
    Buttons[] menuButtonList;

    [SerializeField]
    GameObject[] menuButtonObjects;

    public Buttons[] getMenuList()
    {
        return menuButtonList;
    }

    private void OnEnable()
    {
        updateButtons();
    }

    private void OnDisable()
    {
        Player.instance.menus.setInstance();
    }
    public void updateButtons()
    {
        
        ////check for new buttons, if so just add onto the end they will always be vertical and in single columns
        //Button[] totalButts = GetComponentsInChildren<Button>();
        //menuButtonObjects = new GameObject[totalButts.Length];

        //for (int i = 0; i < totalButts.Length; i++)
        //{
        //    menuButtonObjects[i] = totalButts[i].gameObject;
        //}


        ////How many objects exist
        //int currentChildren = menuButtonObjects.Length;
        ////How many is found
        //int currentButtons = 0;


        //foreach (Button obj in totalButts)
        //{
        //    foreach (Buttons b in menuButtonList)
        //    {
        //        foreach (GameObject button in b.buttons)
        //        {
        //            if (button == obj.gameObject)
        //            {
        //                currentButtons += 1;
        //            }
        //        }
        //    }
        //}

        //Debug.Log("Current: " + currentChildren + " Current Buttons: " + currentButtons);

        //if (currentChildren != currentButtons)
        //{
        //    //If buttons have been destroyed
        //    if (currentChildren > currentButtons)
        //    {
        //        System.Array.Resize(ref menuButtonList, currentButtons);
        //        Debug.Log(menuButtonList.Length);
        //    }
        //    //If new buttons have been added
        //    else if (currentButtons > currentChildren)
        //    {
        //        System.Array.Resize(ref menuButtonList, currentChildren);
        //        Debug.Log(menuButtonList.Length);
        //    }
        //}
    }
}

