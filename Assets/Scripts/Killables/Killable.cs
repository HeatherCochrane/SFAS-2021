using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Killable : MonoBehaviour
{
    protected int playerDir = 1;
    protected int dir = 1;
    IEnumerator checkDistance()
    {
        while (true)
        {
            distX = Player.instance.transform.position.x - transform.position.x;
            distY = Player.instance.transform.position.y - transform.position.y;

            if (distX < 0)
            {
                distX *= -1;
                playerDir = -1;
            }
            else
            {
                playerDir = 1;
            }
            if (distY < 0)
            {
                distY *= -1;
            }

            if (!isDead)
            {
                if (distX <= boundsX && distY < boundsY)
                {
                    if (!Player.instance.playerStatus.getRecentlyDamaged())
                    {
                        attackPlayer();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }


    protected float distX = 0;
    protected float distY = 0;

    float boundsX = 0;
    float boundsY = 0;

    //Enums for keeping track of which enemy was killed
    public enum Species { ENEMY, SHEEP };

    [SerializeField]
    public KillableData data;

    protected PlayerQuests quests;

    int health = 2;

    protected Rigidbody2D rb;

    protected bool isDead = false;

    GameObject drop;

    public int damage = 0;
    public int force;

    GameObject lastArrow;

    protected Animator anim;

    public enum AnimationStates { IDLE, MOVING, ATTACK, DIE, ATTACKLEFT, ATTACKRIGHT, CHARGE, JUMP, DEATH};
    AnimationStates previous;
    AnimationStates current;

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject hitAmount;
    GameObject hit;

    // Start is called before the first frame update
    public void Start()
    {     
        rb = GetComponent<Rigidbody2D>();
        health = data.health;
        quests = Player.instance.playerQuests;
        anim = GetComponent<Animator>();

        canvas.GetComponent<Canvas>().worldCamera = Camera.main;

        boundsX = GetComponent<Collider2D>().bounds.size.x;
        boundsY = GetComponent<Collider2D>().bounds.size.y;
        StartCoroutine("checkDistance");
    }

    public void attackPlayer()
    {
        if (Player.instance.transform.position.x <= transform.position.x)
        {
            Player.instance.playerStatus.takeDamage(damage, false, force);
        }
        else
        {
            Player.instance.playerStatus.takeDamage(damage, true, force);
        }

        attackedPlayerResponse();

    }

    public virtual void attackedPlayerResponse()
    {

    }

    public void takeDamage(bool dir, int dam)
    {
        if (!isDead)
        {
            health -= 1;

            //If attacked from the right
            if (dir)
            {
                rb.velocity = new Vector2(-2, 2);
            }
            else
            {
                rb.velocity = new Vector2(2, 2);
            }

            spawnHitPoint(dam);
            Player.instance.audioHandler.playEnemyHit();

            if (health <= 0)
            {
                isDead = true;
                CancelInvoke();
                StopAllCoroutines();
                killEnemy();
            }
            else
            {
                damageResponse();
            }
        }
    }

    public virtual void damageResponse()
    {

    }
    void spawnHitPoint(int d)
    {
        hit = Instantiate(hitAmount);
        hit.transform.SetParent(canvas.transform);
        hit.GetComponent<TextMeshProUGUI>().text = d.ToString();
        hit.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-2, 2), Random.Range(-1, 1));
    }

    public void killEnemy()
    {
        if (data.drop != null)
        {
            for (int i = 0; i < data.drop.Count; i++)
            {
                for (int j = 0; j < data.drop[i].amount; j++)
                {
                    drop = Instantiate(data.drop[i].drop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    drop.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 5), 3);
                }
            }
        }

        quests.speciesKilled(data.species);

        if (GetComponent<BossEnemy>() != null)
        {
            GetComponent<BossEnemy>().area.openBossArea();
            Player.instance.checkBossDrop(GetComponent<BossEnemy>().drop.getAbility());
            quests.bossesKilled(GetComponent<BossEnemy>().bossName);

            Player.instance.audioHandler.playBossDeath();
        }
        else
        {
            Player.instance.audioHandler.playEnemyDeath();
        }


        isDead = true;
        changeAnimationStatesTrigger(AnimationStates.DEATH);

        StopAllCoroutines();
        CancelInvoke();
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

    public void changeAnimationStatesBool(AnimationStates a)
    {
        resetBoolAnimations();
        resetTriggerAnimations();

        switch (a)
        {
            case AnimationStates.IDLE:
                anim.SetBool("Idle", true);
                break;
            case AnimationStates.MOVING:
                anim.SetBool("Moving", true);
                break;
            case AnimationStates.ATTACK:
                anim.SetBool("Attack", true);
                break;
            case AnimationStates.ATTACKLEFT:
                anim.SetBool("AttackLeft", true);
                break;
            case AnimationStates.ATTACKRIGHT:
                anim.SetBool("AttackRight", true);
                break;
            case AnimationStates.DIE:
                anim.SetBool("Die", true);
                break;
            default:
                anim.SetBool("Idle", true);
                break;
        }

        current = a;
        previous = a;

    }

    public void changeAnimationStatesTrigger(AnimationStates a)
    {
        resetBoolAnimations();
        resetTriggerAnimations();

        switch (a)
        {
            case AnimationStates.ATTACKLEFT:
                anim.SetTrigger("Left");
                break;
            case AnimationStates.ATTACKRIGHT:
                anim.SetTrigger("Right");
                break;
            case AnimationStates.ATTACK:
                anim.SetTrigger("Attack");
                break;
            case AnimationStates.CHARGE:
                anim.SetTrigger("Charge");
                break;
            case AnimationStates.IDLE:
                anim.SetTrigger("Idle");
                break;
            case AnimationStates.MOVING:
                anim.SetTrigger("Moving");
                break;
            case AnimationStates.JUMP:
                anim.SetTrigger("Jump");
                break;
            case AnimationStates.DEATH:
                anim.SetTrigger("Death");
                break;
            default:
                anim.SetBool("Idle", true);
                break;
        }

        current = a;
        previous = a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Arrow")
        {
            if (collision.gameObject != lastArrow)
            {
                if (collision.transform.position.x < transform.position.x && !isDead)
                {
                    takeDamage(false, Player.instance.getRangedDamage());
                }
                else if(!isDead)
                {
                    takeDamage(true, Player.instance.getRangedDamage());
                }

                lastArrow = collision.gameObject;
                Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
            }
            else
            {
                Debug.Log("SAME ARROW COLLISION");
            }
        }
    }

    public AnimationStates getCurrentState()
    {
        return current;
    }
}
