using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private float postAttackDelay = 0f;
    [SerializeField] bool isFlippable = true;
    bool isFlipped = false;
    private Animator animator;
    private AnimatorClipInfo[] _animatorClipInfo;
    private float animLen;
    Vector3 moveDirection;
    SpriteRenderer spriteRenderer;
    // TODO : make this a Vector3
    float prevPos = 0f; 

    PlayerInput playerInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        if (playerInput.IsMoving()) {
            moveDirection = playerInput.GetMoveDirection();
        }
    }

    public void StartForwardPunchAttack() {
        StartCoroutine(ForwardPunchAttack());
    }

    // Get current animation clip info
    private IEnumerator GetAnimationClipInfo() {
        yield return null;
        _animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
    }

    public float GetAnimationClipLength() {
        _animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return _animatorClipInfo[0].clip.length;
    }

    public IEnumerator ForwardPunchAttack() {
        // Start attack animation
        animator.SetTrigger("attackTrigger");
        flipSprite(isFlippable && moveDirection.x < 0);
        // Wait for animation to finish
        yield return StartCoroutine(GetAnimationClipInfo());
        
        float attackLength = GetAnimationClipLength() + postAttackDelay;
        // Delay how long the attack is active
        yield return new WaitForSeconds(attackLength);
        flipSprite(); 
        animator.SetTrigger("attackTrigger");
    }
    
    private void flipSprite(bool shouldFlip = false) {
        if (shouldFlip && !isFlipped) {
            isFlipped = true;
            spriteRenderer.flipX = true;
        } 
        else if (!shouldFlip && isFlipped) {
            isFlipped = false;
            spriteRenderer.flipX = false;
        }
        
    }
    
}
