using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Killable
{
    Player player;
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

    Rigidbody2D rb;

    float speedCap = 5;

    bool charging = false;

    float chargeTime = 5;

    int walkingDir = 1;
    float walkingSpeed = 2;

    [SerializeField]
    int damage = 0;

    [SerializeField]
    int force = 0;

    private void Start()
    {
        player = Player.instance;
        rb = GetComponent<Rigidbody2D>();
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
            if (!charging || rb.velocity.x == 0)
            {
                transform.position += new Vector3(walkingSpeed * dir * Time.deltaTime, 0);
            }

            if (distX <= 5 && distY <= 1 && !onCooldown && !isDead)
            {
                dir = player.transform.position.x - transform.position.x;
                dir = Mathf.Clamp(dir, -1, 1);
                charging = true;

                Debug.Log("CHARGING");
                onCooldown = true;
                Invoke("stopCountdown", cooldownTime);
            }

            if (charging)
            {
                if (distX < 1)
                {
                    chargeTime = 5;
                    charging = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);

                    //do damage to the player when they are close
                    if (distY < 0.5f)
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
        }
    }

}
