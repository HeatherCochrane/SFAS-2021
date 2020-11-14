using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField]
    KillableData data;

    PlayerLevel player;


    int health = 2;

    Rigidbody2D rb;

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health = data.health;
        player = Player.instance.levels;
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
            Instantiate(data.drop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }

        player.addXP(data.XP);

        Destroy(this.gameObject);
    }
}
