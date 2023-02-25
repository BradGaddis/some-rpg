using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("This is a test to see if I like the movement locked or not. Just for toggle")] [SerializeField] bool lockedMovement;
    [SerializeField] [Header("The Player Size Mid-Jump")] float jumpSize; // TODO: make this actually work
    // stats stribable object
    [SerializeField] Stats stats;
    [SerializeField] float jumpForce;

    // Player State Machine
    PlayerStateMachine currentState = new PlayerStateMachine(PlayerState.Idle);
    float runSpeed;

    // Component References
    PlayerInput playerInput;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        // update stats from stats scriptable object
        runSpeed = stats.GetSpeed();
        jumpForce = stats.GetJumpForce(); // this it the scale the player will be scaled to when "jumping"
    }

    // Update is called once per frame
    void Update()
    {
       playerInput.HandleMovement(runSpeed, currentState, rb, lockedMovement);
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(Jump());
        // }
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

   
}
