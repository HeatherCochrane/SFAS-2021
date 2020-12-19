using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField]
    Item data;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.instance.GetComponent<Collider2D>(), true);
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
        if (collision.transform.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            foreach (Collider2D c in transform)
            {
                if (!c.isTrigger)
                {
                   c.enabled = false;
                    Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), Player.instance.GetComponent<Collider2D>(), false);
                }
            }
        }
    }
}
