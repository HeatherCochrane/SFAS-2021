using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    string scene;

    [SerializeField]
    GameObject interact;
    // Start is called before the first frame update
    void Start()
    {
        showInteractIcon(false);
    }

    public void showInteractIcon(bool set)
    {
        interact.SetActive(set);
    }

    public string getSceneName()
    {
        return scene;
    }
}
