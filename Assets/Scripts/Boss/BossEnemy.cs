using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Killable
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
        int currentAttack = -1;

        while(!isDead)
        {
            currentAttack += 1;

            if(currentAttack >= bossPattern.Count)
            {
                currentAttack = 0;
            }
            switchAttack(bossPattern[currentAttack].attack);
            yield return new WaitForSeconds(bossPattern[currentAttack].attackLength);
        }
    }


    public enum Attacks { JumpAttack, IdleAttack, ChargeAttack, ShootProjectile, DecideNextAttack}

    [System.Serializable]
    public struct BossAttacks
    {
        public Attacks attack;
        public int attackLength;
    }
    [SerializeField]
    List<BossAttacks> bossPattern = new List<BossAttacks>();

    float fallMult = 2f;

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

    bool inBattle = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
        StartCoroutine("StartAttack");
        StartCoroutine("checkDistance");
    }

    void switchAttack(Attacks a)
    {
        if (inBattle)
        {
            //resetBoolAnimations();
            //resetTriggerAnimations();

            Invoke(a.ToString(), 0);
        }
        else
        {
            Debug.Log("NO LONGER IN BATTLE!!");
        }
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
            else if(distX <= 1 && distY < 1)
            {
                if (!Player.instance.playerStatus.getRecentlyDamaged())
                {
                    if (dir == 1)
                    {
                        Player.instance.playerStatus.takeDamage(damage, true, force);
                    }
                    else
                    {
                        Player.instance.playerStatus.takeDamage(damage, false, force);
                    }
                }
            }
            else
            {
                rb.velocity = new Vector2(chargeDir * chargeSpeed, rb.velocity.y);
            }

            chargeTime -= Time.deltaTime;
        }      

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (fallMult - 1) * Physics2D.gravity.y * Time.deltaTime;

            if (distX <= 1 && distY < 1)
            {
                if (!Player.instance.playerStatus.getRecentlyDamaged())
                {
                    if (dir == 1)
                    {
                        Player.instance.playerStatus.takeDamage(damage, true, force);
                    }
                    else
                    {
                        Player.instance.playerStatus.takeDamage(damage, false, force);
                    }
                }
            }
        }
    }

    void DecideNextAttack()
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
                ShootProjectile();
            }
        }
        //else if(distX <= minDistance)
        //{
        //    backOff();
        //}
        else
        {
            ChargeAttack();
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

    void ChargeAttack()
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

    void ShootProjectile()
    {
        newProjectile = Instantiate(projectile);
        newProjectile.GetComponent<Arrow>().setDirection(dir, 10);
        newProjectile.transform.position = this.transform.position - new Vector3(0, 1, 0);
    }

    public void setInBattle(bool set)
    {
        inBattle = set;
    }
}
