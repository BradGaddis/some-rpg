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
    float jumpForce;

    Tool hammer = new Tool();

    // player state
    PlayerState currentState = PlayerState.Idle;
    // Player State Machine
    PlayerStateMachine playerStateMachine = new PlayerStateMachine(PlayerState.Idle);

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
        if (currentState != PlayerState.Interacting)
        {
            HandleMovement(Input.GetAxisRaw("Horizontal"), 
                        Input.GetAxisRaw("Vertical"));   
        } 
        else
        {   
            HandleMovement(0, 0);
        }

        if (Input.GetKeyDown (KeyCode.Space) && currentState != PlayerState.Jumping) {
            StartCoroutine (Jump());
        }   

        HandleActions();
    }

    private void HandleMovement(float moveHorizontal, float moveVertical) => rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    
    //  TODO fix double call
    IEnumerator Jump() {
        // hold state
        PlayerState prevState = currentState;
        currentState = PlayerState.Jumping;
        Vector2 curscale = transform.localScale;
        float scaleChange = curscale.x + 0.5f;
        transform.localScale = new Vector2(scaleChange, scaleChange);
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(1,1,1);
        currentState = prevState;
    }

    public void AddPowerUp(PowerUp powerUp) {
        // add power up to player
        Debug.Log("Power Up Collected: " + powerUp.name);
    }

    // switch tool
    IEnumerator UseTool(Tool tool = null) {
        // hold state
        PlayerState prevState = currentState;
        currentState = PlayerState.Interacting;
        Debug.Log("Using tool: " + tool.name);
        yield return new WaitForSeconds(1f);
        currentState = prevState;
    }

    // action handler
    public void HandleActions() {
        // lock movement
        if (Input.GetKeyDown (KeyCode.E) && currentState != PlayerState.Jumping) {
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