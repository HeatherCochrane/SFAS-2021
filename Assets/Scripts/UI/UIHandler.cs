using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public enum Menus { PLAYERUI, INVENTORY, QUESTS, TRADER, DIALOGUE, START, MAP};
    [System.Serializable]
    public struct Menu
    {
        public Menus name;
        public GameObject obj;
        public MenuButtons buttons;
        public bool priority;
    }

    [SerializeField]
    List<Menu> allActiveMenus = new List<Menu>();


    Menus currentMenu;
    Menus currentMenu2;

    GameObject currentMap;

    MenuSelection currentMenuButtons;

    bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        changeMenu(Menus.START);
        inMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InGame()
    {
        changeMenu(Menus.PLAYERUI);
    }

    public void changeMenu(Menus n)
    {
        currentMenu2 = n;

        //if not switching to player ui and already in a menu
        if(n != Menus.PLAYERUI && currentMenu != Menus.PLAYERUI)
        {
            //DONT
            Debug.Log("DONT SWITCH ALREADY IN MENU");
        }
        else
        {
            //if switching to a diff menu and in player ui
            //GO AHEAD

            for (int i = 0; i < allActiveMenus.Count; i++)
            {
                if (allActiveMenus[i].obj != null)
                {
                    if (allActiveMenus[i].name == n)
                    {
                        allActiveMenus[i].obj.SetActive(true);
                        currentMenu = n;

                        if (allActiveMenus[i].buttons != null)
                        {
                            Player.instance.menus.setMenu(allActiveMenus[i].buttons.getMenuList());
                        }

                    }
                    else
                    {
                        allActiveMenus[i].obj.SetActive(false);
                    }
                }
            }
        }
           
        

        if (currentMenu == Menus.PLAYERUI)
        {
            Player.instance.menus.setInstance();
            inMenu = false;
        }
        else
        {
            inMenu = true;
        }

    }

    public bool GetInMenu()
    {
        return inMenu;
    }

    public void setCurrentMap(GameObject m)
    {
        currentMap = m;
        currentMap.SetActive(false);

        for(int i =0; i < allActiveMenus.Count; i++)
        {
            if(allActiveMenus[i].name == Menus.MAP)
            {
                Menu newM = allActiveMenus[i];
                newM.obj = m;
                allActiveMenus[i] = newM;
            }
        }
    }

    public void hideMap()
    {
        if (currentMap != null)
        {
            currentMap.SetActive(false);
        }

        changeMenu(Menus.PLAYERUI);
    }

    public bool getMapActive()
    {
        if (currentMap != null)
        {
            if (currentMap.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    public void changeDoubleMenu(Menus n, Menus m)
    {
        bool pickPriority = false;

        //if not switching to playerUI and also not in the UI
        if (n != Menus.PLAYERUI && currentMenu != Menus.PLAYERUI)
        {
            //DONT
            Debug.Log("DONT SWITCH ALREADY IN MENU");
        }
        else
        {
            for (int i = 0; i < allActiveMenus.Count; i++)
            {
                if (allActiveMenus[i].obj != null)
                {
                    if (allActiveMenus[i].name == n)
                    {
                        allActiveMenus[i].obj.SetActive(true);
                        currentMenu = n;
                       
                    }
                    else if (allActiveMenus[i].name == m)
                    {
                        allActiveMenus[i].obj.SetActive(true);
                        currentMenu2 = m;
                        if (allActiveMenus[i].buttons != null)
                        {
                            if (allActiveMenus[i].priority)
                            {
                                Player.instance.menus.setMenu(allActiveMenus[i].buttons.getMenuList());
                                pickPriority = true;
                            }
                        }
                    }
                    else
                    {
                        allActiveMenus[i].obj.SetActive(false);
                    }
                }
            }
        }
        
        if((currentMenu == Menus.PLAYERUI && currentMenu2 == Menus.INVENTORY) || currentMenu == Menus.INVENTORY && currentMenu2 == Menus.TRADER)
        {
            inMenu = true;
        }
        else
        {
            inMenu = false;
            Player.instance.menus.setInstance();
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
