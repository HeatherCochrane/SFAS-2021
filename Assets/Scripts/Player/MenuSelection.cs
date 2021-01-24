using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.EventSystems;

public class MenuSelection : MonoBehaviour
{
    //Each entry in this list is a row
    [SerializeField]
    List<MenuButtons.Buttons> screenButtons = new List<MenuButtons.Buttons>();

    [SerializeField]
    List<GameObject> currentRowButtons = new List<GameObject>();

    Vector2 currentButton;

    int rows = 0;

    [SerializeField]
    GameObject lastButton;
    [SerializeField]
    GameObject highlightedButton;
    Vector3 originalScale;

    bool instance = false;

    bool mouseOnButt = false;
    GameObject mouseButton;


    public void setMenu(List<MenuButtons.Buttons> b)
    {
        if (b.Count >= 1)
        {
            screenButtons = b;

            currentButton = new Vector2(0, 0);
            rows = screenButtons.Count - 1;

            currentRowButtons = screenButtons[0].buttons;
            highlightedButton = currentRowButtons[(int)currentButton.x];
            lastButton = highlightedButton;

            originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;

            instance = true;
            highlightButton();
        }
    }

    public void setInstance()
    {
        instance = false;
        if (highlightedButton != null)
        {
            highlightedButton.transform.GetComponent<RectTransform>().localScale = originalScale;
        }

        lastButton = null;
        highlightedButton = null;
    }

    //Shiw which button is highlighted by making it bigger
    public void highlightButton()
    {
        if (instance)
        {
            lastButton.transform.GetComponent<RectTransform>().localScale = originalScale;

            if (currentRowButtons[(int)currentButton.x].activeSelf)
            {
                highlightedButton = currentRowButtons[(int)currentButton.x];
            }
            else
            {
                currentButton = new Vector2(0, 0);
                highlightedButton = screenButtons[0].buttons[0];
                currentRowButtons = screenButtons[0].buttons;
            }

            originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;
            highlightedButton.transform.GetComponent<RectTransform>().localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 1.2f, 1);
            lastButton = highlightedButton;

            if (highlightedButton.activeSelf)
            {
                Player.instance.audioHandler.playHighlightButton();
            }
        }

    }

    //Shiw which button is highlighted by making it bigger
    public void highlightPointerButton()
    {
        if (instance)
        {
            lastButton.transform.GetComponent<RectTransform>().localScale = originalScale;

            highlightedButton = currentRowButtons[(int)currentButton.x];

            originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;
            highlightedButton.transform.GetComponent<RectTransform>().localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 1.2f, 1);
            lastButton = highlightedButton;


            if (highlightedButton.activeSelf)
            {
                Player.instance.audioHandler.playHighlightButton();
            }
        }

    }

    //Move up and down through the list of buttons avaliable
    //This function is handled by input from the keyboard or analog stick and allows the player to cycle through the buttons and find the one they are wanting to
    public void OnMoveY(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                float dir = ctx.ReadValue<float>();

                if (dir < 0)
                {
                    //If only one row, moving down or up will instead move left or right
                    if (rows == 0 && currentButton.x < currentRowButtons.Count - 1)
                    {
                        currentButton += new Vector2(1, 0);
                    }
                    //If still within the bounds of the button list
                    else if (currentButton.y + 1 <= rows)
                    {
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                        bool rowHasActive = false;
                        if (currentButton.x < screenButtons[(int)currentButton.y + 1].buttons.Count)
                        {
                            //If button is avaliable directly below the current button and it is not the last row (as this tends to have more offset buttons)
                            if (screenButtons[(int)currentButton.y + 1].buttons[(int)currentButton.x].activeSelf && (currentButton.y + 1 != rows - 1))
                            {
                                currentButton = new Vector2(currentButton.x, currentButton.y + 1);
                                rowHasActive = true;
                            }
                            else
                            {
                                //Find the active button within that row and set it to active depending on which direction the player is coming from
                                if (Player.instance.uiHandler.getInMenu(UIHandler.Menus.TRADER))
                                {
                                    if (currentButton.x >= 3)
                                    {
                                        for (int i = screenButtons[(int)currentButton.y + 1].buttons.Count - 1; i > 0; i--)
                                        {
                                            if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                            {
                                                rowHasActive = true;
                                                currentButton = new Vector2(i, currentButton.y + 1);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < screenButtons[(int)currentButton.y + 1].buttons.Count; i++)
                                        {
                                            if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                            {
                                                rowHasActive = true;
                                                currentButton = new Vector2(i, currentButton.y + 1);
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {

                            if (Player.instance.uiHandler.getInMenu(UIHandler.Menus.TRADER))
                            {
                                if (currentButton.x >= 3)
                                {
                                    for (int i = screenButtons[(int)currentButton.y + 1].buttons.Count - 1; i > 0; i--)
                                    {
                                        if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                        {
                                            rowHasActive = true;
                                            currentButton = new Vector2(i, currentButton.y + 1);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < screenButtons[(int)currentButton.y + 1].buttons.Count; i++)
                                    {
                                        if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                        {
                                            rowHasActive = true;
                                            currentButton = new Vector2(i, currentButton.y + 1);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < screenButtons[(int)currentButton.y + 1].buttons.Count; i++)
                                {
                                    if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                    {
                                        rowHasActive = true;
                                        currentButton = new Vector2(i, currentButton.y + 1);
                                        break;
                                    }
                                }
                            }
                        }

                        if (!rowHasActive)
                        {
                            currentButton = new Vector2(0, 0);
                        }

                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                    }
                    else
                    {
                        currentButton = new Vector2(0, 0);
                        currentRowButtons = screenButtons[0].buttons;
                    }

                }
                else if (dir > 0)
                {
                    if (rows == 0 && currentButton.x > 0)
                    {
                        currentButton -= new Vector2(1, 0);
                    }
                    else if (currentButton.y - 1 >= 0)
                    {
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                        bool rowHasActive = false;
                        if (currentButton.x < screenButtons[(int)currentButton.y - 1].buttons.Count)
                        {
                            if (screenButtons[(int)currentButton.y - 1].buttons[(int)currentButton.x].activeSelf && currentButton.y - 1 != rows)
                            {
                                currentButton = new Vector2(currentButton.x, currentButton.y - 1);
                                rowHasActive = true;
                            }
                            else
                            {
                                if (Player.instance.uiHandler.getInMenu(UIHandler.Menus.TRADER))
                                {
                                    if (currentButton.x >= 3)
                                    {
                                        for (int i = screenButtons[(int)currentButton.y - 1].buttons.Count - 1; i > 0; i--)
                                        {
                                            if (screenButtons[(int)currentButton.y - 1].buttons[i].activeSelf)
                                            {
                                                rowHasActive = true;
                                                currentButton = new Vector2(i, currentButton.y - 1);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < screenButtons[(int)currentButton.y - 1].buttons.Count; i++)
                                        {
                                            if (screenButtons[(int)currentButton.y - 1].buttons[i].activeSelf)
                                            {
                                                rowHasActive = true;
                                                currentButton = new Vector2(i, currentButton.y - 1);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Player.instance.uiHandler.getInMenu(UIHandler.Menus.TRADER))
                            {
                                if (currentButton.x >= 3)
                                {
                                    for (int i = screenButtons[(int)currentButton.y - 1].buttons.Count - 1; i > 0; i--)
                                    {
                                        if (screenButtons[(int)currentButton.y - 1].buttons[i].activeSelf)
                                        {
                                            rowHasActive = true;
                                            currentButton = new Vector2(i, currentButton.y - 1);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < screenButtons[(int)currentButton.y - 1].buttons.Count; i++)
                                    {
                                        if (screenButtons[(int)currentButton.y - 1].buttons[i].activeSelf)
                                        {
                                            rowHasActive = true;
                                            currentButton = new Vector2(i, currentButton.y - 1);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (!rowHasActive)
                        {
                            currentButton = new Vector2(0, 0);
                        }

                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                    }
                    else
                    {
                        currentButton = new Vector2(0, 0);
                        currentRowButtons = screenButtons[0].buttons;
                    }

                }

                highlightButton();
            }
        }
    }


    //Move left and right through the list of buttons avaliable
    //This function is handled by input from the keyboard or analog stick and allows the player to cycle through the buttons and find the one they are wanting to
    public void OnMoveX(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                float dir = ctx.ReadValue<float>();

                if (dir > 0)
                {
                    if (currentButton.x < currentRowButtons.Count - 1)
                    {
                        //If button to the right is avaliable
                        if (currentRowButtons[(int)currentButton.x + 1].activeSelf)
                        {
                            currentButton += new Vector2(1, 0);
                        }
                        else
                        {//Find the next active button the the next row
                            currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                            bool rowHasActive = false;

                            for (int i = (int)currentButton.x; i < screenButtons[(int)currentButton.y].buttons.Count; i++)
                            {
                                if (screenButtons[(int)currentButton.y].buttons[i].activeSelf)
                                {
                                    rowHasActive = true;
                                    currentButton = new Vector2(i, currentButton.y);
                                    break;
                                }
                            }

                            if (!rowHasActive)
                            {
                                if (currentButton.y + 1 <= rows)
                                {
                                    currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                                    for (int i = 0; i < screenButtons[(int)currentButton.y + 1].buttons.Count; i++)
                                    {
                                        if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                                        {
                                            rowHasActive = true;
                                            currentButton = new Vector2(i, currentButton.y + 1);
                                            break;
                                        }
                                    }


                                    if (!rowHasActive)
                                    {
                                        currentButton = new Vector2(0, 0);
                                    }

                                    currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                                }
                            }

                            currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                        }
                    }
                    else if (currentButton.y + 1 <= rows)
                    {
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                        bool rowHasActive = false;

                        for (int i = 0; i < screenButtons[(int)currentButton.y + 1].buttons.Count; i++)
                        {
                            if (screenButtons[(int)currentButton.y + 1].buttons[i].activeSelf)
                            {
                                rowHasActive = true;
                                currentButton = new Vector2(i, currentButton.y + 1);
                                break;
                            }
                        }


                        if (!rowHasActive)
                        {
                            currentButton = new Vector2(0, 0);
                        }

                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                    }

                    currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                }

                else if (dir < 0)
                {
                    if (currentButton.x > 0)
                    {
                        if (currentRowButtons[(int)currentButton.x - 1].activeSelf)
                        {
                            currentButton -= new Vector2(1, 0);
                        }
                        else
                        {
                            currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                            bool rowHasActive = false;

                            for (int i = (int)currentButton.x; i > 0; i--)
                            {
                                if (screenButtons[(int)currentButton.y].buttons[i].activeSelf)
                                {
                                    rowHasActive = true;
                                    currentButton = new Vector2(i, currentButton.y);
                                    break;
                                }
                            }


                            if (!rowHasActive)
                            {
                                currentButton = new Vector2(0, 0);
                            }

                            currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                        }

                    }
                    else if (currentButton.y - 1 >= 0)
                    {
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                        bool rowHasActive = false;

                        for (int i = (int)currentButton.x; i > 0; i--)
                        {
                            if (screenButtons[(int)currentButton.y - 1].buttons[i].activeSelf)
                            {
                                rowHasActive = true;
                                currentButton = new Vector2(i, currentButton.y - 1);
                                break;
                            }
                        }


                        if (!rowHasActive)
                        {
                            currentButton = new Vector2(0, 0);
                        }

                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                    }
                    else
                    {
                        currentButton = new Vector2(0, currentButton.y);
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;
                    }

                }

                highlightButton();
            }
        }
    }



    public void OnButtonSelect(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                //If the input is specifically fom the mouse, ensure the mouse is still over the button before selecting it
                if (ctx.control.ToString() is "Button:/Mouse/leftButton")
                {
                    if (mouseButton == highlightedButton)
                    {
                        if (highlightedButton.activeSelf)
                        {
                            screenButtons[(int)currentButton.y].buttons[(int)currentButton.x].GetComponent<Button>().onClick.Invoke();

                            updateCurrentButton();

                            Player.instance.audioHandler.playButtonTap();
                        }
                    }
                }
                else
                {
                    if (highlightedButton.activeSelf)
                    {
                        screenButtons[(int)currentButton.y].buttons[(int)currentButton.x].GetComponent<Button>().onClick.Invoke();

                        updateCurrentButton();

                        Player.instance.audioHandler.playButtonTap();
                    }
                }
            }
        }
    }

    //Used when the selected button is inactive or no longer avaliable
    public void updateCurrentButton()
    {
        if (highlightedButton == null || !highlightedButton.activeSelf)
        {
            for (int i = 0; i < screenButtons.Count; i++)
            {
                for (int j = 0; j < screenButtons[i].buttons.Count; j++)
                {
                    if (screenButtons[i].buttons[j].activeSelf)
                    {
                        currentButton = new Vector2(j, i);
                        currentRowButtons = screenButtons[i].buttons;
                        highlightButton();
                        return;
                    }
                }
            }
        }
    }

    //Used by the mouse to set the active button
    public void setActiveButton(GameObject b)
    {
        for(int i =0; i < screenButtons.Count; i++)
        {
           for(int j = 0; j < screenButtons[i].buttons.Count; j++)
            {
                if(screenButtons[i].buttons[j].gameObject == b)
                {
                    currentButton = new Vector2(j, i);
                    currentRowButtons = screenButtons[i].buttons;

                    highlightPointerButton();
                    return;
                }
            }
        }

    }

    public void setMouseOnButton(bool set, GameObject m)
    {
        mouseOnButt = set;
        mouseButton = m;

    }
}
