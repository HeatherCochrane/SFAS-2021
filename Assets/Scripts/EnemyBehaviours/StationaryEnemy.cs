using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Enemy
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
        player = Player.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //change to canAttack to allow attacking
        if (!isDead)
        {
            if (canAttack && !Player.instance.getIfHidden())
            {
                dist = Vector2.Distance(this.transform.position, player.transform.position);

                if (dist <= maxDistance)
                {
                    attack();
                }
            }
        }
    }

    public void attack()
    {
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
                Player.instance.playerStatus.takeDamage(damage, true, force);
            }
            else
            {
                Player.instance.playerStatus.takeDamage(damage, false, force);
            }
        }
        else if (hit.collider)
        {
            if (hit.collider.tag == "Player")
            {
                if (this.transform.position.x < player.gameObject.transform.position.x)
                {
                    Player.instance.playerStatus.takeDamage(damage, true, force);
                }
                else
                {
                    Player.instance.playerStatus.takeDamage(damage, false, force);
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
