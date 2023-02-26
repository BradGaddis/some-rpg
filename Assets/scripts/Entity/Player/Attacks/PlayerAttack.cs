using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS


public class PlayerAttack : MonoBehaviour
{
    // Collider2D[] attackHitboxes;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float timeTilImpact = 0.5f;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool enemyWasHit = false;
    private Vector3 moveDir;
    private float attackRange;  
    private float attackAOE;
    private Vector3 prevPos;

    // Componenet References
    [SerializeField] ParticleSystem attackParticles;
    private CircleCollider2D attackCollider;
    private PlayerAnimationHandler playerAnimation;
    private PlayerInput playerInput;

    protected void Start() {
        playerAnimation = GetComponentInParent<PlayerAnimationHandler>();
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        playerInput = GetComponentInParent<PlayerInput>();
        timeTilImpact = timeTilImpact > 0 ? timeTilImpact : 0; // time should always be positive
        attackRange = attackCollider.offset.magnitude;
        attackAOE = attackCollider.radius;
        Debug.Log("Attack range: " + attackRange);
    }

    virtual protected void Update() {
        moveDir = playerInput.GetMoveDirection();
        // use V key to attack
        ChangePosition();
        InitiateAttack();
    }

    void ChangePosition() {
            if (playerInput.IsMoving() && canAttack) {
                attackCollider.offset = moveDir;
            } 
    }

    virtual public void InitiateAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            // isAttacking = true;

            // Purely for animation purposes
            playerAnimation.StartForwardPunchAttack();

            StartCoroutine(AttackDuration());
        }
    }

    virtual protected IEnumerator AttackDuration() {
        Vector3 startPos = transform.parent.position;

        if(timeTilImpact > 0)
        {
            yield return new WaitForSeconds(timeTilImpact);
        }
        // This block is the actual duration of the attack 
        isAttacking = true;
        // This method handles the actual attack
        AttackEnemy();
        yield return new WaitForSeconds(playerAnimation.GetAnimationClipLength());
        canAttack = true;
        isAttacking = false;
        enemyWasHit = false;
        
        
        Vector3 currentPosition = transform.parent.position;
        if (startPos != currentPosition) {
            // if we moved while attacking, we should update the collider offset
            Vector3 newPos = currentPosition - startPos;
            // constrain new position length to attack range
            if (newPos.magnitude != attackRange) {
                newPos = newPos.normalized * attackRange;
            } 
            attackCollider.offset = newPos;
        }

    }
    
    virtual protected void AttackEnemy() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        // find the enemy by components 
        foreach (Collider2D collider in colliders) {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable) && isAttacking && !enemyWasHit){
                ParticleSystem particles = Instantiate(attackParticles, attackCollider.bounds.center, Quaternion.identity);
                Destroy(particles.gameObject, 3f);                
                enemyWasHit = true;       
                damageable.TakeDamage(attackDamage);
                break;
            }
        }
    }

}
