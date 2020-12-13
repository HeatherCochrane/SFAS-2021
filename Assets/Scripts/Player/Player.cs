using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    //Quests
    public PlayerQuests playerQuests;

    //Level and XP handler
    public PlayerLevel levels;

    //Inventory
    public PlayerInventory inventory;

    //Weapons
    public PlayerWeapons weapons;

    //UI
    public UIHandler uiHandler;

    //Event System
    public UnityEngine.EventSystems.EventSystem system;

    //Melee animation
    [SerializeField]
    GameObject attackAnim;

    [SerializeField]
    public GameObject cam;
    public float smoothing = 2f;
    Vector3 offset;
    Vector3 camPos;

    //Player weapons
    Weapon meleeWeapon;
    Weapon longRangeWeapon;
    bool holding = false;
    Vector2 direction;

    [SerializeField]
    LayerMask killables;

    //Movement values
    [SerializeField]
    float speed = 0.2f;
    [SerializeField]
    float speedCap = 2.5f;
    bool stopMovement = false;
    bool facingLeft = false;

    //Health
    int health = 5;

    //Rigidbody
    Rigidbody2D rb;

    //Fall multiplier
    [SerializeField]
    float fallMult = 2f;
    [SerializeField]
    float jump = 5f;
    bool isFalling = false;

    //Character that can be talked to
    Character character;

    //Trader that can be interacted with
    Trader trader;
    bool stopInventoryToggle = false;

    [SerializeField]
    Game dialogue;

    [SerializeField]
    GameObject playerSprites;

    [SerializeField]
    SceneLoader sceneLoader;
    GameObject currentSceneObj;

    bool canLoadScene = false;

    bool trackInput = false;

    float maxLeftCam = 0;
    float maxRightCam = 0;

    bool isHidden = false;

    bool canHide = true;
    float cooldown = 10f;

    bool canReturnTown = false;

    //Double Jump Variables
    bool canDoubleJump = false;
    int jumpNum = 0;

    //Dash Variables
    bool canDash = true;
    float dashAmount = 0.5f;
    bool isDashing = false;
    bool dashCooldown = false;
    int dir = 1;
    float startDashTime = 0.2f;
    float dashTime = 0;

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

        inventory.addWeapon(meleeWeapon);
        inventory.addWeapon(longRangeWeapon);

        offset = cam.transform.position - this.transform.position;
        camPos = cam.transform.position;

        attackAnim.SetActive(false);

        UnityEngine.EventSystems.EventSystem.current = system;

        dashTime = startDashTime;

    }


    public void setInput(bool set)
    {
        trackInput = set;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackInput)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !stopInventoryToggle)
            {
                if (uiHandler.getInMenu(UIHandler.Menus.INVENTORY))
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    stopMovement = false;
                }
                else
                {
                    uiHandler.changeMenu(UIHandler.Menus.INVENTORY);
                    stopMovement = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!uiHandler.getInMenu(UIHandler.Menus.QUESTS))
                {
                    uiHandler.changeMenu(UIHandler.Menus.QUESTS);
                    setMovement(true);
                }
                else
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    setMovement(false);
                }
            }


            if (!stopMovement)
            {

                if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
                {
                    if (canDoubleJump)
                    {
                        jumpNum += 1;
                        rb.velocity = new Vector2(rb.velocity.x, jump);

                        if (jumpNum == 1)
                        {
                            isFalling = true;
                            jumpNum = 0;
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        isFalling = true;
                    }

                }

                if(Input.GetKeyDown(KeyCode.F) && canDash && !isDashing && !dashCooldown)
                {
                    isDashing = true;
                    dashCooldown = true;
                    Debug.Log("Called");
                }

              
                //Interaction Key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (character != null)
                    {
                        uiHandler.changeMenu(UIHandler.Menus.DIALOGUE);
                        beginConversation();
                        character = null;
                    }
                    else if (trader != null)
                    {
                        uiHandler.changeMenu(UIHandler.Menus.TRADER);
                        beginTrading();
                        trader = null;
                    }
                    else if (canLoadScene)
                    {
                        sceneLoader.switchSceneToLoad(currentSceneObj.transform.GetChild(0).gameObject);
                    }
                    else if(canReturnTown)
                    {
                        sceneLoader.switchScene("Town");
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return) && sceneLoader != null)
                {
                    sceneLoader.loadScene();
                    canLoadScene = false;
                }

                //Melee attack
                if (Input.GetMouseButtonDown(1) && meleeWeapon != null)
                {
                    setRandomAngle();
                    attackAnim.SetActive(true);
                    Attack(meleeWeapon.distance, meleeWeapon.damage);
                }

                //Long Range Attack, hold down then release to fire
                if (Input.GetMouseButton(0) && longRangeWeapon != null)
                {
                    holding = true;
                }
                if (Input.GetMouseButtonUp(0) && holding && longRangeWeapon != null)
                {
                    Attack(longRangeWeapon.distance, longRangeWeapon.damage);
                    holding = false;
                }

                if(Input.GetMouseButton(2) && canHide)
                {
                    startHiding();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!stopMovement && trackInput && !isDashing)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += new Vector2(speed, 0);
                facingLeft = false;
                dir = 1;
                playerSprites.transform.localScale = new Vector2(-0.3f, 0.3f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity -= new Vector2(speed, 0);
                facingLeft = true;
                dir = -1;
                playerSprites.transform.localScale = new Vector2(0.3f, 0.3f);
            }
        }

        if (isDashing)
        {
            if (dashTime > 0)
            {
                if (dir == 1)
                {
                    transform.position += new Vector3(dashAmount, 0, 0);
                }
                else
                {
                    transform.position -= new Vector3(dashAmount, 0, 0);
                }

                dashTime -= Time.deltaTime;
            }
            else
            {
                Invoke("stopDashing", 1);
                isDashing = false;
                dashTime = startDashTime;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }



        //Apply force when the player is falling 
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (fallMult - 1) * Physics2D.gravity.y * Time.deltaTime;
        }

        //Prevent the player from picking up too much speed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
    }

  
    void LateUpdate()
    {
        Vector3 targetCamPos = new Vector3(this.transform.position.x + offset.x, this.transform.position.y + offset.y, this.transform.position.z + offset.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetCamPos, smoothing * Time.deltaTime);

        cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, maxLeftCam, maxRightCam), cam.transform.position.y, cam.transform.position.z);
    }

    public void setCamBounds(float l, float r)
    {
        maxLeftCam = l;
        maxRightCam = r;
    }

    void setRandomAngle()
    {
        if (attackAnim.transform.rotation == Quaternion.Euler(0, 0, -20))
        {
            attackAnim.transform.rotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            attackAnim.transform.rotation = Quaternion.Euler(0, 0, -20);
        }
    }
    void hideAttackAnim()
    {
        attackAnim.SetActive(false);
    }

    void startHiding()
    {
        canHide = false;
        isHidden = true;
        Invoke("stopHiding", 3);
    }

    void stopHiding()
    {
        isHidden = false;
        Invoke("stopCooldown", cooldown);
    }

    void stopCooldown()
    {
        canHide = true;
    }

    void stopDashing()
    {
        dashCooldown = false;
    }
    void beginConversation()
    {
        //Pass in the characters dialogue data to begin the conversation
        uiHandler.changeMenu(UIHandler.Menus.DIALOGUE);
        dialogue.startNewDialogue(character.getData().getDialogue(), character.getData().getCharacterSprite(), character.getData().getName(), uiHandler.getMenuObject(UIHandler.Menus.DIALOGUE));
        stopMovement = true;
        holding = false;
        stopInventoryToggle = true;
    }

    void beginTrading()
    {
        trader.setTraderCanvas(uiHandler.getMenuObject(UIHandler.Menus.TRADER));
        SceneLoader.instance.traderButton.GetComponent<TraderButton>().setTrader(trader);
        SceneLoader.instance.leaveButton.GetComponent<TraderButton>().setTrader(trader);
        uiHandler.changeDoubleMenu(UIHandler.Menus.TRADER, UIHandler.Menus.INVENTORY);
        setMovement(true);
        stopInventoryToggle = true;
    }

    public void endConversation()
    {
        stopMovement = false;
        stopInventoryToggle = false;
    }

    public void setMovement(bool set)
    {
        stopMovement = set;
    }

    public void setInventoryToggle(bool set)
    {
        stopInventoryToggle = set;
    }

    public bool getIfHidden()
    {
        return isHidden;
    }

    void Attack(float dist, int damage)
    {
        RaycastHit2D hit;

        //Attack which way the player is facing

        if(direction == new Vector2(0, 0))
        {
            if (facingLeft)
            {
                direction = new Vector2(-1, 0);
            }
            else
            {
                direction = new Vector2(1, 0);
            }
        }

        hit = Physics2D.Raycast(transform.position, direction.normalized, dist, killables);

        if (hit.collider)
        {
            if (hit.collider.tag == "Killable")
            {
                hit.collider.gameObject.GetComponent<Killable>().takeDamage(facingLeft, damage);
            }
            else
            {
                Debug.Log("Hit not killable");
            }
        }
        else
        {
            Debug.Log("Collider null");
        }

        Invoke("hideAttackAnim", 0.2f);
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
   
    public void setRangedWeapon(Weapon w)
    {
        longRangeWeapon = w;
    }

    public void setMeleeWeapon(Weapon m)
    {
        meleeWeapon = m;
    }

    public void takeDamage(int amount, bool left, int force)
    {
        health -= amount;

        rb.velocity = new Vector2(0, 0);

        if(left)
        {
            
            rb.velocity = new Vector2(force, force);
        }
        else
        {
            rb.velocity = new Vector2(-force, force);
        }

        if(health <= 0)
        {
            Debug.Log("Died!");
        }
    }

    public void setDoubleJump(bool set)
    {
        canDoubleJump = set;
    }

    public void setDash(bool set)
    {
        canDash = set;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && collision.contacts[0].normal == new Vector2(0, 1))
        {
            isFalling = false;
            jumpNum = 0;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && collision.contacts[0].normal == new Vector2(0, 1))
        {
            isFalling = false;
            jumpNum = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            character = collision.GetComponent<Character>();
        }
        if (collision.tag == "Trader")
        {
            trader = collision.GetComponent<Trader>();
        }

        if(collision.tag == "SceneSwitcher")
        {
            canLoadScene = true;
            currentSceneObj = collision.gameObject;
        }

        if(collision.tag == "ReturnHome")
        {
            canReturnTown = true;
        }

        if (collision.gameObject.tag == "Pickup")
        {

            if (!collision.gameObject.GetComponent<WorldItem>().getItemData().stackable)
            {
                if (inventory.checkInventorySpace())
                {
                    inventory.addWeapon(collision.gameObject.GetComponent<WorldItem>().getItemData());
                    Destroy(collision.transform.gameObject);
                }
            }
            else
            {
                if (inventory.checkIfStackable(collision.gameObject.GetComponent<WorldItem>().getItemData()))
                {
                    inventory.addStackable(collision.gameObject.GetComponent<WorldItem>().getItemData());
                    Destroy(collision.transform.gameObject);
                }
                else
                {
                    inventory.addWeapon(collision.gameObject.GetComponent<WorldItem>().getItemData());
                    Destroy(collision.transform.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            character = null;
        }
        if(collision.tag == "Trader")
        {
            trader = null;
        }
        if (collision.tag == "SceneSwitcher")
        {
            canLoadScene = false;
            currentSceneObj = null;
        }

        if (collision.tag == "ReturnHome")
        {
            canReturnTown = false;
        }

    }
}
