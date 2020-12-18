using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int dir = 0;
    float speed = 0.4f;
    void Start()
    {
        Invoke("destroyArrow", 1);
    }

    public void setDirection(int d)
    {
        dir = d;
        if(dir == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void FixedUpdate()
    {
        this.transform.position += new Vector3(speed * dir, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Killable" || collision.transform.tag == "Hazard")
        {
            Invoke("destroyArrow", 0.3f);
        }
    }

    void destroyArrow()
    {
        Destroy(this.gameObject);
    }
}
