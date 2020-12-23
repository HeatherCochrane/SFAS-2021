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
    BossScene area;

    [SerializeField]
    TextMeshProUGUI bossNextMove;

    int currentDir = 1;

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

    void switchAttack(Attacks a)
    {
        if (inBattle)
        {
            bossNextMove.text = a.ToString();
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

        if (distX <= 1 && distY < 1)
        {
            if (!Player.instance.playerStatus.getRecentlyDamaged())
            {
                attackPlayer();
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
        else
        {
            ChargeAttack();
        }
    }

    void IdleAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.IDLE);
        Debug.Log("IM IDLING");
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
        Debug.Log("JUMP BITCH");
    }

    void ChargeAttack()
    {
        changeAnimationStatesTrigger(AnimationStates.CHARGE);
        chargeDir = playerDir;
        isCharging = true;
        Debug.Log("CHARGE!!");
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

    public void openBattleArea()
    {
        area.openBossArea();
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
