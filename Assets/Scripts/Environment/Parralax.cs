﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    float startXPos = 0;
    float lengthX = 0;

    GameObject cam;

    [SerializeField]
    float parralaxEffect = 0;

    Vector2 dist;
    // Start is called before the first frame update
    void Start()
    {
        startXPos = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        cam = Player.instance.cam;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dist = cam.transform.position * parralaxEffect;
        //Update the sprite position
        transform.position = new Vector3(startXPos + dist.x, transform.position.y, transform.position.z);
    }
}