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

    float distX = 0;
    float distY = 0;

    IEnumerator checkDistance()
    {
        while (true)
        {
            distX = player.transform.position.x - transform.position.x;
            distY = player.transform.position.y - transform.position.y;

            if (distX < 0)
            {
                distX *= -1;
            }
            if (distY < 0)
            {
                distY *= -1;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        StartCoroutine("checkDistance");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //change to canAttack to allow attacking
        if (!isDead)
        {
            if (canAttack && !Player.instance.getIfHidden())
            {
                if (distX <= maxDistance && distY <= 1)
                {
                    Attack();
                    Debug.Log("ATTACK");
                }
            }
        }
    }

    void Attack()
    {
        canAttack = false;

        if (player.transform.position.x < transform.position.x)
        {
            changeAnimationStatesTrigger(AnimationStates.ATTACKLEFT);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            changeAnimationStatesTrigger(AnimationStates.ATTACKRIGHT);
        }

        Invoke("allowAttack", timeBetweenAttacks);
    }

    void allowAttack()
    {
        canAttack = true;
    }

}
