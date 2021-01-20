using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject controllerControls;

    [SerializeField]
    GameObject keyboardControls;

    private void Start()
    {
        showKeyboard();
    }
    public void showController()
    {
        controllerControls.SetActive(true);
        keyboardControls.SetActive(false);
    }

    public void showKeyboard()
    {
        keyboardControls.SetActive(true);
        controllerControls.SetActive(false);
    }
}
