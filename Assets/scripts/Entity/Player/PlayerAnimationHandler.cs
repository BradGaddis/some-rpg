using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    // float timer = 0;
    // float timerDuration = 1;
    private Animator animator;

    [SerializeField] private float _forwardPunchAttackDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (timer <= 0) {
        //     bool isAttacking = animator.GetBool("isAttacking");
        //     timer = timerDuration;
        //     animator.SetBool("isAttacking", !isAttacking);
        // }
        // else {
        //     timer -= Time.deltaTime;
        // }
    }

    public void StartAttack() {
        StartCoroutine(ForwardPunchAttack());
    }

    public IEnumerator ForwardPunchAttack() {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(_forwardPunchAttackDuration);
        animator.SetBool("isAttacking", false);
    }
    
}
