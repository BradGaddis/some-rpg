using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private AnimatorClipInfo[] _animatorClipInfo;

    private float animLen;

    [SerializeField] private float postAttackDelay = 0f;

    PlayerAttack[] attacks;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attacks = GetComponents<PlayerAttack>();
    }

    public void StartAttack() {
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
        animator.SetTrigger("attackTrigger");
        yield return StartCoroutine(GetAnimationClipInfo());
        float attackLength = GetAnimationClipLength() + postAttackDelay;
        yield return new WaitForSeconds(attackLength);
        animator.SetTrigger("attackTrigger");
    }
    
}
