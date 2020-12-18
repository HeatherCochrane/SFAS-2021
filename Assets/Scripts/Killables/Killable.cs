﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    //Enums for keeping track of which enemy was killed
    public enum Species { ENEMY, SHEEP };

    [SerializeField]
    KillableData data;

    public Player player;
    PlayerLevel playerLevel;
    PlayerQuests quests;

    int health = 2;

    public Rigidbody2D rb;

    public bool isDead = false;

    GameObject drop;

    public int damage = 0;
    public int force;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
                Invoke("killEnemy", 1f);
                StopAllCoroutines();
                isDead = true;
            }
        }
    }

    public void killEnemy()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Arrow")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                takeDamage(false, Player.instance.getRangedDamage());
            }
            else
            {
                takeDamage(true, Player.instance.getRangedDamage());
            }
        }
    }
}
