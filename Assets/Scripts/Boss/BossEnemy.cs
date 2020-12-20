using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    float distX = 0;
    float distY = 0;
    int dir = 1;

    IEnumerator checkDistance()
    {
        while (true)
        {
            distX = Player.instance.transform.position.x - transform.position.x;
            distY = Player.instance.transform.position.y - transform.position.y;

            if (distX < 0)
            {
                distX *= -1;
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            if (distY < 0)
            {
                distY *= -1;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator StartAttack()
    {
        //int currentAttack = -1;

        while(!isDead)
        {
            //currentAttack += 1;

            //if(currentAttack >= bossPattern.Count)
            //{
            //    currentAttack = 0;
            //}
            //switchAttack(bossPattern[currentAttack].attack);
            decideNextAttack();
            yield return new WaitForSeconds(2);
        }
    }


    public enum Attacks { JumpAttack, IdleAttack}

    [System.Serializable]
    public struct BossAttacks
    {
        public Attacks attack;
        public int attackLength;
    }
    [SerializeField]
    List<BossAttacks> bossPattern = new List<BossAttacks>();

    bool isDead = false;

    Animator anim;

    Rigidbody2D rb;


    float maxDistance = 4;
    float minDistance = 1;

    bool isCharging = false;
    float chargeSpeed = 7.5f;
    int chargeDir = 1;
    float chargeTime = 1;

    int choice = 0;

    [SerializeField]
    GameObject projectile;
    GameObject newProjectile;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("StartAttack");
        StartCoroutine("checkDistance");
    }

    void switchAttack(Attacks a)
    {
        //resetBoolAnimations();
        //resetTriggerAnimations();

        Invoke(a.ToString(), 0);
    }

    private void FixedUpdate()
    {
        if(isCharging)
        {
            if (chargeTime <= 0)
            {
                isCharging = false;
                chargeTime = 1;
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(chargeDir * chargeSpeed, rb.velocity.y);
            }

            chargeTime -= Time.deltaTime;
        }      
    }

    void resetBoolAnimations()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(parameter.name, false);
            }
        }
    }

    void resetTriggerAnimations()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(parameter.name);
            }
        }
    }

    void decideNextAttack()
    {
        isCharging = false;
        rb.velocity = new Vector2(0, 0);

        if(distX >= maxDistance)
        {
            choice = Random.Range(0, 10);

            if (choice > 5)
            {
                JumpAttack();
            }
            else
            {
                shootProjectile();
            }
        }
        else if(distX <= minDistance)
        {
            backOff();
        }
        else
        {
            chargeAttack();
        }
    }

    void IdleAttack()
    {
        //anim.SetBool("Idle", true);
        Debug.Log("IM IDLING");
    }

    void JumpAttack()
    {
        if (dir == -1)
        {
            rb.velocity = new Vector2(-5, 12);
        }
        else
        {
            rb.velocity = new Vector2(5, 12);
        }

        Debug.Log("JUMP BITCH");
    }

    void chargeAttack()
    {
        chargeDir = dir;
        isCharging = true;
        Debug.Log("CHARGE!!");
    }

    void backOff()
    {
        if(dir == -1)
        {
            rb.velocity = new Vector2(5, 0);
        }
        else
        {
            rb.velocity = new Vector2(-5, 0);
        }

        Debug.Log("WOAH BACKING OFF!");
    }

    void shootProjectile()
    {
        newProjectile = Instantiate(projectile);
        newProjectile.GetComponent<Arrow>().setDirection(dir, 10);
        newProjectile.transform.position = this.transform.position;
    }
}
