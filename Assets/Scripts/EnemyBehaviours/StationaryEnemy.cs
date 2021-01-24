using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Killable
{
    //Cooldown tracker
    bool canAttack = true;

    //Changeable in editor
    [SerializeField]
    float timeBetweenAttacks = 0;

    //How close the player has to be before attacking
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
                //Check if the player is close enough before attacking
                if (distX <= maxDistance && distY <= 1)
                {
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        //Safety check for lag times
        if (!isDead)
        {
            //Attack the player so long as they havnt been attacked recently
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
