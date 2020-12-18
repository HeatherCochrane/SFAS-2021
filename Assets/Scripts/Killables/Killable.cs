using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    //Enums for keeping track of which enemy was killed
    public enum Species { ENEMY, SHEEP };

    [SerializeField]
    public KillableData data;

    protected Player player;
    protected PlayerLevel playerLevel;
    protected PlayerQuests quests;

    int health = 2;

    protected Rigidbody2D rb;

    protected bool isDead = false;

    GameObject drop;

    public int damage = 0;
    public int force;

    GameObject lastArrow;


    // Start is called before the first frame update
    public void Start()
    {     
        rb = GetComponent<Rigidbody2D>();
        health = data.health;
        player = Player.instance;
        playerLevel = Player.instance.levels;
        quests = Player.instance.playerQuests;

    }

    public void attackPlayer()
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

            if (health <= 0)
            {
                killEnemy();
                isDead = true;
                CancelInvoke();
                StopAllCoroutines();
            }
        }
    }

    public void killEnemy()
    {
        if (!isDead)
        {
            if (data.drop != null)
            { 
                drop = Instantiate(data.drop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                //drop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
            }

            playerLevel.addXP(data.XP);

            quests.speciesKilled(data.species);

            Destroy(this.gameObject);
        }
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
