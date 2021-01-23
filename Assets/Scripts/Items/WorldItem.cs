using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField]
    Item data;


    [SerializeField]
    GameObject interact;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.instance.GetComponent<Collider2D>(), true);
        if (transform.childCount > 0)
        {
            anim = GetComponentInChildren<Animator>();
            showInteractIcon(false);
        }
    }

    public void setData(Item d)
    {
        data = d;
    }
    public Item getItemData()
    {
        return data;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //if (collision.transform.tag == "Ground")
        //{
        //    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        //    foreach (Collider2D c in transform)
        //    {
        //        if (!c.isTrigger)
        //        {
        //            c.enabled = false;
        //            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.instance.GetComponent<Collider2D>(), false);
        //        }
        //    }
        //}

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
