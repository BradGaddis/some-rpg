using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float runSpeed;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(Input.GetAxisRaw("Horizontal"), 
                       Input.GetAxisRaw("Vertical"));   

        if (Input.GetKeyDown (KeyCode.Space)) {
            StartCoroutine (Jump());
        }   
    }

    private void HandleMovement(float moveHorizontal, float moveVertical) => rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    
    IEnumerator Jump() {
        Vector2 curscale = transform.localScale;
        float scaleChange = curscale.x + 0.5f;
        transform.localScale = new Vector2(scaleChange, scaleChange);
        // animate the jump
        return null;
    }

    public void AddPowerUp(PowerUp powerUp) {
        // add power up to player
        Debug.Log("Power Up Collected: " + powerUp.name);
    }

}