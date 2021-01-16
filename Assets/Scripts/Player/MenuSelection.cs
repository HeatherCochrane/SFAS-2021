using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MenuSelection : MonoBehaviour
{
    //Each entry in this list is a row
    MenuButtons.Buttons[] screenButtons;

    GameObject[] currentRowButtons;

    Vector2 currentButton;

    int rows = 0;

    GameObject lastButton;
    GameObject highlightedButton;
    Vector2 originalScale;

    bool instance = false;
    public void setMenu(MenuButtons.Buttons[] b)
    {
        screenButtons = b;

        currentButton = new Vector2(0, 0);
        rows = screenButtons.Length - 1;

        currentRowButtons = screenButtons[0].buttons;
        lastButton = currentRowButtons[(int)currentButton.x];
        highlightedButton = currentRowButtons[(int)currentButton.x];

        originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;

        instance = true;
        highlightButton();
    }

    void highlightButton()
    {
        Debug.Log("HIGHLIGHT BUTTON: " + currentButton);
        lastButton.transform.GetComponent<RectTransform>().localScale = originalScale;

        if (currentRowButtons[(int)currentButton.x].activeSelf)
        {
            highlightedButton = currentRowButtons[(int)currentButton.x];
        }
        else
        {
            currentButton = new Vector2(0, 0);
            highlightedButton = screenButtons[0].buttons[0];
        }

        originalScale = highlightedButton.transform.GetComponent<RectTransform>().localScale;
        highlightedButton.transform.GetComponent<RectTransform>().localScale = new Vector2(originalScale.x * 1.5f, originalScale.y * 1.5f);
        lastButton = highlightedButton;
    }

    public void OnMoveY(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                Debug.Log("MOVE Y");

                float dir = ctx.ReadValue<float>();

                if (dir < 0)
                {
                    if (currentButton.y < rows)
                    {
                        currentButton += new Vector2(0, 1);
                    }
                    else
                    {
                        Debug.Log("Reached end of button rows");
                        currentButton = new Vector2(0, 0);
                    }
                }
                else if (dir > 0)
                {
                    if (currentButton.y > 0)
                    {
                        currentButton -= new Vector2(0, 1);
                    }
                    else
                    {
                        Debug.Log("Reached end of button rows");
                        currentButton = new Vector2(0, 0);
                    }
                }

                currentRowButtons = screenButtons[(int)currentButton.y].buttons;

                if (currentButton.x > currentRowButtons.Length - 1)
                {
                    currentButton = new Vector2(0, currentButton.y);
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
                Debug.Log("MOVE X");

                float dir = ctx.ReadValue<float>();

                if (dir > 0)
                {
                    if (currentButton.x < currentRowButtons.Length - 1)
                    {
                        currentButton += new Vector2(1, 0);
                    }
                    else
                    {
                        Debug.Log("Reached end of row");
                        currentButton = new Vector2(0, currentButton.y);
                    }
                }
                else if (dir < 0)
                {
                    if (currentButton.x > 0)
                    {
                        currentButton -= new Vector2(1, 0);
                    }
                    else
                    {
                        Debug.Log("Reached end of row");
                        currentButton = new Vector2(0, currentButton.y);
                    }
                }
            }

            highlightButton();
        }
    }

    

    public void OnButtonSelect(CallbackContext ctx)
    {
        if (instance)
        {
            if (ctx.performed)
            {
                Debug.Log("SELECT!");
                screenButtons[(int)currentButton.y].buttons[(int)currentButton.x].GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
