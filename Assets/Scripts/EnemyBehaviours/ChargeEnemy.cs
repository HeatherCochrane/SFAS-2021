using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Killable
{
    float cooldownTime = 2;

    bool onCooldown = false;

    float speed = 5f;

    float speedCap = 5;

    bool charging = false;

    float chargeTime = 3;

    float walkingSpeed = 2;

    Vector2 scaleValue;
    private void Start()
    {
        base.Start();
        scaleValue = transform.localScale;
        changeAnimationStatesTrigger(AnimationStates.MOVING);
    }

    void stopCountdown()
    {
        onCooldown = false;
        changeAnimationStatesTrigger(AnimationStates.MOVING);
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
                dir = playerDir;
                transform.localScale = new Vector3(playerDir * scaleValue.x, scaleValue.y, 0);
                charging = true;
                changeAnimationStatesTrigger(AnimationStates.CHARGE);
            }

            if (charging && !Player.instance.playerStatus.getRecentlyDamaged())
            {
                if (distX < 1)
                {
                    chargeTime = 3;
                    charging = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);

                    if (!onCooldown)
                    {
                        onCooldown = true;
                        Invoke("stopCountdown", cooldownTime);
                    }

                    changeAnimationStatesTrigger(AnimationStates.IDLE);
                }
                else if (chargeTime > 0)
                {
                    if (dir== 1)
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
                    chargeTime = 3;
                    charging = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);

                    if (!onCooldown)
                    {
                        onCooldown = true;
                        Invoke("stopCountdown", cooldownTime);
                    }

                    changeAnimationStatesTrigger(AnimationStates.IDLE);
                }

                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
            }
        }
    }

    public override void attackedPlayerResponse()
    {
        chargeTime = 3;
        charging = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        onCooldown = true;
        Invoke("stopCountdown", cooldownTime);

        changeAnimationStatesTrigger(AnimationStates.IDLE);
    }

    public override void damageResponse()
    {
        dir = playerDir;
        transform.localScale = new Vector3(playerDir * scaleValue.x, scaleValue.y, 0);
        charging = true;
        changeAnimationStatesTrigger(AnimationStates.CHARGE);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle" && rb.velocity.y == 0 && !isDead)
        {
            dir *= -1;
            charging = false;
            transform.localScale = new Vector3(dir * scaleValue.x, scaleValue.y, 0);

            if (!onCooldown)
            {

                changeAnimationStatesTrigger(AnimationStates.IDLE);
                onCooldown = true;

                Invoke("stopCountdown", cooldownTime);

            }
        }
    }

}
