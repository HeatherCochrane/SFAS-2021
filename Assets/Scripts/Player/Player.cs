using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    //Quests
    public PlayerQuests playerQuests;

    [SerializeField]
    GameObject cam;
    public float smoothing = 2f;
    Vector3 offset;
    Vector3 camPos;

    //Level and XP handler
    public PlayerLevel levels;

    //Player weapons
    PlayerWeapons weapons;
    Weapon meleeWeapon;
    Weapon longRangeWeapon;
    bool holding = false;
    Vector2 direction;

    //Longe Range variables
    Vector2 mousePos;
    [SerializeField]
    LineRenderer rangeIndicator;

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
        playerQuests = GetComponent<PlayerQuests>();


        meleeWeapon = weapons.getMeleeWeapon();
        longRangeWeapon = weapons.getRangedWeapon();


        offset = cam.transform.position - this.transform.position;
        camPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {
            //Interaction Key
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (character != null)
                {
                    beginConversation();
                }
            }


            //Melee attack
            if (Input.GetMouseButtonDown(1))
            {
                Attack(meleeWeapon.distance, meleeWeapon.damage);
            }

            //Long Range Attack, hold down then release to fire
            if (Input.GetMouseButton(0))
            {
                holding = true;
                drawRange();
                rangeIndicator.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonUp(0) && holding)
            {
                Attack(longRangeWeapon.distance, longRangeWeapon.damage);
                holding = false;
                rangeIndicator.gameObject.SetActive(false);
            }
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

        //Apply force when the player is falling 
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (fallMult - 1) * Physics2D.gravity.y * Time.deltaTime;
        }

        //Prevent the play from picking up too much speed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = new Vector3(this.transform.position.x + offset.x, cam.transform.position.y, this.transform.position.z + offset.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetCamPos, smoothing * Time.deltaTime);
        cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, -262, 255), cam.transform.position.y, cam.transform.position.z);
    }

    void beginConversation()
    {
        //Pass in the characters dialogue data to begin the conversation
        dialogue.startNewDialogue(character.getData().getDialogue());
        stopMovement = true;
        holding = false;
        rangeIndicator.gameObject.SetActive(false);
    }

    public void endConversation()
    {
        stopMovement = false;
    }

    void Attack(float dist, int damage)
    {
        RaycastHit2D hit;

        //Attack which way the player is facing

        hit = Physics2D.Raycast(transform.position, direction.normalized, dist);

        if (hit.collider)
        {
            if (hit.collider.tag == "Killable")
            {
                hit.collider.gameObject.GetComponent<Killable>().takeDamage(facingLeft, damage);
            }
        }
    }

    void drawRange()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);    

        Vector2 length = mousePos - (Vector2)transform.position;
        length = length.normalized * longRangeWeapon.distance;

        rangeIndicator.SetPosition(0, new Vector2(0, 0));

        rangeIndicator.SetPosition(1, new Vector3(length.x, length.y));
        direction = rangeIndicator.GetPosition(1) - rangeIndicator.GetPosition(0);

        if(mousePos.x < transform.position.x)
        {
            facingLeft = true;
        }
        else
        {
            facingLeft = false;
        }
    }

    public string getIfHolding()
    {
        if(holding)
        {
            return "Loaded!";
        }
        else
        {
            return "Released!";
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
