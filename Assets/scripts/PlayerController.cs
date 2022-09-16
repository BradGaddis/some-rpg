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
    }

    private void HandleMovement(float moveHorizontal, float moveVertical)
    {
        rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * runSpeed;
    }
}
