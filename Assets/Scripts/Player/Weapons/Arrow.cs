using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int dir = 0;
    float speed = 0.4f;
    float range = 0;

    [SerializeField]
    ParticleSystem effect;

    ParticleSystem e;
    void Start()
    {
       
    }

    public void setDirection(int d, float r)
    {
        dir = d;
        range = r / 20;

        if(dir == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        Invoke("destroyArrow", range);
    }

    void FixedUpdate()
    {
        this.transform.position += new Vector3(speed * dir, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        destroyArrow();
    }

    void destroyArrow()
    {
        e = Instantiate(effect);
        e.transform.position = transform.position;
        Destroy(this.gameObject);
    }

}
