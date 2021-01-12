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

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //change to canAttack to allow attacking
        if (!isDead)
        {
            if (canAttack)
            {
                if (distX <= maxDistance && distY <= 1)
                {
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        if (!isDead)
        {
            if (!Player.instance.playerStatus.getRecentlyDamaged())
            {
                canAttack = false;

                if (Player.instance.transform.position.x < transform.position.x)
                {
                    changeAnimationStatesTrigger(AnimationStates.ATTACKLEFT);
                }
                else if (Player.instance.transform.position.x > transform.position.x)
                {
                    changeAnimationStatesTrigger(AnimationStates.ATTACKRIGHT);
                }

                Invoke("allowAttack", timeBetweenAttacks);
            }
        }
        else
        {
            CancelInvoke();
        }
    }

    void allowAttack()
    {
        canAttack = true;
    }

}
