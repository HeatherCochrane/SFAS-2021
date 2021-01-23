using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public IEnumerator DashCooldown()
    {
        Player.instance.setDashing(true);
        dashObject.gameObject.SetActive(true);
        dashFill.rectTransform.anchoredPosition = new Vector2(dashFill.transform.parent.GetComponent<Image>().rectTransform.anchoredPosition.x - 50, 0);

        while (dashFill.rectTransform.anchoredPosition.x < 0)
        {
            dashFill.rectTransform.anchoredPosition += new Vector2(3, 0);
            yield return new WaitForSeconds(0.1f);
        }

        dashObject.gameObject.SetActive(false);
        dashFill.rectTransform.anchoredPosition = dashFillPos;
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
    GameObject dashObject;

    [SerializeField]
    Image dashFill;
    Vector2 dashFillPos;
    Vector2 newPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashFillPos = dashFill.rectTransform.position;
        dashObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCooldown()
    {
        StartCoroutine(DashCooldown());
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
                Player.instance.sceneLoader.respawnPlayer();
                health = 5;
                updateHealth();

                Player.instance.audioHandler.playPlayerDeath();
            }

            Player.instance.audioHandler.playPlayerHit();
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

    public void setInvincible(bool set)
    {
        recentDamage = set;
    }
}
