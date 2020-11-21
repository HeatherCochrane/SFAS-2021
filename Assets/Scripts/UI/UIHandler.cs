using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public enum Menus { PLAYERUI, INVENTORY, QUESTS, TRADER, DIALOGUE};
    [System.Serializable]
    public struct Menu
    {
        public Menus name;
        public GameObject obj;
    }

    [SerializeField]
    List<Menu> allActiveMenus = new List<Menu>();


    Menus currentMenu;
    Menus currentMenu2;

    // Start is called before the first frame update
    void Start()
    {
        changeMenu(Menus.PLAYERUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMenu(Menus n)
    {
        currentMenu2 = n;

        for(int i =0; i < allActiveMenus.Count; i++)
        {
            if (allActiveMenus[i].name == n)
            {
                allActiveMenus[i].obj.SetActive(true);
                currentMenu = n;
            }
            else
            {
                allActiveMenus[i].obj.SetActive(false);
            }
        }
    }

    public void changeDoubleMenu(Menus n, Menus m)
    {
        for (int i = 0; i < allActiveMenus.Count; i++)
        {
            if (allActiveMenus[i].name == n || allActiveMenus[i].name == m)
            {
                allActiveMenus[i].obj.SetActive(true);
                currentMenu = n;
                currentMenu2 = m;
            }
            else
            {
                allActiveMenus[i].obj.SetActive(false);
            }
        }
    }

    public bool getInMenu(Menus m)
    {
        if(m == currentMenu || m == currentMenu2)
        {
            return true;
        }

        return false;
    }

    public GameObject getMenuObject(Menus m)
    {
        for(int i =0; i < allActiveMenus.Count; i++)
        {
            if(allActiveMenus[i].name == m)
            {
                return allActiveMenus[i].obj;
            }
        }

        return null;
    }

}
