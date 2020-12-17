using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerHealth;

    int health = 5;

    Rigidbody2D rb;

    [SerializeField]
    ParticleSystem healingEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void healPlayer(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 5);
        playerHealth.text = health.ToString();
        healingEffect.Play();
    }

    public void takeDamage(int amount, bool left, int force)
    {
        health -= amount;

        rb.velocity = new Vector2(0, 0);

        if (left)
        {

            rb.velocity = new Vector2(force, force);
        }
        else
        {
            rb.velocity = new Vector2(-force, force);
        }

        health = Mathf.Clamp(health, 0, 5);

        if (health <= 0)
        {
            Debug.Log("Died!");
        }

        playerHealth.text = health.ToString();
    }

}
