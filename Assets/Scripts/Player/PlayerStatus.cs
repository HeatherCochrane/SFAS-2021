using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    IEnumerator dashCooldown()
    {
        Player.instance.setDashing(true);
        dashCooldownSlider.gameObject.SetActive(true);
        dashCooldownSlider.value = 10;

        while(dashCooldownSlider.value > 0)
        {
            dashCooldownSlider.value -= 1;
            yield return new WaitForSeconds(0.2f);
        }
        dashCooldownSlider.gameObject.SetActive(false);
        Player.instance.setDashing(false);
    }

    [SerializeField]
    GameObject playerHealth;

    int health = 5;

    Rigidbody2D rb;

    [SerializeField]
    ParticleSystem healingEffect;

    bool recentDamage = false;

    [SerializeField]
    Animator damageAnim;

    [SerializeField]
    Animator cameraShake;

    [SerializeField]
    Slider dashCooldownSlider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCooldownSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void healPlayer(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 5);
        updateHealth();
        healingEffect.Play();
    }

    public void takeDamage(int amount, bool left, int force)
    {
        if (!getRecentlyDamaged())
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

            updateHealth();

            cameraShake.SetTrigger("ScreenShake");

            recentDamage = true;
            damageAnim.SetBool("DamageTaken", true);
            Invoke("stopInvulnerableState", 2);
        }
    }

    void updateHealth()
    {
        for(int i =0; i < playerHealth.transform.childCount; i++)
        {
            playerHealth.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i =0; i < health; i++)
        {
            playerHealth.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void stopInvulnerableState()
    {
        damageAnim.SetBool("DamageTaken", false);
        recentDamage = false;
    }

    public bool getRecentlyDamaged()
    {
        return recentDamage;
    }
}
