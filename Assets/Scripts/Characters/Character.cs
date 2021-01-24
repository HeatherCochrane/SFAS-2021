using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    CharacterData data;

    Animator anim;

    private void Start()
    {
        if (transform.childCount > 0)
        {
            anim = transform.GetChild(0).GetComponentInChildren<Animator>();
        }

        showInteractIcon(false);
    }
    public CharacterData getData()
    {
        return data;
    }

    public void showInteractIcon(bool set)
    {
        if (anim != null)
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
    }

}
