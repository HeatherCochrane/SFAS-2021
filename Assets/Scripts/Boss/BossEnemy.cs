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

    public enum Attacks { JumpAttack, IdleAttack, ChargeAttack, ProjectileAttack, DecideNextAttack, MultipleProjectiles}

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
    [SerializeField]
    float chargeSpeed = 7.5f;
    int chargeDir = 1;

    [SerializeField]
    float chargeTime = 1;

    float charge = 0;

    [SerializeField]
    int range = 0;
    int choice = 0;

    [SerializeField]
    GameObject projectile;
    GameObject newProjectile;

    bool inBattle = false;

    [SerializeField]
    public BossScene area;

    int currentDir = 1;

    [SerializeField]
    public BossDrops bossDrop;

    [SerializeField]
    public BossScene.BossNames bossName;

    int multipleCount = 0;

    float originalScale = 0;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        originalScale = transform.localScale.x;
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
            if (charge <= 0)
            {
                isCharging = false;
                charge = chargeTime;
                rb.velocity = new Vector2(0, 0);
                changeAnimationStatesBool(AnimationStates.IDLE);
            }
            else
            {
                rb.velocity = new Vector2(chargeDir * chargeSpeed, rb.velocity.y);
            }

            charge -= Time.deltaTime;
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
        changeAnimationStatesTrigger(AnimationStates.JUMP);

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

    void MultipleProjectiles()
    {
        if (multipleCount != 3)
        {
            changeAnimationStatesTrigger(AnimationStates.ATTACK);
            Invoke("MultipleProjectiles", 1);
            multipleCount += 1;
        }
        else
        {
            multipleCount = 0;
        }
    }

    void ShootProjectile()
    {
        newProjectile = Instantiate(projectile);
        newProjectile.GetComponent<Arrow>().setDirection(playerDir, range);
        newProjectile.transform.position = rangedStartPoint.position;
    }

    public void setInBattle(bool set)
    {
        inBattle = set;
    }

    void updateBossRotation()
    {
        if (currentDir == 1)
        {
            transform.localScale = new Vector3(-originalScale, originalScale, 1);
        }
        else
        {
            transform.localScale = new Vector3(originalScale, originalScale, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            if(collision.contacts[0].normal.x >= 0.2f && getCurrentState() != AnimationStates.IDLE)
            {
                IdleAttack();
            }
            else if (getCurrentState() == AnimationStates.JUMP)
            {
                IdleAttack();
            }
        }
    }
}
