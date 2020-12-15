using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    string scene;

    [SerializeField]
    GameObject interact;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = interact.GetComponentInChildren<Animator>();
        anim.SetTrigger("Exit");
    }

    public void showInteractIcon(bool set)
    {
        if (set)
        {
            anim.SetTrigger("Enter");
        }
        else
        {
            anim.SetTrigger("Exit");
        }
    }

    public string getSceneName()
    {
        return scene;
    }
}
