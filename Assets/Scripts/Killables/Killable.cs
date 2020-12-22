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
            distX = player.transform.position.x - transform.position.x;
            distY = player.transform.position.y - transform.position.y;

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
            yield return new WaitForEndOfFrame();
        }
    }


    protected float distX = 0;
    protected float distY = 0;

    //Enums for keeping track of which enemy was killed
    public enum Species { ENEMY, SHEEP };

    [SerializeField]
    public KillableData data;

    protected Player player;
    protected PlayerQuests quests;

    int health = 2;

    protected Rigidbody2D rb;

    protected bool isDead = false;

    GameObject drop;

    public int damage = 0;
    public int force;

    GameObject lastArrow;

    protected Animator anim;

    public enum AnimationStates { IDLE, MOVING, ATTACK, DIE, ATTACKLEFT, ATTACKRIGHT};
    AnimationStates previous;

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
        player = Player.instance;
        quests = Player.instance.playerQuests;
        anim = GetComponent<Animator>();

        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        StartCoroutine("checkDistance");
    }

    public void attackPlayer()
    {
        if (!Player.instance.playerStatus.getRecentlyDamaged())
        {
            if (Player.instance.transform.position.x < transform.position.x)
            {
                Player.instance.playerStatus.takeDamage(damage, false, force);
            }
            else
            {
                Player.instance.playerStatus.takeDamage(damage, true, force);
            }
        }
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

            if (health <= 0)
            {
                killEnemy();
                isDead = true;
                CancelInvoke();
                StopAllCoroutines();
            }
        }
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
        if (!isDead)
        {
            if (data.drop != null)
            {
                for (int i = 0; i < data.drop.Count; i++)
                {
                    drop = Instantiate(data.drop[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    drop.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 5), 3);
                }
            }
            quests.speciesKilled(data.species);

            Destroy(this.gameObject);
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

    public void changeAnimationStatesBool(AnimationStates a)
    {
        if (a != previous)
        {
            resetBoolAnimations();

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

            previous = a;
        }
    }

    public void changeAnimationStatesTrigger(AnimationStates a)
    {
        resetTriggerAnimations();

        switch (a)
        {
            case AnimationStates.ATTACKLEFT:
                anim.SetTrigger("Left");
                break;
            case AnimationStates.ATTACKRIGHT:
                anim.SetTrigger("Right");
                break;
            default:
                anim.SetBool("Idle", true);
                break;
        }

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
}
