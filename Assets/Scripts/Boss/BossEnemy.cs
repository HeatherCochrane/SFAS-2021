using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEnemy : Killable
{
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

    public enum Attacks { JumpAttack, IdleAttack, ChargeAttack, ProjectileAttack, DecideNextAttack}

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

    [SerializeField]
    public BossScene area;

    int currentDir = 1;

    [SerializeField]
    public BossDrops drop;

    [SerializeField]
    public BossScene.BossNames bossName;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void startBattle()
    {
        inBattle = true;
        StartCoroutine("StartAttack");
    }

    public void setScene(BossScene b)
    {
        area = b;
    }

    void switchAttack(Attacks a)
    {
        if (inBattle)
        {
            currentDir = playerDir;
            updateBossRotation();
            Invoke(a.ToString(), 0);
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
                changeAnimationStatesBool(AnimationStates.IDLE);
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
        else
        {
            ChargeAttack();
        }
    }

    void IdleAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.IDLE);
    }

    void JumpAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.IDLE);

        if (playerDir == -1)
        {
            rb.velocity = new Vector2(-5, 12);
        }
        else
        {
            rb.velocity = new Vector2(5, 12);
        }
    }

    void ChargeAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.CHARGE);
        chargeDir = playerDir;
        isCharging = true;
    }

    void ProjectileAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.ATTACK);
    }

    void ShootProjectile()
    {
        newProjectile = Instantiate(projectile);
        newProjectile.GetComponent<Arrow>().setDirection(playerDir, 10);
        newProjectile.transform.position = this.transform.position - new Vector3(0, 1, 0);
    }

    public void setInBattle(bool set)
    {
        inBattle = set;
    }

    void updateBossRotation()
    {
        if (currentDir == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
