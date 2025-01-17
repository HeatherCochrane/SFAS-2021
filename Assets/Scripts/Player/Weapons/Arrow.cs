﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int dir = 0;
    float speed = 0.3f;
    float range = 0;

    [SerializeField]
    ParticleSystem effect;

    ParticleSystem e;

    public void setDirection(int d, float r)
    {
        dir = d;
        range = r / 20;

        if(dir == 1)
        {
            transform.localScale = new Vector2(-0.3f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector2(0.3f, 0.3f);
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
