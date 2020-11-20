﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    bool canAttack = true;

    [SerializeField]
    Player player;

    [SerializeField]
    float timeBetweenAttacks = 0;

    [SerializeField]
    int damage = 0;

    [SerializeField]
    int maxDistance = 0;

    float dist = 0;

    [SerializeField]
    int force = 0;

    [SerializeField]
    LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canAttack)
        {
            dist = Vector2.Distance(this.transform.position, player.transform.position);

            if (dist <= maxDistance)
            {
                attack();
            }
        }
    }

    public void attack()
    {
        Debug.Log("ATTACKED");

        canAttack = false;

        RaycastHit2D hit;

        //Attack which way the player is facing
        Vector2 direction = player.transform.position - this.transform.position;
        float dist = Vector2.Distance(this.transform.position, player.transform.position);

        hit = Physics2D.Raycast(transform.position, direction.normalized, dist, playerLayer);

        //Stops player hiding at the enemies location
        if(dist <= 1)
        {
            if (this.transform.position.x < player.gameObject.transform.position.x)
            {
                player.takeDamage(damage, true, force);
            }
            else
            {
                player.takeDamage(damage, false, force);
            }
        }
        else if (hit.collider)
        {
            if (hit.collider.tag == "Player")
            {
                if (this.transform.position.x < player.gameObject.transform.position.x)
                {
                    player.takeDamage(damage, true, force);
                }
                else
                {
                    player.takeDamage(damage, false, force);
                }
            }
            else
            {
                Debug.Log("Not player");
            }
        }
        else
        {
            Debug.Log("No collider");
        }

        Invoke("allowAttack", timeBetweenAttacks);
    }

    void allowAttack()
    {
        canAttack = true;
    }

}