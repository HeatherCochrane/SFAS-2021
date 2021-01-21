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

    ScrollRect currentScrollBar;

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

        currentScrollBar = null;
    }

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

            Player.instance.audioHandler.playHighlightButton();
        }

    }

    public void highlightPointerButton()
    {
        if (instance)
        {
            lastButton.transform.GetComponent<RectTransform>().localScale = originalScale;

            highlightedButton = currentRowButtons[(int)currentButton.x];

            originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;
            highlightedButton.transform.GetComponent<RectTransform>().localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 1.2f, 1);
            lastButton = highlightedButton;


            Player.instance.audioHandler.playHighlightButton();
        }

    }

    public void OnMoveY(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                float dir = ctx.ReadValue<float>();

                if (dir < 0)
                {
                    if (rows == 0 && currentButton.x < currentRowButtons.Count - 1)
                    {
                        currentButton += new Vector2(1, 0);
                    }
                    else if (currentButton.y + 1 <= rows)
                    {
                        currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                        bool rowHasActive = false;
                        if (currentButton.x < screenButtons[(int)currentButton.y + 1].buttons.Count)
                        {
                            if (screenButtons[(int)currentButton.y + 1].buttons[(int)currentButton.x].activeSelf && (currentButton.y + 1 != rows - 1))
                            {
                                currentButton = new Vector2(currentButton.x, currentButton.y + 1);
                                rowHasActive = true;
                            }
                            else
                            {
                                if (Player.instance.uiHandler.getInMenu(UIHandler.Menus.TRADER))
                                {
                                    if (currentButton.x >= 3)
                                    {
                                        Debug.Log("RIGHT");
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
                                        Debug.Log("LEFT");
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
                                    Debug.Log("RIGHT");
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
                                    Debug.Log("LEFT");
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
                                Debug.Log("LEFT");
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
                                        Debug.Log("RIGHT");
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
                                        Debug.Log("LEFT");
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
                                    Debug.Log("RIGHT");
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
                                    Debug.Log("LEFT");
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
                        if (currentRowButtons[(int)currentButton.x + 1].activeSelf)
                        {
                            currentButton += new Vector2(1, 0);
                        }
                        else
                        {
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
