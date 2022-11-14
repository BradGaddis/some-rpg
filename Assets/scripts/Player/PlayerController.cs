using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    float runSpeed;
    // stats stribable object
    [SerializeField]
    Stats stats;
    
    // stats
    int health;
    int damage;
    int speed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    [Header("The Player Size Mid-Jump")]
    float jumpSize;

    Tool hammer = new Tool();

    
    // Player State Machine
    PlayerStateMachine currentState = new PlayerStateMachine(PlayerState.Idle);

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // get stats
        runSpeed = stats.GetSpeed();
        health = stats.GetHealth();
        damage = stats.GetDamage();
        jumpForce = stats.GetJumpForce(); // this it the scale the player will be scaled to when "jumping"
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState.state != PlayerState.Interacting)
        {
            HandleMovement(Input.GetAxisRaw("Horizontal"), 
                        Input.GetAxisRaw("Vertical"));   
        } 
        else
        {   
            HandleMovement(0, 0);
        }

        // only call once per frame
        if (Input.GetKeyDown (KeyCode.Space) && currentState.state != PlayerState.Jumping) {
            StartCoroutine (Jump());
        }   

        HandleActions();
    }

    // private void HandleMovement(float moveHorizontal, float moveVertical) => rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    // movement
    private void HandleMovement(float moveHorizontal, float moveVertical)
    {
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            currentState.ChangeState(PlayerState.Running);
        }
        else
        {
            currentState.ChangeState(PlayerState.Idle);
        }
        // lock diagonal movement
        if (moveHorizontal != 0 && moveVertical != 0)
        {
            moveHorizontal = 0;
        }
        rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    }
    
    //  TODO fix double call
    IEnumerator Jump() {
        PlayerState prevState = currentState.state;
        currentState.ChangeState(PlayerState.Jumping);

        
        Vector2 curscale = transform.localScale;
        float scaleChange = curscale.x + jumpSize;
        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        
        transform.localScale = new Vector2(scaleChange, scaleChange);
        playerSprite.transform.position = new Vector2(playerSprite.transform.position.x, playerSprite.transform.position.y + jumpForce);

        yield return new WaitForSeconds(jumpForce);

        GameObject shadow = GameObject.Find("Player Shadow");

        // get shadow position
        Vector3 shadowPosCenter = shadow.GetComponent<BoxCollider2D>().bounds.center;

        playerSprite.transform.position = new Vector2(shadowPosCenter.x, shadowPosCenter.y + playerSprite.bounds.extents.y);
        transform.localScale = curscale;
        currentState.ChangeState(prevState);

        // find Player Sprite in children
    }
    
    public void AddPowerUp(PowerUp powerUp) {
        // add power up to player
        Debug.Log("Power Up Collected: " + powerUp.name);
    }

    // switch tool
    IEnumerator UseTool(Tool tool = null) {
        // hold state
        PlayerState prevState = currentState.state;
        currentState.ChangeState(PlayerState.Interacting);
        Debug.Log("Using tool: " + tool.name);
        yield return new WaitForSeconds(1f);
        currentState.ChangeState(prevState);
    }

    // action handler
    public void HandleActions() {
        if (Input.GetKeyDown (KeyCode.E) && currentState.state != PlayerState.Jumping) {
            StartCoroutine (UseTool(hammer));
        }
    }
}

// Tool class
public class Tool {
    // tool name
    public string name;
    // tool damage
    int damage;
    // tool speed
    int speed;
    // tool range
    int range;
    // tool cooldown
    float cooldown;
    // tool type
    string type;

    public Tool(string name = "Hammer", int damage = 1, int speed = 1, int range = 1, float cooldown = 1f, string type = "melee") {
        this.name = name;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.cooldown = cooldown;
        this.type = type;
    }
}