using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Killable
{
    bool canAttack = true;

    [SerializeField]
    float timeBetweenAttacks = 0;


    [SerializeField]
    int maxDistance = 0;

    float dist = 0;

    Animator anim;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        anim = GetComponent<Animator>();
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
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        canAttack = false;

        if (player.transform.position.x < transform.position.x)
        {
            anim.SetTrigger("Left");
        }
        else if (player.transform.position.x > transform.position.x)
        {
            anim.SetTrigger("Right");
        }

        Invoke("allowAttack", timeBetweenAttacks);
    }

    void allowAttack()
    {
        canAttack = true;
    }

}
