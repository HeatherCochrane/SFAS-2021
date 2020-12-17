using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    //Enums for keeping track of which enemy was killed
    public enum Species { ENEMY, SHEEP};

    [SerializeField]
    KillableData data;

    PlayerLevel player;
    PlayerQuests quests;

    int health = 2;

    Rigidbody2D rb;

    public bool isDead = false;

    GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health = data.health;
        player = Player.instance.levels;
        quests = Player.instance.playerQuests;
    }

    // Update is called once per frame
    void Update()
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

            if (health <= 0)
            {
                Invoke("killEnemy", 1f);
                isDead = true;
            }
        }
    }

    public void killEnemy()
    {
        player = Player.instance.levels;


        if (data.drop != null)
        {
            drop = Instantiate(data.drop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            drop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
        }

        player.addXP(data.XP);

        quests.speciesKilled(data.species);

        Destroy(this.gameObject);
    }
}
