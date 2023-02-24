using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private AnimatorClipInfo[] _animatorClipInfo;
    private float animLen;

    [SerializeField] private float postForwardPunchDelay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("attackTrigger");
        yield return StartCoroutine(GetAnimationClipInfo());
        float attackLength = GetAnimationClipLength() + postForwardPunchDelay;
        yield return new WaitForSeconds(attackLength);
        animator.SetTrigger("attackTrigger");
    }
    
}
