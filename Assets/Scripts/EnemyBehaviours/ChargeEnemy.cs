using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Killable
{
    float dir = 1;

    IEnumerator checkDistance()
    {
        while (true)
        {
            distX = player.transform.position.x - transform.position.x;
            distY = player.transform.position.y - transform.position.y;

            if(distX < 0)
            {
                distX *= -1;
            }
            if(distY < 0)
            {
                distY *= -1;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    [SerializeField]
    float cooldownTime = 0;

    bool onCooldown = false;

    float speed = 5f;

    float distX = 0;
    float distY = 0;

    float speedCap = 5;

    bool charging = false;

    float chargeTime = 5;

    float walkingSpeed = 2;

    private void Start()
    {
        base.Start();
        StartCoroutine("checkDistance");
    }

    void stopCountdown()
    {
        onCooldown = false;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if ((!charging || rb.velocity.x == 0) && !onCooldown)
            {
                transform.position += new Vector3(walkingSpeed * dir * Time.deltaTime, 0);
            }

            if (distX <= 5 && distY <= 1 && !charging && !onCooldown)
            {
                dir = player.transform.position.x - transform.position.x;
                dir = Mathf.Clamp(dir, -1, 1);
                charging = true;
            }

            if (charging && !Player.instance.playerStatus.getRecentlyDamaged())
            {
                if (distX < 1)
                {
                    chargeTime = 5;
                    charging = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);

                    //do damage to the player when they are close
                    if (distY <= 1f)
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

                    if (!onCooldown)
                    {
                        onCooldown = true;
                        Invoke("stopCountdown", cooldownTime);
                    }
                }
                else if (chargeTime > 0)
                {
                    if (dir == 1)
                    {
                        rb.velocity += new Vector2(speed, 0);
                    }
                    else
                    {
                        rb.velocity -= new Vector2(speed, 0);
                    }

                    chargeTime -= Time.deltaTime;
                }
                else
                {
                    chargeTime = 5;
                    charging = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);

                    if (!onCooldown)
                    {
                        onCooldown = true;
                        Invoke("stopCountdown", cooldownTime);
                    }
                }

                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            dir *= -1;
            charging = false;

            if (!onCooldown)
            {
                onCooldown = true;

                Invoke("stopCountdown", cooldownTime);
                Debug.Log("OBSTACLE COOL DOWN");

            }
        }
    }

}
