using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private AnimatorClipInfo[] _animatorClipInfo;
    private float animLen;

    [SerializeField] private float postForwardPunchDelay = 0f;
    Vector3 currentDirection = Vector3.zero;
    float startX = 0f; // set at the start of an animation to determine if the sprite should be return to starting position
    bool isFlippable = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentDirection = Vector3.right;
    }

    private void Update() {
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            currentDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
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

    private float GetAnimationClipLength() {
        _animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return _animatorClipInfo[0].clip.length;
    }

    public IEnumerator ForwardPunchAttack() {
        startX = currentDirection.x;

        // Start attack animation
        animator.SetTrigger("attackTrigger");
        flipSprite(isFlippable);
        // Wait for animation to finish
        yield return StartCoroutine(GetAnimationClipInfo());
        
        float attackLength = GetAnimationClipLength() + postForwardPunchDelay;
        // Delay how long the attack is active
        yield return new WaitForSeconds(attackLength);
        flipSprite(isFlippable && startX != currentDirection.x); 
        animator.SetTrigger("attackTrigger");
    }
    
    void flipSprite(bool shouldFlip) {
        if (shouldFlip) {
            transform.localScale = new Vector3(Mathf.Sign(currentDirection.x), 1f, 1f);
        } 
        else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
