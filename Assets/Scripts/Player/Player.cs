﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public static Player instance;

    //Quests
    public PlayerQuests playerQuests;

    //Inventory
    public PlayerInventory inventory;

    //Weapons
    public PlayerWeapons weapons;

    //UI
    public UIHandler uiHandler;

    //Saved Data
    public DataToSave data;

    //SceneLoader
    public SceneLoader sceneLoader;

    //Player Status effects
    public PlayerStatus playerStatus;

    //Player Menus
    public MenuSelection menus;

    //Audio Handler
    public Audio audioHandler;

    //Event System
    public UnityEngine.EventSystems.EventSystem system;

    [SerializeField]
    public GameObject cam;
    public float smoothing = 2f;
    Vector3 offset;
    Vector3 camPos;
    Vector3 targetCamPos;

    //Player weapons
    Weapon meleeWeapon;
    Weapon longRangeWeapon;
    bool isAttacking = false;

    [SerializeField]
    LayerMask killables;

    //Movement values
    [SerializeField]
    float speed = 0.2f;
    [SerializeField]
    float speedCap = 2.5f;
    bool stopMovement = false;
    bool facingLeft = false;

    //Rigidbody
    Rigidbody2D rb;
    bool isRunning = false;
    Vector2 moveDirection;

    //Fall multiplier
    [SerializeField]
    float fallMult = 2f;
    [SerializeField]
    float jump = 5f;
    bool isGrounded = false;
    int jumpNum = 0;
    bool hasDoubleJumped = false;

    //Character that can be talked to
    Character character;

    //Trader that can be interacted with
    Trader trader;
    bool stopInventoryToggle = false;

    [SerializeField]
    public Game dialogue;

    [SerializeField]
    GameObject playerSprites;

    public bool trackInput = false;

    Vector2 maxHorizontal;
    Vector2 maxVertical;

    //Dash Variables
    bool canDash = true;
    float dashAmount = 50f;
    bool isDashing = false;
    bool dashCooldown = false;
    int dir = -1;
    float startDashTime = 0.2f;
    float dashTime = 0;
    float startingY = 0;
    [SerializeField]
    TrailRenderer dashTrail;

    //Wall jump variables
    bool canWallJump = true;
    bool ignorePlayerDir = false;


    [SerializeField]
    Animator anim;

    GameObject pickUp;

    Building currentBuilding;

    public enum AnimationStates { RUN, JUMP, DASH, IDLE, MELEEDOWN, MELEEUP, RANGED}

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    Transform arrowPos;

    GameObject newArrow;

    AnimationStates previous;

    [SerializeField]
    ParticleSystem pickUpEffect;
    ParticleSystem effectParticles;

    float clickTime = 0.25f;
    float heldTime = 0;

    float attackCooldown = 0.5f;
    bool onAttackCooldown = false;

    bool playerControlled = true;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;

        instance = this;

        rb = GetComponent<Rigidbody2D>();

        weapons = GetComponent<PlayerWeapons>();
        playerQuests = GetComponent<PlayerQuests>();
      
        meleeWeapon = weapons.getMeleeWeapon();
        longRangeWeapon = weapons.getRangedWeapon();

        inventory.addWeapon(meleeWeapon);
        inventory.addWeapon(longRangeWeapon);

        offset = cam.transform.position - this.transform.position;
        camPos = cam.transform.position;

        UnityEngine.EventSystems.EventSystem.current = system;

        dashTime = startDashTime;
        dashTrail.gameObject.SetActive(false);
    }

    public void setInput(bool set)
    {
        trackInput = set;
    }

    void stopAttackCooldown()
    {
        onAttackCooldown = false;
    }

    public void OnInteract(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!stopMovement && trackInput)
            {
                if (character != null)
                {
                    uiHandler.changeMenu(UIHandler.Menus.DIALOGUE);

                    if (!data.hasBeenTalkedTo(character))
                    {
                        beginConversation(0);
                    }
                    else
                    {
                        beginConversation(-1);
                    }

                    data.addCharacter(character);
                    character = null;

                }
                else if (trader != null)
                {
                    beginTrading();
                    trader = null;
                }
                else if (pickUp != null)
                {
                    pickUpItem();
                    pickUp = null;
                }
                else if (currentBuilding != null)
                {
                    sceneLoader.switchScene(currentBuilding.getSceneName(), currentBuilding.GetComponent<TransitionGate>());
                }
            }
        }
    }


    public void OnMap(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (trackInput)
            {
                if (uiHandler.getInMenu(UIHandler.Menus.MAP))
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    setMovement(false);
                    audioHandler.playMap(false);

                }
                else if (!uiHandler.GetInMenu())
                {
                    uiHandler.changeMenu(UIHandler.Menus.MAP);
                    setMovement(true);
                    audioHandler.playMap(true);

                }
            }
        }
    }

    public void onShowAudio(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (trackInput)
            {
                if (uiHandler.getInMenu(UIHandler.Menus.AUDIO))
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    stopMovement = false;

                    audioHandler.playInventory(false);
                }
                else if(!uiHandler.GetInMenu())
                {
                    uiHandler.changeMenu(UIHandler.Menus.AUDIO);
                    stopMovement = true;

                    audioHandler.playInventory(true);
                }
            }
        }
    }
    

    
    public void OnInventory(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (trackInput && !stopInventoryToggle)
            {
                if (uiHandler.getInMenu(UIHandler.Menus.INVENTORY))
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    inventory.setInventory(false);
                    stopMovement = false;

                    audioHandler.playInventory(false);
                }
                else if(!uiHandler.GetInMenu())
                {
                    uiHandler.hideMap();
                    uiHandler.changeDoubleMenu(UIHandler.Menus.PLAYERUI, UIHandler.Menus.INVENTORY);
                    inventory.setInventory(true);
                    stopMovement = true;

                    audioHandler.playInventory(true);
                }
            }
        }
    }


    public void OnQuests(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (trackInput)
            {
                if (uiHandler.getInMenu(UIHandler.Menus.QUESTS))
                {
                    uiHandler.changeMenu(UIHandler.Menus.PLAYERUI);
                    setMovement(false);
                    audioHandler.playMap(false);
                }
                else if (!uiHandler.GetInMenu())
                {
                    uiHandler.changeMenu(UIHandler.Menus.QUESTS);
                    setMovement(true);
                    audioHandler.playMap(true);
                }
            }
        }
    }

    public void OnMeleeAttack(CallbackContext ctx)
    {
        if (!stopMovement && trackInput && !onAttackCooldown)
        {
            if (ctx.performed)
            {
                //Melee attack
                if (meleeWeapon != null && !onAttackCooldown && !playerStatus.getRecentlyDamaged())
                {

                    switchAnimation(AnimationStates.MELEEUP);
                    Attack(meleeWeapon.distance, meleeWeapon.damage);
                    onAttackCooldown = true;
                    Invoke("stopAttackCooldown", attackCooldown);
                    rb.velocity += new Vector2(dir * 4, 0);

                    audioHandler.playMelee();
                }
            }
        }
    }

    public void OnRangeAttack(CallbackContext ctx)
    {
        if (!stopMovement && trackInput && !onAttackCooldown)
        {
            if (ctx.performed)
            {
                if (longRangeWeapon != null && !onAttackCooldown && !playerStatus.getRecentlyDamaged())
                {
                    switchAnimation(AnimationStates.RANGED);
                    onAttackCooldown = true;
                    Invoke("stopAttackCooldown", attackCooldown);

                    audioHandler.playRanged();
                }
            }
        }
    }

    public void OnMove(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveDirection = ctx.ReadValue<Vector2>();
            if(moveDirection.x <= -0.9f || moveDirection.x >= 0.9f)
            {
                isRunning = true;
            }
        }
        else if(ctx.canceled)
        {
            isRunning = false;
        }
    }

    public void OnDash()
    {
        if (!stopMovement && trackInput)
        {
            if (canDash && !isDashing && !dashCooldown)
            {
                playerStatus.startCooldown();
                isDashing = true;
                speedCap = 30;
                startingY = transform.position.y;
                switchAnimation(AnimationStates.DASH);
                dashTrail.gameObject.SetActive(true);

                audioHandler.playDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!stopMovement && trackInput && !isDashing && !isAttacking)
        {
            if (!stopMovement && trackInput && !isDashing && !isAttacking)
            {
                if (!ignorePlayerDir)
                {
                    if (isRunning)
                    {
                        rb.velocity += new Vector2(moveDirection.x, 0);

                        if (moveDirection.x > 0)
                        {
                            facingLeft = false;
                            dir = 1;

                            playerSprites.transform.localScale = new Vector2(-0.3f, 0.3f);
                        }
                        else
                        {
                            facingLeft = true;
                            dir = -1;

                            playerSprites.transform.localScale = new Vector2(0.3f, 0.3f);
                        }
                    }
                }
            }


            if (isRunning && isGrounded)
            {
                switchAnimation(AnimationStates.RUN);
            }

            if (rb.velocity.x == 0 && rb.velocity.y == 0 && isGrounded)
            {
                switchAnimation(AnimationStates.IDLE);
            }

            if (rb.velocity.y > 3.5f || rb.velocity.y < -3.5f && !isGrounded)
            {
                switchAnimation(AnimationStates.JUMP);
            }
        }
        else if (!isDashing)
        {
            switchAnimation(AnimationStates.IDLE);
        }

        if (isDashing)
        {
            if (dashTime > 0)
            {
                if (dir == 1)
                {
                    rb.velocity += new Vector2(dashAmount, 0);
                }
                else
                {
                    rb.velocity -= new Vector2(dashAmount, 0);
                }

                dashTime -= Time.deltaTime;
                //stop the player from dipping down or jumping up
                transform.position = new Vector2(transform.position.x, startingY);
            }
            else
            {
                isDashing = false;
                dashTime = startDashTime;
                speedCap = 5;
                rb.velocity = new Vector2(0, rb.velocity.y);
                switchAnimation(AnimationStates.RUN);
                dashTrail.gameObject.SetActive(false);
            }
        }

            //Apply force when the player is falling 
            if (rb.velocity.y <= 0 && !isDashing)
            {
                rb.velocity += Vector2.up * (fallMult - 1) * Physics2D.gravity.y * Time.deltaTime;
            }

            //Prevent the player from picking up too much speed
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speedCap, speedCap), rb.velocity.y);
        
    }


    public void OnJump(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!stopMovement && trackInput)
            {
                if ((isGrounded || !hasDoubleJumped))
                {
                    if (canWallJump && !isGrounded)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, GetComponent<Collider2D>().bounds.size.x);
                        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right, GetComponent<Collider2D>().bounds.size.x);
                        rb.velocity = new Vector2(0, 0);

                        if (hit.collider != null)
                        {
                            rb.velocity = new Vector2(-jump * 10, jump);
                            playerSprites.transform.localScale = new Vector2(0.3f, 0.3f);

                            ignorePlayerDir = true;
                            Invoke("stopIgnoringPlayerDir", 4);
                        }
                        else if (hit2.collider != null)
                        {
                            rb.velocity = new Vector2(jump * 10, jump);
                            playerSprites.transform.localScale = new Vector2(-0.3f, 0.3f);

                            ignorePlayerDir = true;
                            Invoke("stopIgnoringPlayerDir", 4);
                        }
                        else
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jump);
                            isGrounded = false;
                            switchAnimation(AnimationStates.JUMP);
                            jumpNum += 1;

                            if (jumpNum == 2)
                            {
                                hasDoubleJumped = true;
                                isGrounded = false;
                                jumpNum = 0;
                            }
                        }

                        isGrounded = false;
                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jump);
                        isGrounded = false;
                        switchAnimation(AnimationStates.JUMP);
                        jumpNum += 1;

                        if (jumpNum == 2)
                        {
                            hasDoubleJumped = true;
                            isGrounded = false;
                            jumpNum = 0;
                        }
                    }
                }
            }
        }
    }

    public void OnLookDown(CallbackContext ctx)
    {
        if (!stopMovement && trackInput)
        {
            if (ctx.performed)
            {
                lookDown = true;
            }
            else if (ctx.canceled)
            {
                lookDown = false;
            }
        }
    }

    public void OnLookUp(CallbackContext ctx)
    {
        if (!stopMovement && trackInput)
        {
            if (ctx.performed)
            {
                lookUp = true;
            }
            else if (ctx.canceled)
            {
                lookUp = false;
            }
        }
    }

    bool lookDown = false;
    bool lookUp = false;

    void LateUpdate()
    {
        if (playerControlled)
        {

            if (lookDown)
            {
                targetCamPos = new Vector3(transform.position.x + offset.x, transform.position.y +( offset.y - 3), transform.position.z + offset.z);
            }
            else if(lookUp)
            {
                targetCamPos = new Vector3(transform.position.x + offset.x, transform.position.y + (offset.y + 2), transform.position.z + offset.z);
            }
            else
            {
                targetCamPos = new Vector3(this.transform.position.x + offset.x, this.transform.position.y + offset.y, this.transform.position.z + offset.z);
            }

            cam.transform.position = Vector3.Lerp(cam.transform.position, targetCamPos, smoothing * Time.deltaTime);

            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, maxHorizontal.x, maxHorizontal.y), Mathf.Clamp(cam.transform.position.y, maxVertical.x, maxVertical.y), cam.transform.position.z);
        }
    }

    public void setIsGrounded()
    {
        isGrounded = true;
        ignorePlayerDir = false;
    }
    void resetAnimations()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(parameter.name, false);
            }
        }
    }

    void switchAnimation(AnimationStates a)
    {     
        if (a != previous)
        {
            resetAnimations();

            switch (a)
            {
                case AnimationStates.RUN:
                    anim.SetBool("Run", true);
                    break;
                case AnimationStates.JUMP:
                    anim.SetBool("Jump", true);
                    break;
                case AnimationStates.DASH:
                    anim.SetBool("Dash", true);
                    break;
                case AnimationStates.MELEEUP:
                    anim.SetBool("MeleeUp", true);
                    break;
                case AnimationStates.MELEEDOWN:
                    anim.SetBool("MeleeDown", true);
                    break;
                case AnimationStates.IDLE:
                    anim.SetBool("Idle", true);
                    break;
                case AnimationStates.RANGED:
                    anim.SetBool("Ranged", true);
                    break;
                default:
                    anim.SetBool("Run", true);
                    break;
            }

            previous = a;
        }
    }

    public void setCamBounds(Vector2 h, Vector2 v)
    {
        maxHorizontal = h;
        maxVertical = v;
    }

    void hideAttackAnim()
    {
        isAttacking = false;
    }

    public void setDashing(bool set)
    {
        dashCooldown = set;
    }

    void beginConversation(int index)
    {
        //Pass in the characters dialogue data to begin the conversation
        uiHandler.changeMenu(UIHandler.Menus.DIALOGUE);

        dialogue.startNewDialogue(character.getData().getDialogue(index), character.getData().getCharacterSprite(), character.getData().getName(), uiHandler.getMenuObject(UIHandler.Menus.DIALOGUE));
        stopMovement = true;
        stopInventoryToggle = true;

        audioHandler.playDialogue(true);
    }

    public void setInConvo()
    {
        stopMovement = true;
        stopInventoryToggle = true;

        audioHandler.playDialogue(true);
    }

    void beginTrading()
    {
        trader.setTraderCanvas(uiHandler.getMenuObject(UIHandler.Menus.TRADER));
        SceneLoader.instance.traderButton.GetComponent<TraderButton>().setTrader(trader);
        SceneLoader.instance.leaveButton.GetComponent<TraderButton>().setTrader(trader);
        uiHandler.changeDoubleMenu(UIHandler.Menus.INVENTORY, UIHandler.Menus.TRADER);
        setMovement(true);
        stopInventoryToggle = true;
    }

    public void endConversation()
    {
        stopMovement = false;
        stopInventoryToggle = false;

        audioHandler.playDialogue(false);
    }

    public void setMovement(bool set)
    {
        stopMovement = set;
    }

    public void setInventoryToggle(bool set)
    {
        stopInventoryToggle = set;
    }

    public void spawnArrow()
    {
        newArrow = Instantiate(arrow);
        newArrow.GetComponent<Arrow>().setDirection(dir, longRangeWeapon.distance);
        newArrow.transform.position = arrowPos.position;
    }
    public void longRanged()
    {
        //Attack(longRangeWeapon.distance, longRangeWeapon.damage);
        isAttacking = false;
    }

    void Attack(float dist, int damage)
    {
        isAttacking = true;
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, new Vector2(dir, 0), dist, killables);

        if (hit.collider)
        {
            if (hit.collider.tag == "Killable" || hit.collider.tag =="Hazard")
            {
                hit.collider.gameObject.GetComponent<Killable>().takeDamage(facingLeft, damage);
            }
        }
        else
        {
            Debug.Log("Collider null");
        }

        Invoke("hideAttackAnim", 0.5f);
    }

    public void setRangedWeapon(Weapon w)
    {
        longRangeWeapon = w;
    }

    public void setMeleeWeapon(Weapon m)
    {
        meleeWeapon = m;
    }

    public void setDash(bool set)
    {
        canDash = set;
    }

    public void setWallJump(bool set)
    {
        canWallJump = set;
    }

    public int getRangedDamage()
    {
        return longRangeWeapon.damage;
    }

    public void checkBossDrop(BossDrops.PlayerAbilities p)
    {
        switch(p)
        {
            case BossDrops.PlayerAbilities.DASH:
                setDash(true);
                break;
            case BossDrops.PlayerAbilities.WALLJUMP:
                setWallJump(true);
                break;
        }
    }

    void stopIgnoringPlayerDir()
    {
        ignorePlayerDir = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            foreach(ContactPoint2D c in collision.contacts)
            {
                if(c.normal.y >= 0.2f)
                {
                    hasDoubleJumped = true;
                    isGrounded = true;
                    jumpNum = 0;
                    ignorePlayerDir = false;
                }
            }
          
            CancelInvoke("stopIgnoringPlayerDir");
            GetComponentInChildren<GrassEffect>().spawnGrass();
        }

        if (collision.gameObject.tag == "Funds")
        {
            inventory.adjustFunds(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && collision.contacts[0].normal.y >= 0.2f)
        {
            isGrounded = true;
            hasDoubleJumped = false;
            ignorePlayerDir = false;
                      
            CancelInvoke("stopIgnoringPlayerDir");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = false;
            Invoke("checkGrounded", 0.5f);
        }
    }

    void checkGrounded()
    {
        if(!isGrounded)
        {
            switchAnimation(AnimationStates.JUMP);
        }
    }

    public void setCameraControlled(bool set)
    {
        playerControlled = set;
    }

    void pickUpItem()
    {
        if (!pickUp.gameObject.GetComponent<WorldItem>().getItemData().stackable)
        {
            if (inventory.checkInventorySpace())
            {
                inventory.addWeapon(pickUp.gameObject.GetComponent<WorldItem>().getItemData());
                Destroy(pickUp.transform.gameObject);
            }
        }
        else
        {
            if (inventory.checkIfStackable(pickUp.gameObject.GetComponent<WorldItem>().getItemData()))
            {
                inventory.addStackable(pickUp.gameObject.GetComponent<WorldItem>().getItemData());
                Destroy(pickUp.transform.gameObject);
            }
            else
            {
                inventory.addWeapon(pickUp.gameObject.GetComponent<WorldItem>().getItemData());
                Destroy(pickUp.transform.gameObject);
            }
        }

        effectParticles = Instantiate(pickUpEffect);
        effectParticles.transform.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            pickUp = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Building")
        {
            collision.GetComponent<Building>().showInteractIcon(true);
            currentBuilding = collision.GetComponent<Building>();
        }

        if(collision.tag == "Character")
        {
            character = collision.GetComponent<Character>();
        }
        if (collision.tag == "Trader")
        {
            trader = collision.GetComponent<Trader>();
        }

        if(collision.tag == "TransitionGate")
        {
            sceneLoader.switchScene(collision.GetComponent<TransitionGate>().getScene(), collision.GetComponent<TransitionGate>());
        }

        if (collision.gameObject.tag == "Pickup")
        {
            pickUp = collision.gameObject;
        }

        if(collision.gameObject.tag == "Hazard")
        {
            collision.GetComponentInParent<Killable>().attackPlayer();
        }

        if (collision.gameObject.tag == "Arrow")
        {
            if (!playerStatus.getRecentlyDamaged())
            {
                if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    playerStatus.takeDamage(1, true, 2);
                }
                else
                {
                    playerStatus.takeDamage(1, false, 2);
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
        if(collision.tag == "Pickup")
        {
            pickUp = null;
        }

        if (collision.transform.tag == "Building")
        {
            collision.GetComponent<Building>().showInteractIcon(false);
            currentBuilding = null;
        }

    }
}
