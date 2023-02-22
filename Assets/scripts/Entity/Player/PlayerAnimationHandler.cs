using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    float timer = 0;
    float timerDuration = 1;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) {
            bool isAttacking = animator.GetBool("isAttacking");
            timer = timerDuration;
            animator.SetBool("isAttacking", !isAttacking);
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
