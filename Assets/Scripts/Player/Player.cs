using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    //Level and XP handler
    public PlayerLevel levels;

    //Player weapons
    PlayerWeapons weapons;
    Weapon meleeWeapon;
    Weapon longRangeWeapon;

    //Movement values
    float speed = 0.2f;
    float speedCap = 3f;
    bool stopMovement = false;
    bool facingLeft = false;

    //Rigidbody
    Rigidbody2D rb;

    //Fall multiplier
    float fallMult = 2f;
    float jump = 5f;
    bool isFalling = false;

    //Character that can be talked to
    Character character;

    [SerializeField]
    Game dialogue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;

        levels = GetComponent<PlayerLevel>();
        weapons = GetComponent<PlayerWeapons>();


        meleeWeapon = weapons.getMeleeWeapon();
        longRangeWeapon = weapons.getRangedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //Interaction Key
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(character != null)
            {
                beginConversation();
            }
        }

        //Melee attack
        if(Input.GetMouseButtonDown(1))
        {
            Attack(meleeWeapon.distance, meleeWeapon.damage);
        }

        //Long Range Attack
        if(Input.GetMouseButtonDown(0))
        {
            Attack(longRangeWeapon.distance, longRangeWeapon.damage);
        }
    }

    private void FixedUpdate()
    {
        if (!stopMovement)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += new Vector2(speed, 0);
                facingLeft = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity -= new Vector2(speed, 0);
                facingLeft = true;
            }

            if (Input.GetKey(KeyCode.Space) && !isFalling)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                isFalling = true;
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (fallMult - 1) * Physics2D.gravity.y * Time.deltaTime;
        }

        //Prevent the play from picking up too much speed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
    }

    void beginConversation()
    {
        //Pass in the characters dialogue data to begin the conversation
        dialogue.startNewDialogue(character.getData().getDialogue());
    }

    void Attack(float dist, int damage)
    {
        RaycastHit2D hit;

        //Attack which way the player is facing
        if (facingLeft)
        {
            hit = Physics2D.Raycast(transform.position, -Vector2.right, dist);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, dist);
        }

        if (hit.collider)
        {
            if (hit.collider.tag == "Killable")
            {
                hit.collider.gameObject.GetComponent<Killable>().takeDamage(facingLeft, damage);
            }
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isFalling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            character = collision.GetComponent<Character>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            character = null;
        }
    }
}
